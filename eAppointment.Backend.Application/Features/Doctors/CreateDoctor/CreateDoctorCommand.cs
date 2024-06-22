using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.CreateDoctor
{
    public sealed record CreateDoctorCommand(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string userName,
        string password,
        int roleId,
        int departmentId) : IRequest<Result<string>>;
}
