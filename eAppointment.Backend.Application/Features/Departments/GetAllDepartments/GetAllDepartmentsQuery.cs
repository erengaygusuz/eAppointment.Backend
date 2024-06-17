using eAppointment.Backend.Application.Features.Departments.GetAllDepartments;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetAllDepartment
{
    public sealed record GetAllDepartmentsQuery() : IRequest<Result<List<GetAllDepartmentsQueryResponse>>>;
}
