using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId
{
    public sealed record GetAllDoctorsByDepartmentIdQuery(Guid departmentId) : IRequest<Result<List<GetAllDoctorsByDepartmentIdQueryResponse>>>;
}
