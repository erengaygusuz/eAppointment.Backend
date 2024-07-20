using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorId
{
    public sealed record GetAllAppointmentsByDoctorIdQuery(
        int doctorId) : IRequest<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>;
}
