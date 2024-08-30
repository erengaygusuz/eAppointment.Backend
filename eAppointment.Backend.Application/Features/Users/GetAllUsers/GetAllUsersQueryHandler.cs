using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Extensions;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler(
        UserManager<User> userManager,
        IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<GetAllUsersQueryTableResponse>>
    {
        public async Task<Result<GetAllUsersQueryTableResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersWithRolesTuples = new List<Tuple<User, List<string>>>();

            LazyLoadEvent2 loadEvent = new() 
            {
                first = request.first,
                rows = request.rows,
                sortField = request.sortField,
                sortOrder = request.sortOrder,
                multiSortMeta = request.multiSortMeta,
                filters = request.filters,
                globalFilter = request.globalFilter
            };

            var totalCount = userManager.Users.Count();

            var users = userManager.Users
                .LazyFilters2(loadEvent)
                .LazyOrderBy2(loadEvent)
                .LazyGlobalFilter(loadEvent, x => x.FirstName, x => x.Email, x => x.UserName)
                .LazySkipTake2(loadEvent)
                .ToList();

            for (int i = 0; i < users.Count; i++)
            {
                var userRoles = await userManager.GetRolesAsync(users[i]);

                var tuple = new Tuple<User, List<string>>(users[i], userRoles.ToList());

                usersWithRolesTuples.Add(tuple);
            }

            var allUsersWithRoles = mapper.Map<List<GetAllUsersQueryResponse>>(usersWithRolesTuples);

            GetAllUsersQueryTableResponse response = new(totalCount, allUsersWithRoles);

            return Result<GetAllUsersQueryTableResponse>.Succeed(response);
        }
    }
}
