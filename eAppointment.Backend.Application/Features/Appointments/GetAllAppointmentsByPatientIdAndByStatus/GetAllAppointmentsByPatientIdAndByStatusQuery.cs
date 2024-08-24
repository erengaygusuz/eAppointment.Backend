using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus
{
    public sealed record GetAllAppointmentsByPatientIdAndByStatusQuery(
        int patientId,
        int status) : IRequest<Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>>;
}
