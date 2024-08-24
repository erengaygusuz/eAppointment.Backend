using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById
{
    internal sealed class UpdateDoctorProfileByIdCommandHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper) : IRequestHandler<UpdateDoctorProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorProfileByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (doctor is null)
            {
                return Result<string>.Failure("Doctor not found");
            }

            mapper.Map(request, doctor);

            doctorRepository.Update(doctor);

            return "Doctor updated successfully";
        }
    }
}
