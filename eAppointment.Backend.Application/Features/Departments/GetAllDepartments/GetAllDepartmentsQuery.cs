using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetAllDepartments
{
    public sealed record GetAllDepartmentsQuery() : IRequest<Result<List<GetAllDepartmentsQueryResponse>>>;
}
