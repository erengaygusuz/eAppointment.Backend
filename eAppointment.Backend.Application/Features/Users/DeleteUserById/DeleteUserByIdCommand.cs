using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.DeleteUserById
{
    public sealed record DeleteUserByIdCommand(
        Guid id) : IRequest<Result<string>>;
}
