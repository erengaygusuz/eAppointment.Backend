using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed record GetPatientByIdQuery(
        Guid id) : IRequest<Result<GetPatientByIdQueryResponse>>;
}
