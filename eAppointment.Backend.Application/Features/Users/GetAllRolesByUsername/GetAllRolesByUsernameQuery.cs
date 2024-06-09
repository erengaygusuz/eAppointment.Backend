using eAppointment.Backend.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllRolesByUsername
{
    public sealed record GetAllRolesByUsernameQuery(Guid id) : IRequest<Result<List<AppRole>>>;
}
