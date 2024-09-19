using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Auth.Login
{
    public sealed record LoginCommand(
        string userName, 
        string password) : IRequest<Result<LoginCommandResponse>>;
}
