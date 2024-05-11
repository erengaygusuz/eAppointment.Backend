using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatient
{
    public sealed class GetAllPatientsQueryHandler (IPatientRepository patientRepository): IRequestHandler<GetAllPatientsQuery, Result<List<Patient>>>
    {
        public async Task<Result<List<Patient>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            List<Patient> patients = await patientRepository.GetAll().OrderBy(p => p.FirstName).ToListAsync(cancellationToken);

            return patients;
        }
    }
}
