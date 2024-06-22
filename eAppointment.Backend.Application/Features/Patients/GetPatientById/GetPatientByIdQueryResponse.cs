namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed record GetPatientByIdQueryResponse(
        string firstName,
        string lastName,
        string identityNumber,
        string phoneNumber,
        string email,
        string userName,
        int countyId,
        int cityId,
        string? fullAddress);
}
