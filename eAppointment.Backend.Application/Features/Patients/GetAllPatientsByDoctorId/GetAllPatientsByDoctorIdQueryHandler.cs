using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    internal sealed class GetAllPatientsByDoctorIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<GetAllPatientsByDoctorIdQuery, Result<List<GetAllPatientsByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllPatientsByDoctorIdQueryResponse>>> Handle(GetAllPatientsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository
                .Where(d => d.Id == request.doctorId)
                .Include(a => a.Appointments!)
                .ThenInclude(p => p.Patient)
                .OrderBy(p => p.User.FirstName).FirstOrDefaultAsync(cancellationToken);

            var response = mapper.Map<List<GetAllPatientsByDoctorIdQueryResponse>>(doctor);

            return response;
        }
    }
}
