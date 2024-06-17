using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler (
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IUserRoleRepository userRoleRepository) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
    {
        public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await userManager.Users.OrderBy(u => u.FirstName).ToListAsync(cancellationToken);

            List<GetAllUsersQueryResponse> response = users.Select(s => new GetAllUsersQueryResponse()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FirstName + " " + s.LastName,
                UserName = s.UserName,
                Email = s.Email

            }).ToList();

            foreach(var item in response)
            {
                List<UserRole> userRoles = await userRoleRepository
                    .Where(p => p.UserId == item.Id).ToListAsync(cancellationToken);

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

                item.RoleId = roles.FirstOrDefault()!.Id;
                item.RoleName = roles.FirstOrDefault()!.Name;
            }

            return response;
        }
    }
}
