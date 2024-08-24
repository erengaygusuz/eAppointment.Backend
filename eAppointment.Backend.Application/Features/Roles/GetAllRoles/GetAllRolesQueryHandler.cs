using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Roles.GetAllRoles
{
    internal sealed class GetAllRolesQueryHandler(
        RoleManager<Role> roleManager) : IRequestHandler<GetAllRolesQuery, Result<List<GetAllRolesQueryResponse>>>
    {
        public async Task<Result<List<GetAllRolesQueryResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            List<Role> roles = await roleManager.Roles
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);

            List<GetAllRolesQueryResponse> response =
                roles.Select(s =>
                    new GetAllRolesQueryResponse
                    (
                        id: s.Id,
                        name: s.Name
                    )).ToList();

            return response;
        }
    }
}
