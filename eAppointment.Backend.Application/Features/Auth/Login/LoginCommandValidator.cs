using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Features.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly IStringLocalizer<object> _localization;

        public LoginCommandValidator(IStringLocalizer<object> localization)
        {
            _localization = localization;

            var validationMessagePath = "Features.Auth.Login.ValidationMessages";

            RuleFor(x => x.userNameOrEmail)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "UserNameOrEmail.NotNull"])
                .MinimumLength(3).WithMessage(_localization[validationMessagePath + "." + "UserNameOrEmail.MinimumLength"])
                .MaximumLength(100).WithMessage(_localization[validationMessagePath + "." + "UserNameOrEmail.MaximumLength"])
                .Matches("^((?![ ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserNameOrEmail.NotUseSpaces"])
                .Matches("^((?![ğĞçÇşŞüÜöÖıİ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserNameOrEmail.NotUseTurkishCharacters"])
                .Matches("^((?![A-Z]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserNameOrEmail.NotUseUpperLetters"])
                .Matches("^((?![0-9]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserNameOrEmail.NotUseNumbers"]);

            RuleFor(x => x.password)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "Password.NotNull"])
                .MinimumLength(1).WithMessage(_localization[validationMessagePath + "." + "Password.MinimumLength"])
                .MaximumLength(5).WithMessage(_localization[validationMessagePath + "." + "Password.MaximumLength"]);
        }
    }
}
