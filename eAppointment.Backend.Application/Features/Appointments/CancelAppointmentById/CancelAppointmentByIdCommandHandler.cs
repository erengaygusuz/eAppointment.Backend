using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    internal sealed class CancelAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        IStringLocalizer<object> localization,
        ILogger<CancelAppointmentByIdCommandHandler> logger) : IRequestHandler<CancelAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CancelAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.CancelAppointment.Others";

            Appointment? appointment = await appointmentRepository.GetByExpressionAsync(p => p.Id == request.id, cancellationToken);

            if (appointment is null)
            {
                logger.LogError("User could not found");

                return Result<string>.Failure(localization[translatedMessagePath + "." + "NotFound"]);
            }

            if (appointment.Status == AppointmentStatus.SuccessfullyCompleted ||
               appointment.Status == AppointmentStatus.NotAttended)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotCanceled"]);
            }

            appointment.Status = AppointmentStatus.Cancelled;

            appointmentRepository.Update(appointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Appointment cancelled successfully");

            return localization[translatedMessagePath + "." + "SuccessfullyCancelled"].Value;
        }
    }
}
