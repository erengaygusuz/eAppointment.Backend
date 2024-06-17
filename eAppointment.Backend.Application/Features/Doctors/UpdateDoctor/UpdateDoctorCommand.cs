using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctor
{
    public sealed record UpdateDoctorCommand(
        Guid id, 
        string firstName,
        string lastName,
        Guid departmentId) : IRequest<Result<string>>;
}
