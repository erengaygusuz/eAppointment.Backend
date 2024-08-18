using eAppointment.Backend.Application.Services;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

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
                .FirstOrDefaultAsync(p => p.UserName == request.userNameOrEmail ||
               p.Email == request.userNameOrEmail, cancellationToken);

            if (appUser is null)
            {
                logger.LogError(localization[translatedMessagePath + "." + "NotFound"].Value);

                return Result<LoginCommandResponse>.Failure(localization[translatedMessagePath + "." + "NotFound"]);
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(appUser, request.password);

            if (!isPasswordCorrect)
            {
                logger.LogError(localization[translatedMessagePath + "." + "WrongPassword"].Value);

                return Result<LoginCommandResponse>.Failure(localization[translatedMessagePath + "." + "WrongPassword"]);
            }

            string token = await jwtProvider.CreateTokenAsync(appUser);

            LoginCommandResponse response = new(token);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);

            return Result<LoginCommandResponse>.Succeed(response);
        }
    }
}
