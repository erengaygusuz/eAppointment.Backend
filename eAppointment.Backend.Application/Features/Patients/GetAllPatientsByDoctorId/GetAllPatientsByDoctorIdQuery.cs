using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    public sealed record GetAllPatientsByDoctorIdQuery(
        int doctorId) : IRequest<Result<List<GetAllPatientsByDoctorIdQueryResponse>>>;
}
