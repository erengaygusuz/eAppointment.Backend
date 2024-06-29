using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus
{
    internal sealed class GetAllAppointmentsByPatientIdAndByStatusQueryHandler (
        IAppointmentRepository appointmentRepository,
        IMapper mapper): IRequestHandler<GetAllAppointmentsByPatientIdAndByStatusQuery, Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>> Handle(GetAllAppointmentsByPatientIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository
                .Where(p => p.PatientId == request.patientId &&
                       p.Status == AppointmentStatus.FromValue(request.status))
                .Include(p => p.Patient)
                .ThenInclude(u => u.User)
                .ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>(appointments);

            return response;
        }
    }
}
