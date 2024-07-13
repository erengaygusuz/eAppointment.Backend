using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Roles.GetAllRoles
{
    public sealed record GetAllRolesQuery() : IRequest<Result<List<GetAllRolesQueryResponse>>>;
}
