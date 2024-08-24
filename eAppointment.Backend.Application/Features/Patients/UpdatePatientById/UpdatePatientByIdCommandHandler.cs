using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientById
{
    public sealed class UpdatePatientByIdCommandHandler(
        IPatientRepository patientRepository,
        IMapper mapper,
        UserManager<User> userManager,
        IStringLocalizer<object> localization,
        ILogger<UpdatePatientByIdCommandHandler> logger) : IRequestHandler<UpdatePatientByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdatePatientByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Patients.UpdatePatient.Others";

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

            Patient patient = await patientRepository.GetAsync(x => x.UserId == request.id);

            patient.IdentityNumber = request.identityNumber;
            patient.FullAddress = request.fullAddress;
            patient.CountyId = request.countyId;

            patientRepository.Update(patient);

            logger.LogInformation("Patient updated successfully");

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
