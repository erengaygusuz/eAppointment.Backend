using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    internal sealed class GetAllPatientsByDoctorIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<GetAllPatientsByDoctorIdQuery, Result<List<GetAllPatientsByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllPatientsByDoctorIdQueryResponse>>> Handle(GetAllPatientsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.doctorId,
               trackChanges: false,
               include: x => x.Include(a => a.Appointments!).ThenInclude(p => p.Patient),
               orderBy: x => x.OrderBy(p => p.User!.FirstName),
               cancellationToken);

            if (doctor == null)
            {
                return Result<List<GetAllPatientsByDoctorIdQueryResponse>>.Failure((int)HttpStatusCode.NotFound, "Doctor not found");
            }

            var response = mapper.Map<List<GetAllPatientsByDoctorIdQueryResponse>>(doctor);

            return new Result<List<GetAllPatientsByDoctorIdQueryResponse>>((int)HttpStatusCode.OK, response);
        }
    }
}
