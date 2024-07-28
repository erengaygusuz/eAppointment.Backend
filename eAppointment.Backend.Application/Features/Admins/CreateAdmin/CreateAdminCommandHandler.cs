using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Data;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.CreateAdmin
{
    internal sealed class CreateAdminCommandHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateAdminCommand> createAdminCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<CreateAdminCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Admins.CreateAdmin.Others";

            var validationResult = await createAdminCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            User user = mapper.Map<User>(request);

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "CannotCreated"]);
            }

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Admin");

            if (!roleResult.Succeeded)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "RoleCannotAdded"]);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value;
        }
    }
}
