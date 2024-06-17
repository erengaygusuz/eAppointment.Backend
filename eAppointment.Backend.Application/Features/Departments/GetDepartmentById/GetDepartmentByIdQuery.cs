using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed record GetDepartmentByIdQuery(
        Guid id) : IRequest<Result<GetDepartmentByIdQueryResponse>>;
}
