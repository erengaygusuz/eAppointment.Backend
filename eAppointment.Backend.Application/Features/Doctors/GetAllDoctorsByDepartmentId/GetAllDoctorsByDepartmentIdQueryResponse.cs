namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId
{
    public sealed record GetAllDoctorsByDepartmentIdQueryResponse(
        int id,
        string firstName,
        string lastName,
        string departmentName);
}
