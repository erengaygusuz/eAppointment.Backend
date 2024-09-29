using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus
{
    internal sealed class GetAllAppointmentsByPatientIdAndByStatusQueryHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper) : IRequestHandler<GetAllAppointmentsByPatientIdAndByStatusQuery, Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>> Handle(GetAllAppointmentsByPatientIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository.GetAllAsync(
                expression: p => p.PatientId == request.patientId &&
                            p.Status == AppointmentStatus.FromValue(request.status),
                trackChanges: false,
                include: x => x.Include(p => p.Patient).ThenInclude(u => u.User),
                orderBy: x => x.OrderBy(a => a.CreatedDate),
                cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>(appointments);

            return Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
