using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId
{
    internal sealed class GetAllAppointmentsByPatientIdQueryHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper) : IRequestHandler<GetAllAppointmentsByPatientIdQuery, Result<List<GetAllAppointmentsByPatientIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByPatientIdQueryResponse>>> Handle(GetAllAppointmentsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository
                .Where(p => p.PatientId == request.patientId)
                .Include(p => p.Doctor)
                .ThenInclude(u => u.User)
                .Include(p => p.Doctor)
                .ThenInclude(u => u.Department)
                .ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByPatientIdQueryResponse>>(appointments);

            return response;
        }
    }
}
