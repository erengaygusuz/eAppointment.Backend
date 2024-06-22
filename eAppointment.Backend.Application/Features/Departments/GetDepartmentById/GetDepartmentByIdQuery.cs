using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed record GetDepartmentByIdQuery(
        int id) : IRequest<Result<GetDepartmentByIdQueryResponse>>;
}
