using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.CreateDepartment
{
    public sealed record CreateDepartmentCommand(
        string name) : IRequest<Result<string>>;
}

