using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.DeleteUserById
{
    internal sealed class DeleteUserByIdCommandHandler(
        UserManager<User> userManager,
        IStringLocalizer<object> localization,
        ILogger<DeleteUserByIdCommandHandler> logger) : IRequestHandler<DeleteUserByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Users.DeleteUser.Others";

            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                logger.LogError("User could not found");

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            IdentityResult result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                logger.LogError("User could not deleted");

                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            logger.LogInformation("User deleted successfully");

            return localization[translatedMessagePath + "." + "SuccessfullyDeleted"].Value;
        }
    }
}
