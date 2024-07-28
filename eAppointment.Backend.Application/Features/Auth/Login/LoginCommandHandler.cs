using eAppointment.Backend.Application.Services;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Auth.Login
{
    internal sealed class LoginCommandHandler(
        UserManager<User> userManager, 
        IJwtProvider jwtProvider,
        IValidator<LoginCommand> loginCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Auth.Login.Others";

            var validationResult = await loginCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<LoginCommandResponse>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            User? appUser = await userManager.Users
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .FirstOrDefaultAsync(p => p.UserName == request.userNameOrEmail ||
               p.Email == request.userNameOrEmail, cancellationToken);

            if (appUser is null)
            {
                return Result<LoginCommandResponse>.Failure(localization[translatedMessagePath + "." + "NotFound"]);
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(appUser, request.password);

            if (!isPasswordCorrect)
            {
                return Result<LoginCommandResponse>.Failure(localization[translatedMessagePath + "." + "WrongPassword"]);
            }

            string token = await jwtProvider.CreateTokenAsync(appUser);

            LoginCommandResponse response = new(token);

            return Result<LoginCommandResponse>.Succeed(response);
        }
    }
}
