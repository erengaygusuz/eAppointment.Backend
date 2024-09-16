using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById
{
    public sealed class UpdatePatientCommandHandler(
        IPatientRepository patientRepository,
        IMapper mapper) : IRequestHandler<UpdatePatientProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdatePatientProfileByIdCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await patientRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: p => p.Include(x => x.User),
               orderBy: null,
               cancellationToken);

            if (patient is null)
            {
                return Result<string>.Failure("Patient not found");
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

                mapper.Map(request, patient);

                patient.User.ProfilePhotoPath = Guid.NewGuid() + ".png";

                using var stream = new FileStream($"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + patient.User.ProfilePhotoPath, FileMode.Create);

                await request.profilePhoto.CopyToAsync(stream);
            }

            else
            {
                mapper.Map(request, patient);
            }

            patientRepository.Update(patient);

            return "Patient updated successfully";
        }
    }
}
