using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.CreateDoctor
{
    public sealed record CreateDoctorCommand(string firstName, string lastName, int department) : IRequest<Result<string>>;
}
