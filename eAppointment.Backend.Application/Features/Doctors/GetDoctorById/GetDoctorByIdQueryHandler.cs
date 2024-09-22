using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    internal sealed class GetDoctorByIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<GetDoctorByIdQuery, Result<GetDoctorByIdQueryResponse>>
    {
        public async Task<Result<GetDoctorByIdQueryResponse>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: x => x.Include(u => u.User).Include(d => d.Department),
               orderBy: x => x.OrderBy(p => p.Department!.DepartmentKey).ThenBy(p => p.User!.FirstName),
               cancellationToken);

            var response = mapper.Map<GetDoctorByIdQueryResponse>(doctor);

            return response;
        }
    }
}
