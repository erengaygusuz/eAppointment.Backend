using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Auth.Login
{
    public sealed record LoginCommand(
        string userNameOrEmail, 
        string password) : IRequest<Result<LoginCommandResponse>>;
}
