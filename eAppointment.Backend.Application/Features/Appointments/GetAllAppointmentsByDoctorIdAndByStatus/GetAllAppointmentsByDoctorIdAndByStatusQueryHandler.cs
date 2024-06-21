using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus
{
    internal sealed class GetAllNotAttendedAppointmentsByDoctorIdQueryHandler (
        IAppointmentRepository appointmentRepository): IRequestHandler<GetAllAppointmentsByDoctorIdAndByStatusQuery, Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository
                .Where(p => p.DoctorId == request.doctorId && 
                       p.Status == AppointmentStatus.FromValue(request.status))
                .Include(p => p.Patient)
                .ToListAsync(cancellationToken);

            List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse> response =
                appointments.Select(s => 
                    new GetAllAppointmentsByDoctorIdAndByStatusQueryResponse
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
