using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.UpdateDepartment
{
    public sealed record UpdateDepartmentCommand(
        int id,
        string name) : IRequest<Result<string>>;
}
