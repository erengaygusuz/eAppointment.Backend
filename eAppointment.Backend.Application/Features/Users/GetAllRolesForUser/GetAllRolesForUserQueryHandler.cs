using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllRolesForUser
{
    internal sealed class GetAllRolesForUserQueryHandler (
        RoleManager<AppRole> roleManager): IRequestHandler<GetAllRolesForUserQuery, Result<List<AppRole>>>
    {
        public async Task<Result<List<AppRole>>> Handle(GetAllRolesForUserQuery request, CancellationToken cancellationToken)
        {
            List<AppRole> roles = await roleManager.Roles.OrderBy(p => p.Name).ToListAsync(cancellationToken);

            return roles;
        }
    }
}
