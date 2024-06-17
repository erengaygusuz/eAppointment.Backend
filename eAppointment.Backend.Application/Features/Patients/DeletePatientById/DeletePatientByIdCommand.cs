using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.DeletePatientById
{
    public sealed record DeletePatientByIdCommand(Guid id) : IRequest<Result<string>>;
}
