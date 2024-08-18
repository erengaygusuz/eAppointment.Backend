using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.Json;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.CreateAdmin
{
    internal sealed class CreateAdminCommandHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateAdminCommand> createAdminCommandValidator,
        IStringLocalizer<object> localization,
        ILogger<CreateAdminCommandHandler> logger) : IRequestHandler<CreateAdminCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Admins.CreateAdmin.Others";

            logger.LogInformation("Validation Started");

            var validationResult = await createAdminCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                logger.LogError("Validation Completed with Errors: ", JsonSerializer.Serialize(validationResult.Errors.Select(x => x.ErrorMessage).ToList()));

                return Result<string>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            logger.LogInformation("Validation Completed Successfully");

            logger.LogInformation("Mapping Started");

            User user = mapper.Map<User>(request);

            logger.LogInformation("Mapping Completed Successfully");

            logger.LogInformation("User Creation Started");

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                logger.LogError(localization[translatedMessagePath + "." + "CannotCreated"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CannotCreated"]);
            }

            logger.LogInformation("User Creation Completed Successfully");

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            logger.LogInformation("User Role Addition Started");

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Admin");

            if (!roleResult.Succeeded)
            {
                logger.LogError(localization[translatedMessagePath + "." + "RoleCannotAdded"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "RoleCannotAdded"]);
            }

            logger.LogInformation("User Role Addition Completed Successfully");

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);

            return Result<string>.Succeed(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);
        }
    }
}
