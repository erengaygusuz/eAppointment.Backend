using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetAllDepartments
{
    public sealed class GetAllDepartmentsQueryHandler(
        IDepartmentRepository departmentRepository,
        IMapper mapper) : IRequestHandler<GetAllDepartmentsQuery, Result<List<GetAllDepartmentsQueryResponse>>>
    {
        public async Task<Result<List<GetAllDepartmentsQueryResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Department> departments = await departmentRepository.GetAll()
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllDepartmentsQueryResponse>>(departments);

            return response;
        }
    }
}
