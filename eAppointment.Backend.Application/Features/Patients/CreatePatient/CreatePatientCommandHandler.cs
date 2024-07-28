using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.CreatePatient
{
    internal sealed class CreatePatientCommandHandler(
        UserManager<User> userManager,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreatePatientCommand> createPatientCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<CreatePatientCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Patients.CreatePatient.Others";

            var validationResult = await createPatientCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            User user = mapper.Map<User>(request);

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "CannotCreated"]);
            }

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Patient");

            if (!roleResult.Succeeded)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "RoleCannotAdded"]);
            }

            Patient patient = new Patient()
            {
                UserId = addedUser!.Id,
                CountyId = request.countyId,
                FullAddress = request.fullAddress,
                IdentityNumber = request.identityNumber
            };

            await patientRepository.AddAsync(patient, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value;
        }
    }
}
