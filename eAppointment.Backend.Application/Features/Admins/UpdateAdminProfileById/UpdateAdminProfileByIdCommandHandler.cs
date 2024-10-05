using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

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

                return Result<string>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            if (request.profilePhoto != null)
            {
                DotNetEnv.Env.Load();

                var userProfileImagesFolderPath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}";

                if (!Directory.Exists(userProfileImagesFolderPath))
                {
                    Directory.CreateDirectory(userProfileImagesFolderPath);
                }

                if (!string.IsNullOrEmpty(user.ProfilePhotoPath))
                {
                    File.Delete(user.ProfilePhotoPath);

                    user.ProfilePhotoPath = "";
                }

                mapper.Map(request, user);

                user.ProfilePhotoPath = Guid.NewGuid() + ".png";

                using var stream = new FileStream($"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + user.ProfilePhotoPath, FileMode.Create);

                await request.profilePhoto.CopyToAsync(stream);
            }

            else
            {
                mapper.Map(request, user);
            }

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                logger.LogError("User could not updated");

                return Result<string>.Failure((int)HttpStatusCode.InternalServerError, localization[translatedMessagePath + "." + "CouldNotUpdated"]);
            }

            logger.LogInformation("Admin profile updated successfully");

            return Result<string>.Succeed((int)HttpStatusCode.OK, localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value);
        }
    }
}
