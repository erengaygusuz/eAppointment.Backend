using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.CreateAppointment
{
    public sealed record CreateAppointmentCommand(
        string startDate,
        string endDate,
        int patientId,
        int doctorId) : IRequest<Result<string>>;
}
