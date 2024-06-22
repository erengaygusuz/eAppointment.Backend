using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    public sealed record CancelAppointmentByIdCommand(
        int id) : IRequest<Result<string>>;
}
