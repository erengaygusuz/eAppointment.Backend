using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Admins.CreateAdmin
{
    internal sealed class CreateAdminCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<CreateAdminCommandHandler> logger) : IRequestHandler<CreateAdminCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Admins.CreateAdmin.Others";

            User user = mapper.Map<User>(request);

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                logger.LogError("User could not created");

                return Result<string>.Failure(500, localization[translatedMessagePath + "." + "CannotCreated"]);
            }

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Admin");

            if (!roleResult.Succeeded)
            {
                logger.LogError("Role could not add to user");

                return Result<string>.Failure(500, localization[translatedMessagePath + "." + "RoleCannotAdded"]);
            }

            logger.LogInformation("Admin created successfully");

            return Result<string>.Succeed(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);
        }
    }
}
