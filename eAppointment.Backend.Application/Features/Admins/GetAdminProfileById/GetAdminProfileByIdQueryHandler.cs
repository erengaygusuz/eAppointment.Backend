using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            DotNetEnv.Env.Load();

            var filePath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + user.ProfilePhotoPath;

            if (File.Exists(filePath))
            {
                var fileBytes = File.ReadAllBytes(filePath);

                var base64Content = Convert.ToBase64String(fileBytes);

                response.ProfilePhotoContentType = "image/png";
                response.ProfilePhotoBase64Content = base64Content;
            }

            return response;
        }
    }
}
