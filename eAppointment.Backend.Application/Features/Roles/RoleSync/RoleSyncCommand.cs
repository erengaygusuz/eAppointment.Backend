using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Roles.RoleSync
{
    public sealed record RoleSyncCommand() : IRequest<Result<string>>;
}
