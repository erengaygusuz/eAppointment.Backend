using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus
{
    public sealed record GetAllAppointmentsByPatientIdAndByStatusQuery(
        Guid patientId,
        int status) : IRequest<Result<List<GetAllAppointmentsByPatientIdAndByStatusQueryResponse>>>;
}
