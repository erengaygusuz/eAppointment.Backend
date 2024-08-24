using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Departments.CreateDepartment
{
    public sealed record CreateDepartmentCommand(
        string name) : IRequest<Result<string>>;
}

