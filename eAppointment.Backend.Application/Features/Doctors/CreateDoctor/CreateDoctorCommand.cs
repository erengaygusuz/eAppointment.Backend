using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.CreateDoctor
{
    public sealed record CreateDoctorCommand(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string userName,
        string password,
        int departmentId) : IRequest<Result<string>>;
}
