using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;
using eAppointment.Backend.Application.Features.Doctors.GetDoctorProfileById;
using System.Net;
using eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId;
using System.Numerics;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed class GetPatientByIdQueryHandler(
        IPatientRepository patientRepository,
        IMapper mapper) : IRequestHandler<GetPatientByIdQuery, Result<GetPatientByIdQueryResponse>>
    {
        public async Task<Result<GetPatientByIdQueryResponse>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = await patientRepository.GetAsync(
               expression: x => x.UserId == request.id,
               trackChanges: false,
               include: x => x.Include(p => p.User).Include(c => c.County),
               orderBy: x => x.OrderBy(p => p.User.FirstName),
               cancellationToken);

            if (patient == null)
            {
                return Result<GetPatientByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, "Patient not found");
            }

            var response = mapper.Map<GetPatientByIdQueryResponse>(patient);

            return new Result<GetPatientByIdQueryResponse>((int)HttpStatusCode.OK, response);
        }
    }
}
