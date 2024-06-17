namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed record GetDepartmentByIdQueryResponse(
        Guid id,
        string name);
}
