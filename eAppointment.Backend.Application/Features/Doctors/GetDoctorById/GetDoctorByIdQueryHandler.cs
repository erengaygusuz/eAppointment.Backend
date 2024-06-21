using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    internal sealed class GetDoctorByIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<GetDoctorByIdQuery, Result<GetDoctorByIdQueryResponse>>
    {
        public async Task<Result<GetDoctorByIdQueryResponse>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = (await doctorRepository
                .Where(x => x.Id == request.id)
                .Include(u => u.User)
                .Include(d => d.Department)
                .OrderBy(p => p.Department.Name)
                .ThenBy(p => p.User.FirstName).ToListAsync(cancellationToken)).FirstOrDefault();

            var response = mapper.Map<GetDoctorByIdQueryResponse>(doctor);

            return response;
        }
    }
}
