﻿using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    internal sealed class UpdateAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<UpdateAppointmentByIdCommandHandler> logger) : IRequestHandler<UpdateAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.UpdateAppointment.Others";

            Appointment? appointment = await appointmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (appointment is null)
            {
                logger.LogError(localization[translatedMessagePath + "." + "NotFound"].Value);

                return Result<string>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            mapper.Map(request, appointment);

            appointmentRepository.Update(appointment);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);

            return Result<string>.Succeed((int)HttpStatusCode.OK, localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value);
        }
    }
}
