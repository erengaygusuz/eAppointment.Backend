using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientById
{
    public sealed record UpdatePatientByIdCommand(
        Guid id,
        string firstName,
        string lastName,
        string phoneNumber,
        string userName,
        string email,
        string identityNumber,
        Guid countyId,
        string fullAddress) : IRequest<Result<string>>;
}
