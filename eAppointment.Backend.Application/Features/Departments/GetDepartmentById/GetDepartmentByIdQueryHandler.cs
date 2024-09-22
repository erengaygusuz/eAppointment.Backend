using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed class GetAllDepartmentsQueryHandler(
        IDepartmentRepository departmentRepository) : IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdQueryResponse>>
    {
        public async Task<Result<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Department department = await departmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: d => d.Include(d => d.DepartmentTranslations),
               orderBy: null,
               cancellationToken);

            GetDepartmentByIdQueryResponse response = new GetDepartmentByIdQueryResponse
            (
                id: department.Id,
                name: department.DepartmentTranslations.FirstOrDefault(a => a.Language.Code == Thread.CurrentThread.CurrentCulture.Name).TranslationText
            );

            return response;
        }
    }
}
