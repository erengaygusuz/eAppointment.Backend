using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.DeleteDepartmentById
{
    internal sealed class DeleteDepartmentByIdCommandHandler(
        IDepartmentRepository departmentRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepartmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteDepartmentByIdCommand request, CancellationToken cancellationToken)
        {
            Department? department = await departmentRepository.GetByExpressionAsync(p => p.Id == request.id, cancellationToken);

            if (department == null)
            {
                return Result<string>.Failure("Department not found");
            }

            departmentRepository.Delete(department);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Department deleted successfully";
        }
    }
}
