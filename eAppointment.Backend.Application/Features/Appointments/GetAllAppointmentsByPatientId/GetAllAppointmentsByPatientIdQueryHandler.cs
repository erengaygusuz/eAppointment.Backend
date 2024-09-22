using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId
{
    internal sealed class GetAllAppointmentsByPatientIdQueryHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper) : IRequestHandler<GetAllAppointmentsByPatientIdQuery, Result<List<GetAllAppointmentsByPatientIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByPatientIdQueryResponse>>> Handle(GetAllAppointmentsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository.GetAllAsync(
                expression: p => p.PatientId == request.patientId,
                trackChanges: false,
                include: x => x.Include(p => p.Doctor).ThenInclude(u => u.User)
                               .Include(p => p.Doctor).ThenInclude(u => u.Department)
                               .ThenInclude(x => x.DepartmentTranslations).ThenInclude(x => x.Language),
                orderBy: x => x.OrderBy(a => a.CreatedDate),
                cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByPatientIdQueryResponse>>(appointments);

            return response;
        }
    }
}
