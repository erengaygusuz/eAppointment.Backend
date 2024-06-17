using eAppointment.Backend.Application.Features.Patients.GetAllPatients;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatient
{
    public sealed class GetAllPatientsQueryHandler (IPatientRepository patientRepository): IRequestHandler<GetAllPatientsQuery, Result<List<GetAllPatientsQueryResponse>>>
    {
        public async Task<Result<List<GetAllPatientsQueryResponse>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            List<Patient> patients = await patientRepository.GetAll()
                .Include(p => p.User)
                .OrderBy(p => p.User.FirstName).ToListAsync(cancellationToken);

            List<GetAllPatientsQueryResponse> response =
                patients.Select(s =>
                    new GetAllPatientsQueryResponse
                    (
                        id: s.Id,
                        firstName: s.User.FirstName,
                        lastName: s.User.LastName,
                        identityNumber: s.IdentityNumber,
                        cityName: s.County.City.Name,
                        countyName: s.County.Name,
                        fullAddress: s.FullAddress
                    )).ToList();

            return response;
        }
    }
}
