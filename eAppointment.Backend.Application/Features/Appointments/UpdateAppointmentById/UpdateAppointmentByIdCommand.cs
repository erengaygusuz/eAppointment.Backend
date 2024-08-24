using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    public sealed record UpdateAppointmentByIdCommand(
        int id,
        string startDate,
        string endDate,
        int status) : IRequest<Result<string>>;
}
