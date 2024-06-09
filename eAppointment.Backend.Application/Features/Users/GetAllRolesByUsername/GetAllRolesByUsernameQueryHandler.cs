using eAppointment.Backend.Application.Features.Users.GetAllRolesByUsername;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllRolesForUser
{
    internal sealed class GetAllRolesByUsernameQueryHandler (
        RoleManager<AppRole> roleManager,
        IUserRoleRepository userRoleRepository) : IRequestHandler<GetAllRolesByUsernameQuery, Result<List<AppRole>>>
    {
        public async Task<Result<List<AppRole>>> Handle(GetAllRolesByUsernameQuery request, CancellationToken cancellationToken)
        {
            List<AppUserRole> userRoles = await userRoleRepository
                   .Where(p => p.UserId == request.id).ToListAsync(cancellationToken);

            List<AppRole> roles = new();

            foreach (var userRole in userRoles)
            {
                AppRole? role = await roleManager.Roles
                    .Where(p => p.Id == userRole.RoleId).FirstOrDefaultAsync(cancellationToken);

                if (role is not null)
                {
                    roles.Add(role);
                }
            }

            return roles;
        }
    }
}
