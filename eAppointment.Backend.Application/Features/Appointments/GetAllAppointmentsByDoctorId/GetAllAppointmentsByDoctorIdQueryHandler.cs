using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
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
            List<Appointment> appointments = await appointmentRepository.GetAllAsync(
               expression: p => p.DoctorId == request.doctorId,
               trackChanges: false,
               include: x => x.Include(p => p.Patient).ThenInclude(u => u.User),
               orderBy: x => x.OrderBy(a => a.CreatedDate),
               cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByDoctorIdQueryResponse>>(appointments);

            return response;
        }
    }
}
