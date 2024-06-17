using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.DeleteDepartmentById
{
    public sealed record DeleteDepartmentByIdCommand(Guid id) : IRequest<Result<string>>;
}
