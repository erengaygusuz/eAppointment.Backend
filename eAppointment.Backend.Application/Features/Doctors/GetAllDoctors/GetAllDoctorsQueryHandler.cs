using eAppointment.Backend.Application.Features.Doctors.GetAllDoctors;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctor
{
    internal sealed class GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorsQuery, Result<List<GetAllDoctorsQueryResponse>>>
    {
        public async Task<Result<List<GetAllDoctorsQueryResponse>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await doctorRepository.GetAll()
                .Include(u => u.User)
                .Include(d => d.Department)
                .OrderBy(p => p.Department.Name)
                .ThenBy(p => p.User.FirstName).ToListAsync(cancellationToken);

            List<GetAllDoctorsQueryResponse> response =
                doctors.Select(s =>
                    new GetAllDoctorsQueryResponse
                    (
                        id: s.Id,
                        firstName: s.User.FirstName,
                        lastName: s.User.LastName,
                        departmentName: s.Department.Name
                    )).ToList();

            return response;
        }
    }
}
