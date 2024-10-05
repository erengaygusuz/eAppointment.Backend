using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    internal sealed class GetAllPatientsByDoctorIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<GetAllPatientsByDoctorIdQueryHandler> logger) : IRequestHandler<GetAllPatientsByDoctorIdQuery, Result<List<GetAllPatientsByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllPatientsByDoctorIdQueryResponse>>> Handle(GetAllPatientsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Patients.GetAllPatientsByDoctorId.Others";

            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.doctorId,
               trackChanges: false,
               include: x => x.Include(a => a.Appointments!).ThenInclude(p => p.Patient),
               orderBy: x => x.OrderBy(p => p.User!.FirstName),
               cancellationToken);

            if (doctor == null)
            {
                logger.LogError("User could not created");

                return Result<List<GetAllPatientsByDoctorIdQueryResponse>>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            var response = mapper.Map<List<GetAllPatientsByDoctorIdQueryResponse>>(doctor);

            return Result<List<GetAllPatientsByDoctorIdQueryResponse>>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
