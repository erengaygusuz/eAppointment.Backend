using eAppointment.Backend.Application.Features.Auth.Login;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<object> _localization;

        public LoginCommandValidator(UserManager<User> userManager, IStringLocalizer<object> localization)
        {
            _userManager = userManager;
            _localization = localization;

            var validationMessagePath = "Features.Auth.Login.ValidationMessages";

            RuleFor(x => x.userNameOrEmail)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "UserName.NotNull"])
                .MinimumLength(3).WithMessage(_localization[validationMessagePath + "." + "UserName.MinimumLength"])
                .MaximumLength(100).WithMessage(_localization[validationMessagePath + "." + "UserName.MaximumLength"])
                .Matches("^((?![ ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseSpaces"])
                .Matches("^((?![ğĞçÇşŞüÜöÖıİ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseTurkishCharacters"])
                .Matches("^((?![A-Z]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseUpperLetters"])
                .Matches("^((?![0-9]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseNumbers"])
                .Must(UniqueUsername).WithMessage(_localization[validationMessagePath + "." + "UserName.NotUnique"]);

            RuleFor(x => x.password)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "Password.NotNull"])
                .MinimumLength(1).WithMessage(_localization[validationMessagePath + "." + "Password.MinimumLength"])
                .MaximumLength(5).WithMessage(_localization[validationMessagePath + "." + "Password.MaximumLength"]);
        }

        private bool UniqueUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return true;
            }

            var user = _userManager.FindByNameAsync(username).GetAwaiter().GetResult();

            if (user == null)
            {
                return true;
            }

            return false;
        }
    }
}
