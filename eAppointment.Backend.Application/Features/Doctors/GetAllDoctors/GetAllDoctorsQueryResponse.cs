namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctors
{
    public sealed record GetAllDoctorsQueryResponse(
        Guid id,
        string firstName,
        string lastName,
        string departmentName);
}
