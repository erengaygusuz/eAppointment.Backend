using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.CreateAdmin
{
    internal sealed class CreateAdminCommandHandler(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateAdminCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.Users.AnyAsync(p => p.Email == request.email))
            {
                return Result<string>.Failure("Email alraedy exists");
            }

            if (await userManager.Users.AnyAsync(p => p.UserName == request.userName))
            {
                return Result<string>.Failure("User Name alraedy exists");
            }

            User user = mapper.Map<User>(request);
            user.RoleId = roleManager.Roles.Where(r => r.Name == "Admin").FirstOrDefault()!.Id;

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "User created successfully";
        }
    }
}
