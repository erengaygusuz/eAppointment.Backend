using AutoMapper;
using eAppointment.Backend.Application.Features.Doctors.CreateDoctor;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    internal sealed class GetDoctorByIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<CreateDoctorCommandHandler> logger) : IRequestHandler<GetDoctorByIdQuery, Result<GetDoctorByIdQueryResponse>>
    {
        public async Task<Result<GetDoctorByIdQueryResponse>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.GetDoctorById.Others";

            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.UserId == request.id,
               trackChanges: false,
               include: x => x.Include(u => u.User).Include(d => d.Department),
               orderBy: x => x.OrderBy(p => p.Department!.DepartmentKey).ThenBy(p => p.User!.FirstName),
               cancellationToken);

            if (doctor == null)
            {
                logger.LogError("User could not found");

                return Result<GetDoctorByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            var response = mapper.Map<GetDoctorByIdQueryResponse>(doctor);

            return Result<GetDoctorByIdQueryResponse>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
