using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointment
{
    public sealed record UpdateAppointmentCommand(
        Guid id,
        string startDate,
        string endDate) : IRequest<Result<string>>;
}
