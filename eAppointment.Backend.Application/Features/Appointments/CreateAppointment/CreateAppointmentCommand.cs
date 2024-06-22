using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.CreateAppointment
{
    public sealed record CreateAppointmentCommand(
        string startDate,
        string endDate,
        int patientId,
        int doctorId) : IRequest<Result<string>>;
}
