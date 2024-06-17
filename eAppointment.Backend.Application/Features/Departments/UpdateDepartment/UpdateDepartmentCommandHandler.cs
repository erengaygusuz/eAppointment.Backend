﻿using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.UpdateDepartment
{
    public sealed class UpdateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateDepartmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department? department = await departmentRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.id, cancellationToken);

            if(department is null)
            {
                return Result<string>.Failure("Department not found");
            }

            mapper.Map(request, department);

            departmentRepository.Update(department);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Department updated successfully";
        }
    }
}
