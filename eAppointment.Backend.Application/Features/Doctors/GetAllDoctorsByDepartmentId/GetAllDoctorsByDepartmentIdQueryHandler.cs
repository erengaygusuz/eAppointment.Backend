using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId
{
    internal sealed class GetAllDoctorsByDepartmentIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<GetAllDoctorsByDepartmentIdQuery, Result<List<GetAllDoctorsByDepartmentIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllDoctorsByDepartmentIdQueryResponse>>> Handle(GetAllDoctorsByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await doctorRepository.GetAllAsync(
               expression: p => p.DepartmentId == request.departmentId,
               trackChanges: false,
               include: x => x.Include(p => p.User),
               orderBy: x => x.OrderBy(p => p.User!.FirstName),
               cancellationToken);

            var response = mapper.Map<List<GetAllDoctorsByDepartmentIdQueryResponse>>(doctors);

            return response;
        }
    }
}
