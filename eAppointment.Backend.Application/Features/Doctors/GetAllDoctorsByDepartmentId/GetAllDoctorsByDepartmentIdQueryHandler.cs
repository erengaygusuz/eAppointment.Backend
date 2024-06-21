using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId
{
    internal sealed class GetAllDoctorsByDepartmentIdQueryHandler(
        IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorsByDepartmentIdQuery, Result<List<GetAllDoctorsByDepartmentIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllDoctorsByDepartmentIdQueryResponse>>> Handle(GetAllDoctorsByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await doctorRepository
                .Where(p => p.DepartmentId == request.departmentId)
                .Include(u => u.User)
                .Include(d => d.Department)
                .OrderBy(p => p.User.FirstName).ToListAsync(cancellationToken);

            List<GetAllDoctorsByDepartmentIdQueryResponse> response =
                doctors.Select(s =>
                    new GetAllDoctorsByDepartmentIdQueryResponse
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
