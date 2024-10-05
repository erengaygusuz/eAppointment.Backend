using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientProfileById
{
    internal sealed class GetPatientProfileByIdQueryHandler(
        IPatientRepository patientRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<GetPatientProfileByIdQueryHandler> logger) : IRequestHandler<GetPatientProfileByIdQuery, Result<GetPatientProfileByIdQueryResponse>>
    {
        public async Task<Result<GetPatientProfileByIdQueryResponse>> Handle(GetPatientProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Patients.GetPatientProfileById.Others";

            Patient? patient = await patientRepository.GetAsync(
               expression: x => x.Id == request.id,
               trackChanges: false,
               include: x => x.Include(p => p.User).Include(c => c.County),
               orderBy: x => x.OrderBy(p => p.User.FirstName),
               cancellationToken);

            if (patient == null)
            {
                logger.LogError("User could not found");

                return Result<GetPatientProfileByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            var response = mapper.Map<GetPatientProfileByIdQueryResponse>(patient);

            DotNetEnv.Env.Load();

            var filePath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + patient.User.ProfilePhotoPath;

            if (!string.IsNullOrEmpty(patient.User.ProfilePhotoPath))
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

                    return Result<GetPatientProfileByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "ProfilePhotoCouldNotFound"]);
                }
            }

            return Result<GetPatientProfileByIdQueryResponse>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
