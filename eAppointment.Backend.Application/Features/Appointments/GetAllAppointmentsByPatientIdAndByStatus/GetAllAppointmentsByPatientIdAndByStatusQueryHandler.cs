using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus
{
    internal sealed class GetAllAppointmentsByPatientIdAndByStatusQueryHandler (
        IAppointmentRepository appointmentRepository): IRequestHandler<GetAllAppointmentsByPatientIdAndByStatusQuery, Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>> Handle(GetAllAppointmentsByPatientIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository
                .Where(p => p.PatientId == request.patientId &&
                       p.Status == AppointmentStatus.FromValue(request.status))
                .Include(p => p.Patient)
                .ToListAsync(cancellationToken);

            List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse> response =
                appointments.Select(s => 
                    new GetAllAppointmentsByPatientIdAndByStatusQueryResponse
                    (
                        s.Id,
                        s.StartDate,
                        s.EndDate,
                        s.Patient.User.FirstName + " " + s.Patient.User.LastName)
                    ).ToList();

            return response;
        }
    }
}
