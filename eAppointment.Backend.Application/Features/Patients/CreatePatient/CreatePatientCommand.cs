using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.CreatePatient
{
    public sealed record CreatePatientCommand(
        string firstName,
        string lastName,
        string city,
        string town,
        string fullAddress,
        string identityNumber) : IRequest<Result<string>>;
}
