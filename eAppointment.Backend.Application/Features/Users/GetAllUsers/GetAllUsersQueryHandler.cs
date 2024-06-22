using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler (
        UserManager<User> userManager,
        IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
    {
        public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersWithRolesTuples = new List<Tuple<User, List<string>>>();

            var users = userManager.Users.ToList();

            for (int i = 0; i < users.Count; i++)
            {
                var userRoles = await userManager.GetRolesAsync(users[i]);

                var tuple = new Tuple<User, List<string>>(users[i], userRoles.ToList());

                usersWithRolesTuples.Add(tuple);
            }

            var response = mapper.Map<List<GetAllUsersQueryResponse>>(usersWithRolesTuples);

            return response;
        }
    }
}
