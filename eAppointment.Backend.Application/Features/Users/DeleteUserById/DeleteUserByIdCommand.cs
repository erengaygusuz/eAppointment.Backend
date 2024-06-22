using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.DeleteUserById
{
    public sealed record DeleteUserByIdCommand(
        int id) : IRequest<Result<string>>;
}
