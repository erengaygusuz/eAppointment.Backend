using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllDoctorsByDepartment
{
    internal sealed class GetAllDoctorByDepartmentQueryHandler (IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorByDepartmentQuery, Result<List<Doctor>>>
    {
        public async Task<Result<List<Doctor>>> Handle(GetAllDoctorByDepartmentQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await doctorRepository
                .Where(p => p.Department == request.departmentValue)
                .OrderBy(p => p.FirstName).ToListAsync(cancellationToken);

            return doctors;
        }
    }
}
