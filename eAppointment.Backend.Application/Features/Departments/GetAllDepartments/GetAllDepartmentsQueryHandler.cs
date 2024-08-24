using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;

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
               include: null,
               orderBy: x => x.OrderBy(a => a.Name),
               cancellationToken);

            var response = mapper.Map<List<GetAllDepartmentsQueryResponse>>(departments);

            return response;
        }
    }
}
