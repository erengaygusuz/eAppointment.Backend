using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Roles.RoleSync
{
    internal sealed class RoleSyncCommandHandler (
        RoleManager<Role> roleManager): IRequestHandler<RoleSyncCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RoleSyncCommand request, CancellationToken cancellationToken)
        {
            List<Role> currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);

            List<Role> staticRoles = Constants.GetRoles();

            foreach (Role role in currentRoles)
            {
                if (!staticRoles.Any(r => r.Name == role.Name))
                {
                    await roleManager.DeleteAsync(role);
                }
            }

            foreach (Role role in staticRoles)
            {
                if (!currentRoles.Any(r => r.Name == role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            return "Role Sync is successfull";
        }
    }
}
