using AutoMapper;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    internal sealed class UpdateAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            DateTime startDate = DateTime.ParseExact(request.startDate, "dd.MM.yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(request.endDate, "dd.MM.yyyy HH:mm", null);

            Appointment? appointment = await appointmentRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.id, cancellationToken);

            if (appointment is null)
            {
                return Result<string>.Failure("Appointment not found");
            }

            bool isAppointmentDateNotAvailable = await appointmentRepository
                    .AnyAsync(p => p.DoctorId == appointment.DoctorId &&
                     ((p.StartDate < endDate && p.StartDate >= startDate) || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor
                     (p.EndDate > startDate && p.EndDate <= endDate) || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor
                     (p.StartDate >= startDate && p.EndDate <= endDate) || // Mevcut randevu, diğer randevu içinde tamamen
                     (p.StartDate <= startDate && p.EndDate >= endDate)), // Mevcut randevu, diğer randevuyu tamamen kapsıyor
                     cancellationToken);

            if (isAppointmentDateNotAvailable)
            {
                return Result<string>.Failure("Appointment date is not available");
            }

            var response = mapper.Map<UpdateAppointmentByIdCommand>(appointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Appointment updated successfully";
        }
    }
}
