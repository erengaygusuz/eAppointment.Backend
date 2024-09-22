using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorProfileById
{
    internal sealed class GetDoctorProfileByIdQueryHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<GetDoctorProfileByIdQuery, Result<GetDoctorProfileByIdQueryResponse>>
    {
        public async Task<Result<GetDoctorProfileByIdQueryResponse>> Handle(GetDoctorProfileByIdQuery request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: x => x.Include(u => u.User).Include(d => d.Department),
               orderBy: x => x.OrderBy(p => p.Department!.DepartmentKey).ThenBy(p => p.User!.FirstName),
               cancellationToken);

            var response = mapper.Map<GetDoctorProfileByIdQueryResponse>(doctor);

            DotNetEnv.Env.Load();

            var filePath = $"{Environment.GetEnvironmentVariable("User__Profile__Image__Folder__Path")}" + doctor.User.ProfilePhotoPath;

            if (File.Exists(filePath))
            {
                var fileBytes = File.ReadAllBytes(filePath);

                var base64Content = Convert.ToBase64String(fileBytes);

                response.ProfilePhotoContentType = "image/png";
                response.ProfilePhotoBase64Content = base64Content;
            }

            return response;
        }
    }
}
