using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Net;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById
{
    internal sealed class UpdateDoctorProfileByIdCommandHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<UpdateDoctorByIdCommandHandler> logger) : IRequestHandler<UpdateDoctorProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.UpdateDoctorProfileById.Others";

            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: d => d.Include(x => x.User),
               orderBy: null,
               cancellationToken);

            if (doctor is null)
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

                if (!string.IsNullOrEmpty(doctor.User.ProfilePhotoPath))
                {
                    File.Delete(doctor.User.ProfilePhotoPath);

                    doctor.User.ProfilePhotoPath = "";
                }

                mapper.Map(request, doctor.User);

                doctor.User.ProfilePhotoPath = Guid.NewGuid() + ".png";

                using var stream = new FileStream($"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + doctor.User.ProfilePhotoPath, FileMode.Create);

                await request.profilePhoto.CopyToAsync(stream);
            }

            else
            {
                mapper.Map(request, doctor.User);
            }

            doctorRepository.Update(doctor);

            return Result<string>.Succeed((int)HttpStatusCode.OK, localization[translatedMessagePath + "." + "SuccessfullyUpdated"]);
        }
    }
}
