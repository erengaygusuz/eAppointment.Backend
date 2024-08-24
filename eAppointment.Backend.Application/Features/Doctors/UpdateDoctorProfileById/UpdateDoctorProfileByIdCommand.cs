using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById
{
    public sealed record UpdateDoctorProfileByIdCommand(
        int id,
        string firstName,
        string lastName,
        string phoneNumber) : IRequest<Result<string>>;
}
