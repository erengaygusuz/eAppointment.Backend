using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    public sealed record DeleteAppointmentByIdCommand(Guid id) : IRequest<Result<string>>;
}
