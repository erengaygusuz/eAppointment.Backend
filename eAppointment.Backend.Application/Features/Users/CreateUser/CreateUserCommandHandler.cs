using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.CreateUser
{
    internal sealed class CreateUserCommandHandler(
        UserManager<AppUser> userManager,
        IUserRoleRepository userRoleRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.Users.AnyAsync(p => p.Email == request.email))
            {
                return Result<string>.Failure("Email alraedy exists");
            }

            if (await userManager.Users.AnyAsync(p => p.UserName == request.userName))
            {
                return Result<string>.Failure("User Name alraedy exists");
            }

            AppUser appUser = mapper.Map<AppUser>(request);

            IdentityResult result = await userManager.CreateAsync(appUser, request.password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            if (request.roleIds.Any())
            {
                List<AppUserRole> appUserRoles = new();

                foreach (var roleId in request.roleIds)
                {
                    AppUserRole appUserRole = new()
                    {
                        RoleId = roleId,
                        UserId = appUser.Id
                    };

                    appUserRoles.Add(appUserRole);
                }

                await userRoleRepository.AddRangeAsync(appUserRoles, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return "User created successfully";
        }
    }
}
