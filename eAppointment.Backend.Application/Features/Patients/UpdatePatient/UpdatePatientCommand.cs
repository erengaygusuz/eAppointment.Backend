using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatient
{
    public sealed record UpdatePatientCommand(
        Guid id,
        string firstName,
        string lastName,
        Guid cityId,
        Guid countyId,
        string fullAddress) : IRequest<Result<string>>;
}
