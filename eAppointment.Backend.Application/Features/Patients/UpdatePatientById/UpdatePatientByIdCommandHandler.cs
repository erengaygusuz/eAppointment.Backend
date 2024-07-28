using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientById
{
    public sealed class UpdatePatientByIdCommandHandler(
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<User> userManager,
        IValidator<UpdatePatientByIdCommand> updatePatientByIdCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<UpdatePatientByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdatePatientByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Patients.UpdatePatient.Others";

            var validationResult = await updatePatientByIdCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            mapper.Map(request, user);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotUpdated"]);
            }

            Patient patient = await patientRepository.GetByExpressionAsync(x => x.UserId == request.id);

            patient.IdentityNumber = request.identityNumber;
            patient.FullAddress = request.fullAddress;
            patient.CountyId = request.countyId;

            patientRepository.Update(patient);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
