using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointments
{
    public sealed record GetAllAppointmentsByDoctorIdQuery(Guid doctorId) : IRequest<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>;
}
