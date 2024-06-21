namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    public sealed record GetAllPatientsByDoctorIdQueryResponse(
        Guid id,
        string firstName,
        string lastName,
        string departmentName);
}
