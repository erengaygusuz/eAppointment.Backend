namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed record GetPatientByIdQueryResponse(
        Guid id,
        string firstName,
        string lastName,
        string identityNumber,
        string cityName,
        string countyName,
        string? fullAddress);
}
