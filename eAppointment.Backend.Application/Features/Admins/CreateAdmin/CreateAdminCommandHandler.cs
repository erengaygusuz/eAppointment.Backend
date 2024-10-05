using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

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

                return Result<string>.Failure((int)HttpStatusCode.InternalServerError, localization[translatedMessagePath + "." + "CouldNotCreated"]);
            }

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            var roleResult = await userManager.AddToRoleAsync(addedUser!, Domain.Constants.Roles.Admin);

            if (!roleResult.Succeeded)
            {
                logger.LogError("Role could not add to user");

                return Result<string>.Failure((int)HttpStatusCode.InternalServerError, localization[translatedMessagePath + "." + "RoleCouldNotAdded"]);
            }

            logger.LogInformation("Admin created successfully");

            return Result<string>.Succeed((int)HttpStatusCode.Created, localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);
        }
    }
}
