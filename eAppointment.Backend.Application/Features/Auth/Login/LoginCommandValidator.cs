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

            RuleFor(x => x.userName)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "UserName.NotNull"])
                .MinimumLength(3).WithMessage(_localization[validationMessagePath + "." + "UserName.MinimumLength"])
                .MaximumLength(100).WithMessage(_localization[validationMessagePath + "." + "UserName.MaximumLength"])
                .Matches("^((?![ ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseSpaces"])
                .Matches("^((?![ğĞçÇşŞüÜöÖıİ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseTurkishCharacters"])
                .Matches("^((?![A-Z]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseUpperLetters"])
                .Matches("^((?![0-9]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseNumbers"]);

            RuleFor(x => x.password)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "Password.NotNull"])
                .MinimumLength(1).WithMessage(_localization[validationMessagePath + "." + "Password.MinimumLength"])
                .MaximumLength(5).WithMessage(_localization[validationMessagePath + "." + "Password.MaximumLength"]);
        }
    }
}
