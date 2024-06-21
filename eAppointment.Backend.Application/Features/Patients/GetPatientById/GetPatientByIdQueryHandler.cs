using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed class GetPatientByIdQueryHandler(IPatientRepository patientRepository) : IRequestHandler<GetPatientByIdQuery, Result<GetPatientByIdQueryResponse>>
    {
        public async Task<Result<GetPatientByIdQueryResponse>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = (await patientRepository
                .Where(x => x.Id == request.id)
                .Include(p => p.User)
                .OrderBy(p => p.User.FirstName).ToListAsync(cancellationToken)).FirstOrDefault();

            GetPatientByIdQueryResponse response = new GetPatientByIdQueryResponse
            (
                id: patient!.Id,
                firstName: patient.User.FirstName,
                lastName: patient.User.LastName,
                identityNumber: patient.IdentityNumber,
                cityName: patient.County.City.Name,
                countyName: patient.County.Name,
                fullAddress: patient.FullAddress
            );

            return response;
        }
    }
}
