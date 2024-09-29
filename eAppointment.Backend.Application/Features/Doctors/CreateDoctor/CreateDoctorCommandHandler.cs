﻿using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;
using System.Net;

namespace eAppointment.Backend.Application.Features.Doctors.CreateDoctor
{
    internal sealed class CreateDoctorCommandHandler(
        UserManager<User> userManager,
        IDoctorRepository doctorRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<CreateDoctorCommandHandler> logger) : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.CreateDoctor.Others";

            User user = mapper.Map<User>(request);

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                logger.LogError("User could not created");

                return Result<string>.Failure((int)HttpStatusCode.InternalServerError, localization[translatedMessagePath + "." + "CannotCreated"]);
            }

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            if (addedUser is null)
            {
                logger.LogError("User could not found");

                return Result<string>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            var roleResult = await userManager.AddToRoleAsync(addedUser!, Domain.Constants.Roles.Doctor);

            if (!roleResult.Succeeded)
            {
                logger.LogError("Role could not add to user");

                return Result<string>.Failure((int)HttpStatusCode.InternalServerError, localization[translatedMessagePath + "." + "RoleCannotAdded"]);
            }

            Doctor doctor = new Doctor()
            {
                UserId = addedUser!.Id,
                DepartmentId = request.departmentId
            };

            await doctorRepository.AddAsync(doctor, cancellationToken);

            logger.LogInformation("Doctor created successfully");

            return Result<string>.Succeed((int)HttpStatusCode.Created, localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);
        }
    }
}
