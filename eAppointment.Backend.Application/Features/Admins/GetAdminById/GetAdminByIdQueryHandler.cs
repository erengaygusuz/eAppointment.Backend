using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Admins.GetAdminById
{
    internal sealed class GetAdminByIdQueryHandler(
        UserManager<User> userManager,
        IMapper mapper) : IRequestHandler<GetAdminByIdQuery, Result<GetAdminByIdQueryResponse>>
    {
        public async Task<Result<GetAdminByIdQueryResponse>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.Where(x => x.Id == request.id).FirstOrDefaultAsync(cancellationToken);

            var response = mapper.Map<GetAdminByIdQueryResponse>(user);

            return response;
        }
    }
}
