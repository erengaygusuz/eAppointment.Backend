using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using System.Net;

namespace eAppointment.Backend.Application.Features.Departments.DeleteDepartmentById
{
    internal sealed class DeleteDepartmentByIdCommandHandler(
        IDepartmentRepository departmentRepository) : IRequestHandler<DeleteDepartmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteDepartmentByIdCommand request, CancellationToken cancellationToken)
        {
            Department? department = await departmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (department == null)
            {
                return Result<string>.Failure((int)HttpStatusCode.NotFound, "Department not found");
            }

            departmentRepository.Remove(department);

            return new Result<string>((int)HttpStatusCode.OK, "Department deleted successfully");
        }
    }
}
