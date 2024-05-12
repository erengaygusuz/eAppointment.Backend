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
        IPatientRepository patientRepository) : IRequestHandler<CreateAppointmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Patient patient = new();

            if(request.patientId is null)
            {
                patient = new()
                {
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    IdentityNumber = request.identityNumber,
                    City = request.city,
                    Town = request.town,
                    FullAddress = request.fullAddress
                };

                await patientRepository.AddAsync(patient, cancellationToken);
            }

            Appointment appointment = new()
            {
                DoctorId = request.doctorId,
                PatientId = request.patientId ?? patient.Id,
                StartDate = DateTime.ParseExact(request.startDate, "dd.MM.yyyy HH:mm", null),
                EndDate = DateTime.ParseExact(request.endDate, "dd.MM.yyyy HH:mm", null),
                IsCompleted = false
            };

            await appointmentRepository.AddAsync(appointment, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Appointment created successfully";
        }
    }
}
