using eAppointment.Backend.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllRolesForUser
{
    public sealed record GetAllRolesForUserQuery() : IRequest<Result<List<AppRole>>>;
}
