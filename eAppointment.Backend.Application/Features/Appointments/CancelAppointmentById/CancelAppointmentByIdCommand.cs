using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    public sealed record CancelAppointmentByIdCommand(Guid id) : IRequest<Result<string>>;
}
