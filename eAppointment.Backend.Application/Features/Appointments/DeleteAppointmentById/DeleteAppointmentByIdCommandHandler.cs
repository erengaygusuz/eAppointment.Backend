using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    internal sealed class DeleteAppointmentByIdCommandHandler (
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await appointmentRepository.GetByExpressionAsync(p => p.Id == request.id, cancellationToken);

            if(appointment is null)
            {
                return Result<string>.Failure("Appointment not found");
            }

            if(appointment.IsCompleted)
            {
                return Result<string>.Failure("You cannot delete a completed appointment");
            }

            appointmentRepository.Delete(appointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Appointment deleted successfully";
        }
    }
}
