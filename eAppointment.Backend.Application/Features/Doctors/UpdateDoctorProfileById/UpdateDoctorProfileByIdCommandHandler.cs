using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById
{
    internal sealed class UpdateDoctorProfileByIdCommandHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<UpdateDoctorProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorProfileByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: d => d.Include(x => x.User),
               orderBy: null,
               cancellationToken);

            if (doctor is null)
            {
                return Result<string>.Failure((int)HttpStatusCode.NotFound, "Doctor not found");
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

            return new Result<string>((int)HttpStatusCode.OK, "Doctor updated successfully");
        }
    }
}
