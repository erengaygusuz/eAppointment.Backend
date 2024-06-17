using eAppointment.Backend.Application.Features.Departments.GetAllDepartments;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetAllDepartment
{
    public sealed class GetAllDepartmentsQueryHandler (
        IDepartmentRepository departmentRepository): IRequestHandler<GetAllDepartmentsQuery, Result<List<GetAllDepartmentsQueryResponse>>>
    {
        public async Task<Result<List<GetAllDepartmentsQueryResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Department> departments = await departmentRepository.GetAll()
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);

            List<GetAllDepartmentsQueryResponse> response =
                departments.Select(s =>
                    new GetAllDepartmentsQueryResponse
                    (
                        s.Id,
                        s.Name
                    )).ToList();

            return response;
        }
    }
}
