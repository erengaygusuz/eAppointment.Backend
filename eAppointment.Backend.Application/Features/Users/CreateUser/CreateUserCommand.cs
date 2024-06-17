using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.CreateUser
{
    public sealed record CreateUserCommand(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string userName,
        string password,
        string identityNumber,
        Guid departmentId,
        Guid roleId,
        string roleName) : IRequest<Result<string>>;
}
