using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus
{
    public sealed record GetAllAppointmentsByDoctorIdAndByStatusQuery(
        int doctorId,
        int status) : IRequest<Result<List<GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>>>;
}
