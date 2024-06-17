using eAppointment.Backend.Application.Features.Roles.GetAllRoles;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Roles.GetAllRolesByUserId
{
    public sealed record GetAllRolesByUserIdQuery(Guid userId) : IRequest<Result<List<GetAllRolesQueryResponse>>>;
}
