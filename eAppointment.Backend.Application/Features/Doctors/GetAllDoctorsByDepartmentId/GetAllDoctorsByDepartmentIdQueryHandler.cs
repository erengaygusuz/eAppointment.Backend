using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId
{
    internal sealed class GetAllDoctorsByDepartmentIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<GetAllDoctorsByDepartmentIdQuery, Result<List<GetAllDoctorsByDepartmentIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllDoctorsByDepartmentIdQueryResponse>>> Handle(GetAllDoctorsByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await doctorRepository
                .Where(p => p.DepartmentId == request.departmentId)
                .Include(u => u.User)
                .Include(d => d.Department)
                .OrderBy(p => p.User.FirstName).ToListAsync(cancellationToken);

            var response = mapper.Map<List< GetAllDoctorsByDepartmentIdQueryResponse>>(doctors);

            return response;
        }
    }
}
