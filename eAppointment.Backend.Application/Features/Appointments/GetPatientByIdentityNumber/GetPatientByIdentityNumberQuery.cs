using eAppointment.Backend.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetPatientByIdentityNumber
{
    public sealed record GetPatientByIdentityNumberQuery(
        string identityNumber) : IRequest<Result<Patient>>;
}
