using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById
{
    internal sealed class UpdateDoctorByIdCommandHandler(
        IMapper mapper,
        IDoctorRepository doctorRepository,
        UserManager<User> userManager,
        IStringLocalizer<object> localization,
        ILogger<UpdateDoctorByIdCommandHandler> logger) : IRequestHandler<UpdateDoctorByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.UpdateDoctor.Others";

            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                logger.LogError("User could not found");

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            mapper.Map(request, user);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                logger.LogError("User could not updated");

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotUpdated"]);
            }

            Doctor doctor = await doctorRepository.GetAsync(x => x.UserId == request.id);

            doctor.DepartmentId = request.departmentId;

            doctorRepository.Update(doctor);

            logger.LogInformation("Doctor updated successfully");

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
