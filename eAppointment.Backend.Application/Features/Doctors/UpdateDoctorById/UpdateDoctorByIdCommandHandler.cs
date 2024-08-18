using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById
{
    internal sealed class UpdateDoctorByIdCommandHandler(
        IMapper mapper,
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IStringLocalizer<object> localization,
        ILogger<UpdateDoctorByIdCommandHandler> logger) : IRequestHandler<UpdateDoctorByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Doctors.UpdateDoctor.Others";

            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                logger.LogError(localization[translatedMessagePath + "." + "CouldNotFound"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotFound"]);
            }

            mapper.Map(request, user);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                logger.LogError(localization[translatedMessagePath + "." + "CouldNotUpdated"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "CouldNotUpdated"]);
            }

            Doctor doctor = await doctorRepository.GetByExpressionAsync(x => x.UserId == request.id);

            doctor.DepartmentId = request.departmentId;

            doctorRepository.Update(doctor);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value);

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
