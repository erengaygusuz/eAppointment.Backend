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
        UserManager<User> userManager,
        IUserRoleRepository userRoleRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                return Result<string>.Failure("User not found");
            }

            if (user.UserName != request.userName)
            {

                if (await userManager.Users.AnyAsync(p => p.UserName == request.userName))
                {
                    return Result<string>.Failure("User Name alraedy exists");
                }
            }

            mapper.Map(request, user);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            if (request.roles.Any())
            {
                List<UserRole> userRoles = await userRoleRepository
                    .Where(p => p.UserId == user.Id).ToListAsync();

                userRoleRepository.DeleteRange(userRoles);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                userRoles = new();

                foreach (var role in request.roles)
                {
                    UserRole userRole = new()
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    };

                    userRoles.Add(userRole);
                }

                await userRoleRepository.AddRangeAsync(userRoles, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return "User updated successfully";
        }
    }
}
