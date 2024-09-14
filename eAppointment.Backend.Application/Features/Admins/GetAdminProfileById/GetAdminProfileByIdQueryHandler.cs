using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Admins.GetAdminProfileById
{
    internal sealed class GetAdminProfileByIdQueryHandler(
        UserManager<User> userManager,
        IMapper mapper) : IRequestHandler<GetAdminProfileByIdQuery, Result<GetAdminProfileByIdQueryResponse>>
    {
        public async Task<Result<GetAdminProfileByIdQueryResponse>> Handle(GetAdminProfileByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.Where(x => x.Id == request.id).FirstOrDefaultAsync(cancellationToken);

            var response = mapper.Map<GetAdminProfileByIdQueryResponse>(user);

            return response;
        }
    }
}
