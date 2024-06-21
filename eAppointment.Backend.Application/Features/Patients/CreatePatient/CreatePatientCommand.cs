using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.CreatePatient
{
    public sealed record CreatePatientCommand(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string userName,
        string identityNumber,
        Guid countyId,
        string fullAddress,
        string password,
        Guid roleId) : IRequest<Result<string>>;
}
