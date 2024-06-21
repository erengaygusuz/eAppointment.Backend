using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    internal sealed class GetAllPatientsByDoctorIdQueryHandler(
        IDoctorRepository doctorRepository) : IRequestHandler<GetAllPatientsByDoctorIdQuery, Result<List<GetAllPatientsByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllPatientsByDoctorIdQueryResponse>>> Handle(GetAllPatientsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository
                .Where(d => d.Id == request.doctorId)
                .Include(a => a.Appointments!)
                .ThenInclude(p => p.Patient)
                .OrderBy(p => p.User.FirstName).FirstOrDefaultAsync(cancellationToken);

            List<GetAllPatientsByDoctorIdQueryResponse> response =
                doctor!.Appointments!.Select(s =>
                    new GetAllPatientsByDoctorIdQueryResponse
                    (
                        id: s.Id,
                        firstName: s.Patient.User.FirstName,
                        lastName: s.Patient.User.LastName,
                        departmentName: s.Doctor.Department.Name
                    )).ToList();

            return response;
        }
    }
}
