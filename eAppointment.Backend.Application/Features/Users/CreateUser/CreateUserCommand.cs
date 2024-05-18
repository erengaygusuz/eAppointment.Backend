using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.CreateUser
{
    public sealed record CreateUserCommand(
        string firstName,
        string lastName,
        string email,
        string userName,
        string password,
        List<Guid> roleIds) : IRequest<Result<string>>;
}
