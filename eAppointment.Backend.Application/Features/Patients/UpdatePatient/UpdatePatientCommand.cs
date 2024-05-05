using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatient
{
    public sealed record UpdatePatientCommand(
        Guid id,
        string firstName,
        string lastName,
        string city,
        string town,
        string identityNumber,
        string fullAddress) : IRequest<Result<string>>;
}
