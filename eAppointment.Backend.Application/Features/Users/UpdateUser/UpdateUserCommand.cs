using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.UpdateUser
{
    public sealed record UpdateUserCommand( 
        Guid id,
        string firstName,
        string lastName,
        string email,
        string userName,
        List<Guid> roleIds) : IRequest<Result<string>>;
}
