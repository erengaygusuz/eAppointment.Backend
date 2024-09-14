using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Admins.UpdateAdminProfileById
{
    internal sealed class UpdateAdminProfileByIdCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<UpdateAdminProfileByIdCommandHandler> logger) : IRequestHandler<UpdateAdminProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAdminProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Admins.UpdateAdminProfile.Others";

            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                logger.LogError("User could not found");

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            mapper.Map(request, user);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                logger.LogError("User could not updated");

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotUpdated"]);
            }

            logger.LogInformation("Admin updated successfully");

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
