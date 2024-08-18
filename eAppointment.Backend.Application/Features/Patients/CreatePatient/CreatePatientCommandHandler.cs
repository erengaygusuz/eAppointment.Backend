﻿using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.CreatePatient
{
    internal sealed class CreatePatientCommandHandler(
        UserManager<User> userManager,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<CreatePatientCommandHandler> logger) : IRequestHandler<CreatePatientCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Patients.CreatePatient.Others";

            User user = mapper.Map<User>(request);

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                logger.LogError(localization[translatedMessagePath + "." + "CannotCreated"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CannotCreated"]);
            }

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Patient");

            if (!roleResult.Succeeded)
            {
                logger.LogError(localization[translatedMessagePath + "." + "RoleCannotAdded"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "RoleCannotAdded"]);
            }

            Patient patient = new Patient()
            {
                UserId = addedUser!.Id,
                CountyId = request.countyId,
                FullAddress = request.fullAddress,
                IdentityNumber = request.identityNumber
            };

            await patientRepository.AddAsync(patient, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);

            return localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value;
        }
    }
}
