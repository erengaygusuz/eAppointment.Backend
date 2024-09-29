using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;
using System.Net;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus
{
    internal sealed class GetAllNotAttendedAppointmentsByDoctorIdQueryHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper) : IRequestHandler<GetAllAppointmentsByDoctorIdAndByStatusQuery, Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository.GetAllAsync(
                expression: p => p.DoctorId == request.doctorId &&
                            p.Status == AppointmentStatus.FromValue(request.status),
                trackChanges: false,
                include: x => x.Include(p => p.Patient).ThenInclude(u => u.User),
                orderBy: x => x.OrderBy(a => a.CreatedDate), 
                cancellationToken);

            var response = mapper.Map<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>(appointments);

            return Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
