using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.UpdateUser
{
    public sealed record UpdateUserCommand( 
        Guid id,
        string firstName,
        string lastName,
        string phoneNumber,
        string userName,
        Guid roleId,
        string roleName) : IRequest<Result<string>>;
}
