using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentStatusById
{
    public sealed record UpdateAppointmentStatusByIdCommand(
        int id,
        int status) : IRequest<Result<string>>;
}
