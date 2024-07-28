using AutoMapper;
using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.CreateDoctor
{
    internal sealed class CreateDoctorCommandHandler(
        UserManager<User> userManager,
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateDoctorCommand> createDoctorCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.CreateDoctor.Others";

            var validationResult = await createDoctorCommandValidator.ValidateAsync(request);

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

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Doctor");

            if (!roleResult.Succeeded)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "RoleCannotAdded"]);
            }

            Doctor doctor = new Doctor()
            {
                UserId = addedUser!.Id,
                DepartmentId = request.departmentId
            };

            await doctorRepository.AddAsync(doctor, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value;
        }
    }
}
