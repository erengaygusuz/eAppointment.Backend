using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    internal sealed class CancelAppointmentByIdCommandHandler (
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CancelAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CancelAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await appointmentRepository.GetByExpressionAsync(p => p.Id == request.id, cancellationToken);

            if(appointment is null)
            {
                return Result<string>.Failure("Appointment not found");
            }

            if(appointment.Status == AppointmentStatus.SuccessfullyCompleted || 
               appointment.Status == AppointmentStatus.NotAttend)
            {
                return Result<string>.Failure("You cannot cancel a completed appointment");
            }

            appointment.Status = AppointmentStatus.Canceled;

            appointmentRepository.Update(appointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Appointment canceled successfully";
        }
    }
}
