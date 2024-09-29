using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Admins.GetAdminById
{
    internal sealed class GetAdminByIdQueryHandler(
        UserManager<User> userManager,
        IMapper mapper, 
        IStringLocalizer<object> localization,
        ILogger<GetAdminByIdQueryHandler> logger) : IRequestHandler<GetAdminByIdQuery, Result<GetAdminByIdQueryResponse>>
    {
        public async Task<Result<GetAdminByIdQueryResponse>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Admins.GetAdminById.Others";

            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                logger.LogError("User could not found");

                return Result<GetAdminByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            var response = mapper.Map<GetAdminByIdQueryResponse>(user);

            return new Result<GetAdminByIdQueryResponse>((int)HttpStatusCode.OK, response);
        }
    }
}
