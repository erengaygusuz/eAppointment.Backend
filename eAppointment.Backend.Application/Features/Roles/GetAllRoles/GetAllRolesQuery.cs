using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Roles.GetAllRoles
{
    public sealed record GetAllRolesQuery() : IRequest<Result<List<GetAllRolesQueryResponse>>>;
}
