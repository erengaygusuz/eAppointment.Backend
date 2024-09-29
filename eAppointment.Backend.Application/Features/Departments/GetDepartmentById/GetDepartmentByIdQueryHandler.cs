using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed class GetAllDepartmentsQueryHandler(
        IDepartmentRepository departmentRepository,
        IStringLocalizer<object> localization,
        ILogger<GetAllDepartmentsQueryHandler> logger) : IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdQueryResponse>>
    {
        public async Task<Result<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Departments.GetDepartmentById.Others";

            Department department = await departmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: d => d.Include(d => d.DepartmentTranslations),
               orderBy: null,
               cancellationToken);

            if (department is null)
            {
                logger.LogError("Department could not found");

                return Result<GetDepartmentByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            GetDepartmentByIdQueryResponse response = new GetDepartmentByIdQueryResponse
            (
                id: department.Id,
                name: department.DepartmentTranslations.FirstOrDefault(a => a.Language.Code == Thread.CurrentThread.CurrentCulture.Name).TranslationText
            );

            return Result<GetDepartmentByIdQueryResponse>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
