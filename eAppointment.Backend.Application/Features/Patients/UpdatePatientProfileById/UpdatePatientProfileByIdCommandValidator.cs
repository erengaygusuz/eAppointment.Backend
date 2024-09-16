using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById
{
    public class UpdatePatientProfileByIdCommandValidator : AbstractValidator<UpdatePatientProfileByIdCommand>
    {
        private readonly IStringLocalizer<object> _localization;

        public UpdatePatientProfileByIdCommandValidator(IStringLocalizer<object> localization)
        {
            _localization = localization;

            var validationMessagePath = "Features.Patients.UpdatePatientProfile.ValidationMessages";

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

            RuleFor(x => x.countyId)
                .GreaterThan(0).WithMessage(_localization[validationMessagePath + "." + "CountyId.GreaterThanZero"]);

            RuleFor(x => x.fullAddress)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "FullAddress.NotNull"])
                .MinimumLength(50).WithMessage(_localization[validationMessagePath + "." + "FullAddress.MinimumLength"])
                .MaximumLength(500).WithMessage(_localization[validationMessagePath + "." + "FullAddress.MaximumLength"]);
        }
    }
}
