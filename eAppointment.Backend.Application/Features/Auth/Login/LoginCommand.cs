using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Auth.Login
{
    public sealed record LoginCommand(
        string userNameOrEmail, 
        string password) : IRequest<Result<LoginCommandResponse>>;
}
