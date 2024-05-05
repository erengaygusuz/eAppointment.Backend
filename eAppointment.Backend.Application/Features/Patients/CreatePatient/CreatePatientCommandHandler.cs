using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.CreatePatient
{
    internal sealed class CreatePatientCommandHandler (
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
        ) : IRequestHandler<CreatePatientCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            if(patientRepository.Any(p => p.IdentityNumber == request.identityNumber))
            {
                return Result<string>.Failure("This identity number already is in use");
            }

            Patient patient = mapper.Map<Patient>(request);

            await patientRepository.AddAsync(patient, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Patient created successfully";
        }
    }
}
