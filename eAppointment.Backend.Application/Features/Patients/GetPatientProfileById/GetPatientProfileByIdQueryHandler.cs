using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientProfileById
{
    internal sealed class GetPatientProfileByIdQueryHandler(
        IPatientRepository patientRepository,
        IMapper mapper) : IRequestHandler<GetPatientProfileByIdQuery, Result<GetPatientProfileByIdQueryResponse>>
    {
        public async Task<Result<GetPatientProfileByIdQueryResponse>> Handle(GetPatientProfileByIdQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = await patientRepository.GetAsync(
               expression: x => x.Id == request.id,
               trackChanges: false,
               include: x => x.Include(p => p.User).Include(c => c.County),
               orderBy: x => x.OrderBy(p => p.User.FirstName),
               cancellationToken);

            if (patient == null)
            {
                return Result<GetPatientProfileByIdQueryResponse>.Failure((int)HttpStatusCode.NotFound, "Patient not found");
            }

            var response = mapper.Map<GetPatientProfileByIdQueryResponse>(patient);

            DotNetEnv.Env.Load();

            var filePath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + patient.User.ProfilePhotoPath;

            if (File.Exists(filePath))
            {
                var fileBytes = File.ReadAllBytes(filePath);

                var base64Content = Convert.ToBase64String(fileBytes);

                response.ProfilePhotoContentType = "image/png";
                response.ProfilePhotoBase64Content = base64Content;
            }

            return Result<GetPatientProfileByIdQueryResponse>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
