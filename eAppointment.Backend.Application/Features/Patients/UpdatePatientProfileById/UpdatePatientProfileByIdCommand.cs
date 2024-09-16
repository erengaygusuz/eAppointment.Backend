using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.AspNetCore.Http;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById
{
    public sealed record UpdatePatientProfileByIdCommand(
        int id,
        string firstName,
        string lastName,
        string phoneNumber,
        int countyId,
        string fullAddress,
        IFormFile? profilePhoto) : IRequest<Result<string>>;
}
