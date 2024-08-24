using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Departments.DeleteDepartmentById
{
    public sealed record DeleteDepartmentByIdCommand(
        int id) : IRequest<Result<string>>;
}
