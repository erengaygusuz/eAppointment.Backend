namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId
{
    public sealed record GetAllDoctorsByDepartmentIdQueryResponse(
        Guid id,
        string firstName,
        string lastName,
        string departmentName);
}
