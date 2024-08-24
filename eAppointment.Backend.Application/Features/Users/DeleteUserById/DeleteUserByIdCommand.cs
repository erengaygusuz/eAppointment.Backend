using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Users.DeleteUserById
{
    public sealed record DeleteUserByIdCommand(
        int id) : IRequest<Result<string>>;
}
