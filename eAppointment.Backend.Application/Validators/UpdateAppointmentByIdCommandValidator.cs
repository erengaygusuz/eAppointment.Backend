using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Validators
{
    public class UpdateAppointmentByIdCommandValidator : AbstractValidator<UpdateAppointmentByIdCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<object> _localization;

        public UpdateAppointmentByIdCommandValidator(UserManager<User> userManager, IStringLocalizer<object> localization)
        {
            _userManager = userManager;
            _localization = localization;

            var validationMessagePath = "Features.Admins.CreateAdmin.ValidationMessages";

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

            RuleFor(x => x.userName)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "UserName.NotNull"])
                .MinimumLength(3).WithMessage(_localization[validationMessagePath + "." + "UserName.MinimumLength"])
                .MaximumLength(100).WithMessage(_localization[validationMessagePath + "." + "UserName.MaximumLength"])
                .Matches("^((?![ ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseSpaces"])
                .Matches("^((?![ğĞçÇşŞüÜöÖıİ]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseTurkishCharacters"])
                .Matches("^((?![A-Z]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseUpperLetters"])
                .Matches("^((?![0-9]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseNumbers"])
                .Must(UniqueUsername).WithMessage(_localization[validationMessagePath + "." + "UserName.NotUnique"]);

            RuleFor(x => x.email)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "Email.NotNull"])
                .MaximumLength(150).WithMessage(_localization[validationMessagePath + "." + "Email.MaximumLength"])
                .EmailAddress().WithMessage(_localization[validationMessagePath + "." + "Email.NotValid"])
                .Must(UniqueEmail).WithMessage(_localization[validationMessagePath + "." + "Email.NotUnique"]);

            RuleFor(x => x.phoneNumber)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotNull"])
                .Matches("((\\(\\d{3}\\) ?)|(\\d{3}-)) ?\\d{3}-\\d{4}").WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotValid"])
                .Matches("^((?![a-zA-Z]).)*$").WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotUseLetters"]);

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

        private bool UniqueEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }

            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();

            if (user == null)
            {
                return true;
            }

            return false;
        }
    }
}
