using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler (
        UserManager<User> userManager,
        IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
    {
        public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await userManager.Users.OrderBy(u => u.FirstName).ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllUsersQueryResponse>>(users);

            return response;
        }
    }
}
