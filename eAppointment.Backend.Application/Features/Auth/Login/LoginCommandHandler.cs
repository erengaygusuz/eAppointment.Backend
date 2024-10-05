using eAppointment.Backend.Application.Services;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Auth.Login
{
    internal sealed class LoginCommandHandler(
        UserManager<User> userManager, 
        IJwtProvider jwtProvider,
        IStringLocalizer<object> localization,
        ILogger<LoginCommandHandler> logger) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Auth.Login.Others";

            User? appUser = await userManager.Users
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .FirstOrDefaultAsync(p => p.UserName == request.userName, cancellationToken);

            if (appUser is null)
            {
                logger.LogError("User could not found");

                return Result<LoginCommandResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(appUser, request.password);

            if (!isPasswordCorrect)
            {
                logger.LogError("User password is wrong");

                return Result<LoginCommandResponse>.Failure((int)HttpStatusCode.InternalServerError, localization[translatedMessagePath + "." + "WrongPassword"]);
            }

            string token = await jwtProvider.CreateTokenAsync(appUser);

            LoginCommandResponse response = new(token);

            logger.LogInformation("User successfully logged in");

            return Result<LoginCommandResponse>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
