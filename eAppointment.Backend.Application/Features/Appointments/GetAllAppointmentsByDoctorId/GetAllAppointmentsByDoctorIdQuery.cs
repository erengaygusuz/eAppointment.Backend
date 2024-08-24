using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorId
{
    public sealed record GetAllAppointmentsByDoctorIdQuery(
        int doctorId) : IRequest<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>;
}
