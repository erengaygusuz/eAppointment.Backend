using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed record GetPatientByIdQuery(
        int id) : IRequest<Result<GetPatientByIdQueryResponse>>;
}
