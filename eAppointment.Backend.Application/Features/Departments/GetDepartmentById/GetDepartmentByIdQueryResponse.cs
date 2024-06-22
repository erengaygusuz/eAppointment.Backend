namespace eAppointment.Backend.Application.Features.Departments.GetDepartmentById
{
    public sealed record GetDepartmentByIdQueryResponse(
        int id,
        string name);
}
