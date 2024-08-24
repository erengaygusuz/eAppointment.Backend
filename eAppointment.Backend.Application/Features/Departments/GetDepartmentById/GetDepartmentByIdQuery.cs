using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed record GetDepartmentByIdQuery(
        int id) : IRequest<Result<GetDepartmentByIdQueryResponse>>;
}
