using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctor
{
    public sealed record UpdateDoctorCommand(
        Guid id, 
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string userName,
        Guid roleId) : IRequest<Result<string>>;
}
