using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorProfileById
{
    internal sealed class GetDoctorProfileByIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<GetDoctorProfileByIdQueryHandler> logger) : IRequestHandler<GetDoctorProfileByIdQuery, Result<GetDoctorProfileByIdQueryResponse>>
    {
        public async Task<Result<GetDoctorProfileByIdQueryResponse>> Handle(GetDoctorProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.GetDoctorProfileById.Others";

            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: x => x.Include(u => u.User).Include(d => d.Department),
               orderBy: x => x.OrderBy(p => p.Department!.DepartmentKey).ThenBy(p => p.User!.FirstName),
               cancellationToken);

            if (doctor == null)
            {
                logger.LogError("User could not found");

                return Result<GetDoctorProfileByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            var response = mapper.Map<GetDoctorProfileByIdQueryResponse>(doctor);

            DotNetEnv.Env.Load();

            var filePath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + doctor.User.ProfilePhotoPath;

            if (!string.IsNullOrEmpty(doctor.User.ProfilePhotoPath))
            {
                if (File.Exists(filePath))
                {
                    var fileBytes = File.ReadAllBytes(filePath);

                    var base64Content = Convert.ToBase64String(fileBytes);

                    response.ProfilePhotoContentType = "image/png";
                    response.ProfilePhotoBase64Content = base64Content;
                }
                else
                {
                    logger.LogError("Profile photo could not found");

                    return Result<GetDoctorProfileByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "ProfilePhotoCouldNotFound"]);
                }
            }

            return Result<GetDoctorProfileByIdQueryResponse>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
