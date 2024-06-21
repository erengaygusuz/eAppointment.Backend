using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetUserById
{
    internal sealed class GetUserByIdQueryHandler(
        UserManager<User> userManager,
        IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdQueryResponse>>
    {
        public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await userManager.Users.Where(x => x.Id == request.id).FirstOrDefaultAsync(cancellationToken); 

            var response = mapper.Map<GetUserByIdQueryResponse>(user);

            return response;
        }
    }
}
