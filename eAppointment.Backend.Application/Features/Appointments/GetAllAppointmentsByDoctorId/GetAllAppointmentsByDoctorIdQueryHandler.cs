using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorId
{
    internal sealed class GetAllNotAttendedAppointmentsByDoctorIdQueryHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper) : IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository
                .Where(p => p.DoctorId == request.doctorId)
                .Include(p => p.Patient)
                .ThenInclude(u => u.User)
                .ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByDoctorIdQueryResponse>>(appointments);

            return response;
        }
    }
}
