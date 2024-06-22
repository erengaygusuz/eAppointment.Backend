using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById
{
    public sealed record UpdateDoctorProfileByIdCommand(
        int id, 
        string firstName,
        string lastName,
        string phoneNumber) : IRequest<Result<string>>;
}
