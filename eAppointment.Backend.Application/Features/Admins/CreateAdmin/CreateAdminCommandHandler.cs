using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
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
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateAdminCommand> createAdminCommandValidator) : IRequestHandler<CreateAdminCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await createAdminCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            if (await userManager.Users.AnyAsync(p => p.Email == request.email))
            {
                return Result<string>.Failure("Email alraedy exists");
            }

            if (await userManager.Users.AnyAsync(p => p.UserName == request.userName))
            {
                return Result<string>.Failure("User Name alraedy exists");
            }

            User user = mapper.Map<User>(request);

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Admin");

            if (!roleResult.Succeeded)
            {
                return Result<string>.Failure("Role could not add to user");
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "User created successfully";
        }
    }
}
