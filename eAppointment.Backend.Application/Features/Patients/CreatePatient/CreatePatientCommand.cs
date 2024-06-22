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
        int countyId,
        string fullAddress,
        string password) : IRequest<Result<string>>;
}
