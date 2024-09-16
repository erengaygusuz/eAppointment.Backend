using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientProfileById
{
    public sealed record GetPatientProfileByIdQuery(
        int id) : IRequest<Result<GetPatientProfileByIdQueryResponse>>;
}
