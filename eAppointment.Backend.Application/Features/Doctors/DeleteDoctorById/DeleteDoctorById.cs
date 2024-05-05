using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.DeleteDoctorById
{
    public sealed record DeleteDoctorByIdCommand(Guid id) : IRequest<Result<string>>;
}
