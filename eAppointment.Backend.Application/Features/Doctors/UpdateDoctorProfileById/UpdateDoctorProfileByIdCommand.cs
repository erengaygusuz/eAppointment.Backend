using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.AspNetCore.Http;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById
{
    public sealed record UpdateDoctorProfileByIdCommand(
        int id,
        string firstName,
        string lastName,
        string phoneNumber,
        IFormFile? profilePhoto) : IRequest<Result<string>>;
}
