using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctor
{
    public sealed record UpdateDoctorCommand(
        Guid id, 
        string firstname,
        string lastname,
        int departmentValue) : IRequest<Result<string>>;
}
