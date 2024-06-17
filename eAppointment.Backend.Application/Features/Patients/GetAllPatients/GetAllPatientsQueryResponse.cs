namespace eAppointment.Backend.Application.Features.Patients.GetAllPatients
{
    public sealed record GetAllPatientsQueryResponse(
        Guid id,
        string firstName,
        string lastName,
        string identityNumber,
        string cityName,
        string countyName,
        string? fullAddress);
}
