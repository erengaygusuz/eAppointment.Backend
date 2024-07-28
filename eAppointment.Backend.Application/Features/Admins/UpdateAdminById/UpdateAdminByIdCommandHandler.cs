using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.UpdateAdminById
{
    internal sealed class UpdateAdminByIdCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IValidator<UpdateAdminByIdCommand> updateAdminByIdCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<UpdateAdminByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAdminByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Admins.UpdateAdmin.Others";

            var validationResult = await updateAdminByIdCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "UserCouldNotFound"]);
            }

            mapper.Map(request, user);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "UserCouldNotUpdated"]);
            }

            return localization[translatedMessagePath + "." + "UserSuccessfullyCreated"].Value;
        }
    }
}
