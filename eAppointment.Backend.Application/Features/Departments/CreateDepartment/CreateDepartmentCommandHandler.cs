using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using System.Net;

namespace eAppointment.Backend.Application.Features.Departments.CreateDepartment
{
    internal sealed class CreateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository,
        IMapper mapper) : IRequestHandler<CreateDepartmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = mapper.Map<Department>(request);

            await departmentRepository.AddAsync(department, cancellationToken);

            return Result<string>.Succeed((int)HttpStatusCode.Created, "Department created successfully");
        }
    }
}
