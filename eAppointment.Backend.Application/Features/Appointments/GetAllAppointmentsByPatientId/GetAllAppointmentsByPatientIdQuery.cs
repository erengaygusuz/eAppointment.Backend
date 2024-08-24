using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId
{
    public sealed record GetAllAppointmentsByPatientIdQuery(
        int patientId,
        int status) : IRequest<Result<List<GetAllAppointmentsByPatientIdQueryResponse>>>;
}
