using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed class GetAllDepartmentsQueryHandler(
        IDepartmentRepository departmentRepository) : IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdQueryResponse>>
    {
        public async Task<Result<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Department department = await departmentRepository
                .GetByExpressionAsync(x => x.Id == request.id, cancellationToken);

            GetDepartmentByIdQueryResponse response = new GetDepartmentByIdQueryResponse
            (
                id: department.Id,
                name: department.Name
            );

            return response;
        }
    }
}
