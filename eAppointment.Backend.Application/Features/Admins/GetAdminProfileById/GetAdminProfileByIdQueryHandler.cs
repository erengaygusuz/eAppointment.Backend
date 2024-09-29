using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Admins.GetAdminProfileById
{
    internal sealed class GetAdminProfileByIdQueryHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<GetAdminProfileByIdQueryHandler> logger) : IRequestHandler<GetAdminProfileByIdQuery, Result<GetAdminProfileByIdQueryResponse>>
    {
        public async Task<Result<GetAdminProfileByIdQueryResponse>> Handle(GetAdminProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Admins.GetAdminProfileById.Others";

            User? user = await userManager.Users.Where(x => x.Id == request.id).FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                logger.LogError("User could not found");

                return Result<GetAdminProfileByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            var response = mapper.Map<GetAdminProfileByIdQueryResponse>(user);

            DotNetEnv.Env.Load();

            var filePath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + user.ProfilePhotoPath;

            if (File.Exists(filePath))
            {
                var fileBytes = File.ReadAllBytes(filePath);

                var base64Content = Convert.ToBase64String(fileBytes);

                response.ProfilePhotoContentType = "image/png";
                response.ProfilePhotoBase64Content = base64Content;
            }

            return Result<GetAdminProfileByIdQueryResponse>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
