using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus
{
    internal sealed class GetAllNotAttendedAppointmentsByDoctorIdQueryHandler (
        IAppointmentRepository appointmentRepository,
        IMapper mapper): IRequestHandler<GetAllAppointmentsByDoctorIdAndByStatusQuery, Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository
                .Where(p => p.DoctorId == request.doctorId && 
                       p.Status == AppointmentStatus.FromValue(request.status))
                .Include(p => p.Patient)
                .ThenInclude(u => u.User)
                .ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>(appointments);

            return response;
        }
    }
}
