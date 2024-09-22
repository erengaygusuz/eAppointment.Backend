using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace eAppointment.Backend.Application.Features.Departments.GetAllDepartments
{
    public sealed class GetAllDepartmentsQueryHandler(
        IDepartmentRepository departmentRepository,
        IMapper mapper) : IRequestHandler<GetAllDepartmentsQuery, Result<List<GetAllDepartmentsQueryResponse>>>
    {
        public async Task<Result<List<GetAllDepartmentsQueryResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Department> departments = await departmentRepository.GetAllAsync(
               expression: null,
               trackChanges: false,
               include: d => d.Include(d => d.DepartmentTranslations).ThenInclude(x => x.Language),
               orderBy: x => x.OrderBy(a => a.DepartmentKey),
               cancellationToken);

            var response = mapper.Map<List<GetAllDepartmentsQueryResponse>>(departments);

            return response;
        }
    }
}
