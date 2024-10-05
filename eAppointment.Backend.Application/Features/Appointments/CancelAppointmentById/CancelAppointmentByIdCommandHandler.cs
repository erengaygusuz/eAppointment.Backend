using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;
using System.Net;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    internal sealed class CancelAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IStringLocalizer<object> localization,
        ILogger<CancelAppointmentByIdCommandHandler> logger) : IRequestHandler<CancelAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CancelAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.CancelAppointment.Others";

            Appointment? appointment = await appointmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (appointment is null)
            {
                logger.LogError("User could not found");

                return Result<string>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            if (appointment.Status == AppointmentStatus.SuccessfullyCompleted ||
               appointment.Status == AppointmentStatus.NotAttended)
            {
                return Result<string>.Failure((int)HttpStatusCode.InternalServerError, localization[translatedMessagePath + "." + "CouldNotCanceled"]);
            }

            appointment.Status = AppointmentStatus.Cancelled;

            appointmentRepository.Update(appointment);

            logger.LogInformation("Appointment cancelled successfully");

            return Result<string>.Succeed((int)HttpStatusCode.OK, localization[translatedMessagePath + "." + "SuccessfullyCancelled"].Value);
        }
    }
}
