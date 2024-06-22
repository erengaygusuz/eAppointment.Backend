using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.UpdateAdminById
{
    internal sealed class UpdateAdminByIdCommandHandler(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IMapper mapper) : IRequestHandler<UpdateAdminByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAdminByIdCommand request, CancellationToken cancellationToken)
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
            user.RoleId = roleManager.Roles.Where(r => r.Name == "Admin").FirstOrDefault()!.Id;

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            return "User updated successfully";
        }
    }
}
