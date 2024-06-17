﻿using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatient
{
    public sealed class UpdatePatientCommandHandler(
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdatePatientCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await patientRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.id, cancellationToken);

            if(patient is null)
            {
                return Result<string>.Failure("Patient not found");
            }

            mapper.Map(request, patient);

            patientRepository.Update(patient);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Patient updated successfully";
        }
    }
}
