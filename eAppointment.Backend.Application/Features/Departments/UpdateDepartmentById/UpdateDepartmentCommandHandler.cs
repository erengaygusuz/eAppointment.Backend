﻿using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Departments.UpdateDepartment
{
    public sealed class UpdateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository,
        IMapper mapper) : IRequestHandler<UpdateDepartmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department? department = await departmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (department is null)
            {
                return Result<string>.Failure("Department not found");
            }

            mapper.Map(request, department);

            departmentRepository.Update(department);

            return "Department updated successfully";
        }
    }
}
