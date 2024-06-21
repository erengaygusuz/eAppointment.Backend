using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById
{
    public sealed record UpdatePatientProfileByIdCommand(
        Guid id,
        string firstName,
        string lastName,
        string phoneNumber,
        Guid countyId,
        string fullAddress) : IRequest<Result<string>>;
}
