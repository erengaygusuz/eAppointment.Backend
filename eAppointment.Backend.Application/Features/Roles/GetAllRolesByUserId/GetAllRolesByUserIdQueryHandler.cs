using eAppointment.Backend.Application.Features.Roles.GetAllRoles;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Roles.GetAllRolesByUserId
{
    internal sealed class GetAllRolesByUserIdQueryHandler(
        RoleManager<Role> roleManager,
        IUserRoleRepository userRoleRepository) : IRequestHandler<GetAllRolesByUserIdQuery, Result<List<GetAllRolesQueryResponse>>>
    {
        public async Task<Result<List<GetAllRolesQueryResponse>>> Handle(GetAllRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<UserRole> userRoles = await userRoleRepository
                   .Where(p => p.UserId == request.userId).ToListAsync(cancellationToken);

            List<Role> roles = new();

            foreach (var userRole in userRoles)
            {
                Role? role = await roleManager.Roles
                    .Where(p => p.Id == userRole.RoleId).FirstOrDefaultAsync(cancellationToken);

                if (role is not null)
                {
                    roles.Add(role);
                }
            }

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
