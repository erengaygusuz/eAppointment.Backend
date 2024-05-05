using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.DeletePatient
{
    internal sealed class DeletePatientByIdCommandHandler(
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeletePatientByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await patientRepository.GetByExpressionAsync(p => p.Id == request.id, cancellationToken);

            if (patient == null)
            {
                return Result<string>.Failure("Patient not found");
            }

            patientRepository.Delete(patient);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Patient deleted successfully";
        }
    }
}
