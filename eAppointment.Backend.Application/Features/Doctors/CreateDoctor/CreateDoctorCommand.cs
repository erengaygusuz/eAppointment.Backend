using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.CreateDoctor
{
    public sealed record CreateDoctorCommand(string firstName, string lastName, int departmentValue) : IRequest<Result<string>>;
}
