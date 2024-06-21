using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById
{
    internal sealed class UpdateDoctorProfileByIdCommandHandler (
        IDoctorRepository doctorRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper): IRequestHandler<UpdateDoctorProfileByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorProfileByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.id, cancellationToken);

            if (doctor is null)
            {
                return Result<string>.Failure("Doctor not found");
            }

            mapper.Map(request, doctor);

            doctorRepository.Update(doctor);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor updated successfully";
        }
    }
}
