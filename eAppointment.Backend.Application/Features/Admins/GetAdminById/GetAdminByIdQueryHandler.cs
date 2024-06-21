using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.GetUserById
{
    internal sealed class GetAdminByIdQueryHandler(
        UserManager<User> userManager) : IRequestHandler<GetAdminByIdQuery, Result<GetAdminByIdQueryResponse>>
    {
        public async Task<Result<GetAdminByIdQueryResponse>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.Where(x => x.Id == request.id).FirstOrDefaultAsync(cancellationToken);

            GetAdminByIdQueryResponse response = new GetAdminByIdQueryResponse()
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleId = user.RoleId
            };

            return response;
        }
    }
}
