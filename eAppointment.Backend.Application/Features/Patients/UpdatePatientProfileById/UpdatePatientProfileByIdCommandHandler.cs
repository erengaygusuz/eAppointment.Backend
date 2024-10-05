using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById
{
    public sealed class UpdatePatientProfileByIdCommandHandler(
        IPatientRepository patientRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<UpdatePatientProfileByIdCommandHandler> logger) : IRequestHandler<UpdatePatientProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdatePatientProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Patients.UpdatePatientProfileById.Others";

            Patient? patient = await patientRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: p => p.Include(x => x.User),
               orderBy: null,
               cancellationToken);

            if (patient is null)
            {
                logger.LogError("User could not found");

                return Result<string>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            if (request.profilePhoto != null)
            {
                DotNetEnv.Env.Load();

                var userProfileImagesFolderPath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}";

                if (!Directory.Exists(userProfileImagesFolderPath))
                {
                    Directory.CreateDirectory(userProfileImagesFolderPath);
                }

                if (!string.IsNullOrEmpty(patient.User.ProfilePhotoPath))
                {
                    File.Delete(patient.User.ProfilePhotoPath);

                    patient.User.ProfilePhotoPath = "";
                }

                mapper.Map(request, patient.User);

                patient.User.ProfilePhotoPath = Guid.NewGuid() + ".png";

                using var stream = new FileStream($"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + patient.User.ProfilePhotoPath, FileMode.Create);

                await request.profilePhoto.CopyToAsync(stream);
            }

            else
            {
                mapper.Map(request, patient.User);
            }

            patientRepository.Update(patient);

            return Result<string>.Succeed((int)HttpStatusCode.OK, localization[translatedMessagePath + "." + "SuccessfullyUpdated"]);
        }
    }
}
