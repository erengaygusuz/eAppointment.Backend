using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.UpdateUser
{
    internal sealed class UpdateUserCommandHandler(
        UserManager<AppUser> userManager,
        IUserRoleRepository userRoleRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByIdAsync(request.id.ToString());

            if (appUser is null)
            {
                return Result<string>.Failure("User not found");
            }

            if (appUser.Email != request.email)
            {
                if (await userManager.Users.AnyAsync(p => p.Email == request.email))
                {
                    return Result<string>.Failure("Email alraedy exists");
                }
            }

            if (appUser.UserName != request.userName)
            {

                if (await userManager.Users.AnyAsync(p => p.UserName == request.userName))
                {
                    return Result<string>.Failure("User Name alraedy exists");
                }
            }

            mapper.Map(request, appUser);

            IdentityResult result = await userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            if (request.roles.Any())
            {
                List<AppUserRole> appUserRoles = await userRoleRepository
                    .Where(p => p.UserId == appUser.Id).ToListAsync();

                userRoleRepository.DeleteRange(appUserRoles);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                appUserRoles = new();

                foreach (var role in request.roles)
                {
                    AppUserRole appUserRole = new()
                    {
                        RoleId = role.Id,
                        UserId = appUser.Id
                    };

                    appUserRoles.Add(appUserRole);
                }

                await userRoleRepository.AddRangeAsync(appUserRoles, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return "User updated successfully";
        }
    }
}
