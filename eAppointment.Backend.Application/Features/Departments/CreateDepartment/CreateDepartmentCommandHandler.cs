﻿using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.CreateDepartment
{
    internal sealed class CreateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateDepartmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = mapper.Map<Department>(request);

            await departmentRepository.AddAsync(department, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);

            return "Department created successfully";
        }
    }
}
