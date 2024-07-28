using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById
{
    internal sealed class UpdateDoctorByIdCommandHandler(
        IMapper mapper,
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IValidator<UpdateDoctorByIdCommand> updateDoctorByIdCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<UpdateDoctorByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.UpdateDoctor.Others";

            var validationResult = await updateDoctorByIdCommandValidator.ValidateAsync(request);

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

            Doctor doctor = await doctorRepository.GetByExpressionAsync(x => x.UserId == request.id);

            doctor.DepartmentId = request.departmentId;

            doctorRepository.Update(doctor);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
