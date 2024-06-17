using AutoMapper;
using eAppointment.Backend.Application.Features.Users.CreateUser;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.CreateDepartment
{
    internal sealed class CreateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Department department = mapper.Map<Department>(request);

            await departmentRepository.AddAsync(department, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Department created successfully";
        }
    }
}
