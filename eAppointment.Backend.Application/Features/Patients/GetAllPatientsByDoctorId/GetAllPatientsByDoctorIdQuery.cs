using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    public sealed record GetAllPatientsByDoctorIdQuery(
        Guid doctorId) : IRequest<Result<List<GetAllPatientsByDoctorIdQueryResponse>>>;
}
