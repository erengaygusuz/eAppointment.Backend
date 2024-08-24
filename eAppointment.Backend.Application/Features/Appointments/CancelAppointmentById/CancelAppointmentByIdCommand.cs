using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById
{
    public sealed record CancelAppointmentByIdCommand(
        int id) : IRequest<Result<string>>;
}
