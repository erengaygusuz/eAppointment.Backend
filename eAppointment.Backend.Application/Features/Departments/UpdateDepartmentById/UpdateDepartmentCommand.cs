using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Departments.UpdateDepartment
{
    public sealed record UpdateDepartmentCommand(
        int id,
        string name) : IRequest<Result<string>>;
}
