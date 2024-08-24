using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById
{
    public sealed class UpdatePatientCommandHandler(
        IPatientRepository patientRepository,
        IMapper mapper) : IRequestHandler<UpdatePatientProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdatePatientProfileByIdCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await patientRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (patient is null)
            {
                return Result<string>.Failure("Patient not found");
            }

            mapper.Map(request, patient);

            patientRepository.Update(patient);

            return "Patient updated successfully";
        }
    }
}
