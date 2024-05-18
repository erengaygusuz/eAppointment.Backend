using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Roles.RoleSync
{
    internal sealed class RoleSyncCommandHandler (
        RoleManager<AppRole> roleManager): IRequestHandler<RoleSyncCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RoleSyncCommand request, CancellationToken cancellationToken)
        {
            List<AppRole> currentRoles = await roleManager.Roles.ToListAsync(cancellationToken);

            List<AppRole> staticRoles = Constants.GetRoles();

            foreach (AppRole role in currentRoles)
            {
                if (!staticRoles.Any(r => r.Name == role.Name))
                {
                    await roleManager.DeleteAsync(role);
                }
            }

            foreach (AppRole role in staticRoles)
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
