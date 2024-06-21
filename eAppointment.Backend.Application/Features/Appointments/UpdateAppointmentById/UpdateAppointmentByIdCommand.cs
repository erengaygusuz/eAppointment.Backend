using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    public sealed record UpdateAppointmentByIdCommand(
        Guid id,
        string startDate,
        string endDate,
        int status) : IRequest<Result<string>>;
}
