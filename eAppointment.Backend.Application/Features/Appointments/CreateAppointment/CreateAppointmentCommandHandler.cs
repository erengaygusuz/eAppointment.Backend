using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.CreateAppointment
{
    internal sealed class CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateAppointmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Patient patient = new();

            DateTime startDate = DateTime.ParseExact(request.startDate, "dd.MM.yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(request.endDate, "dd.MM.yyyy HH:mm", null);

            bool isAppointmentDateNotAvailable = await appointmentRepository
                    .AnyAsync(p => p.DoctorId == request.doctorId &&
                     ((p.StartDate < endDate && p.StartDate >= startDate) || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor
                     (p.EndDate > startDate && p.EndDate <= endDate) || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor
                     (p.StartDate >= startDate && p.EndDate <= endDate) || // Mevcut randevu, diğer randevu içinde tamamen
                     (p.StartDate <= startDate && p.EndDate >= endDate)), // Mevcut randevu, diğer randevuyu tamamen kapsıyor
                     cancellationToken);

            if (isAppointmentDateNotAvailable)
            {
                return Result<string>.Failure("Appointment date is not available");
            }

            var appointment = mapper.Map<Appointment>(request);

            await appointmentRepository.AddAsync(appointment, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Appointment created successfully";
        }
    }
}
