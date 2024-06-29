using eAppointment.Backend.Application.Services;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Auth.Login
{
    internal sealed class LoginCommandHandler(UserManager<User> userManager, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? appUser = await userManager.Users
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(p => p.UserName == request.userNameOrEmail || 
               p.Email == request.userNameOrEmail, cancellationToken);

            if (appUser is null)
            {
                return Result<LoginCommandResponse>.Failure("User not found.");
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(appUser, request.password);

            if(!isPasswordCorrect)
            {
                return Result<LoginCommandResponse>.Failure("Password is wrong.");
            }

            string token = await jwtProvider.CreateTokenAsync(appUser);
            LoginCommandResponse response = new(token);

            return Result<LoginCommandResponse>.Succeed(response);
        }
    }
}
