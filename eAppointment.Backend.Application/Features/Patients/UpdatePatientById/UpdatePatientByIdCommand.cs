using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientById
{
    public sealed record UpdatePatientByIdCommand(
        int id,
        string firstName,
        string lastName,
        string phoneNumber,
        string userName,
        string email,
        string identityNumber,
        int countyId,
        string fullAddress) : IRequest<Result<string>>;
}
