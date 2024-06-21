namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed record GetPatientByIdQueryResponse(
        string firstName,
        string lastName,
        string identityNumber,
        string phoneNumber,
        string email,
        string userName,
        Guid countyId,
        Guid cityId,
        string? fullAddress);
}
