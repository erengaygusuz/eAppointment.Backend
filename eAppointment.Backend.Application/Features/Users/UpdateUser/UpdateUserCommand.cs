using eAppointment.Backend.Domain.Entities;
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
        List<Role> roles) : IRequest<Result<string>>;
}
