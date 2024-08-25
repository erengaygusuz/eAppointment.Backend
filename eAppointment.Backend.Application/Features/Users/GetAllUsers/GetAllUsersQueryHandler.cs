using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using eAppointment.Backend.Domain.Extensions;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler(
        UserManager<User> userManager,
        IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<GetAllUsersQueryTableResponse>>
    {
        public async Task<Result<GetAllUsersQueryTableResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersWithRolesTuples = new List<Tuple<User, List<string>>>();

            var users = userManager.Users;

            int totalCount = users.Count();
            int filteredCount = totalCount;

            users = users.Skip(request.skip).Take(request.take);

            if (!string.IsNullOrEmpty(request.searchTerm))
            {
                users = users.Where(c => c.FirstName.Contains(request.searchTerm) ||
                                         c.LastName.Contains(request.searchTerm) ||
                                         c.Email.Contains(request.searchTerm) ||
                                         c.UserName.Contains(request.searchTerm));
            }

            if (!string.IsNullOrEmpty(request.sortFields))
            {
                var sortFieldArray = request.sortFields.Split(',');
                var sortOrderArray = request.sortOrders.Split(',');

                for (int i = 0; i < sortFieldArray.Length; i++)
                {
                    var sortField = sortFieldArray[i];
                    var sortOrder = sortOrderArray[i];

                    if (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
                    {
                        users = users.OrderByProperty(sortField);
                    }
                    else
                    {
                        users = users.OrderByDescendingProperty(sortField);
                    }
                }
            }

            filteredCount = users.Count();

            var userResult = users.ToList();

            for (int i = 0; i < userResult.Count; i++)
            {
                var userRoles = await userManager.GetRolesAsync(userResult[i]);

                var tuple = new Tuple<User, List<string>>(userResult[i], userRoles.ToList());

                usersWithRolesTuples.Add(tuple);
            }

            var allUsersWithRoles = mapper.Map<List<GetAllUsersQueryResponse>>(usersWithRolesTuples);

            GetAllUsersQueryTableResponse response = new(totalCount, filteredCount, allUsersWithRoles);

            return Result<GetAllUsersQueryTableResponse>.Succeed(response);
        }
    }
}
