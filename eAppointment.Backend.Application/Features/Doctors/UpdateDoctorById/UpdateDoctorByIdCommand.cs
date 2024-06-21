using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById
{
    public sealed record UpdateDoctorByIdCommand(
        Guid id, 
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string userName,
        Guid departmentId) : IRequest<Result<string>>;
}
