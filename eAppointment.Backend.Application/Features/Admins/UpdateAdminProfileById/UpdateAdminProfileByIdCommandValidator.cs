using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Features.Admins.UpdateAdminProfileById
{
    public class UpdateAdminProfileByIdCommandValidator : AbstractValidator<UpdateAdminProfileByIdCommand>
    {
        private readonly IStringLocalizer<object> _localization;

        public UpdateAdminProfileByIdCommandValidator(UserManager<User> userManager, IStringLocalizer<object> localization)
        {
            _localization = localization;

            var validationMessagePath = "Features.Admins.UpdateAdminProfile.ValidationMessages";

            RuleFor(x => x.firstName)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "FirstName.NotNull"])
                .MinimumLength(3).WithMessage(_localization[validationMessagePath + "." + "FirstName.MinimumLength"])
                .MaximumLength(50).WithMessage(_localization[validationMessagePath + "." + "FirstName.MaximumLength"])
                .Matches("^((?![0-9]).)*$").WithMessage(_localization[validationMessagePath + "." + "FirstName.NotUseNumbers"]);

            RuleFor(x => x.lastName)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "LastName.NotNull"])
                .MinimumLength(3).WithMessage(_localization[validationMessagePath + "." + "LastName.MinimumLength"])
                .MaximumLength(50).WithMessage(_localization[validationMessagePath + "." + "LastName.MaximumLength"])
                .Matches("^((?![0-9]).)*$").WithMessage(_localization[validationMessagePath + "." + "LastName.NotUseNumbers"]);

            RuleFor(x => x.phoneNumber)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotNull"])
                .Matches("((\\(\\d{3}\\) ?)|(\\d{3}-)) ?\\d{3}-\\d{4}").WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotValid"])
                .Matches("^((?![a-zA-Z]).)*$").WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotUseLetters"]);
        }
    }
}
