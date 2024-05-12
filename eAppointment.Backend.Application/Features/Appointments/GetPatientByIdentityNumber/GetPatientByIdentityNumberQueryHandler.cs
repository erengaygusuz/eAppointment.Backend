using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetPatientByIdentityNumber
{
    internal sealed class GetPatientByIdentityNumberQueryHandler(IPatientRepository patientRepository) : IRequestHandler<GetPatientByIdentityNumberQuery, Result<Patient>>
    {
        public async Task<Result<Patient>> Handle(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = await patientRepository.GetByExpressionAsync(p => p.IdentityNumber == request.identityNumber, cancellationToken);

            return patient;
        }
    }
}
