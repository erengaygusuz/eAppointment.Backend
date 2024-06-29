using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId
{
    public sealed record GetAllAppointmentsByPatientIdQuery(
        int patientId,
        int status) : IRequest<Result<List<GetAllAppointmentsByPatientIdQueryResponse>>>;
}
