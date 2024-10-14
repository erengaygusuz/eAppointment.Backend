using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById
{
    public class UpdateDoctorByIdCommandValidator : AbstractValidator<UpdateDoctorByIdCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<object> _localization;

        public UpdateDoctorByIdCommandValidator(UserManager<User> userManager, IStringLocalizer<object> localization)
        {
            _userManager = userManager;
            _localization = localization;

            var validationMessagePath = "Features.Doctors.UpdateDoctorById.ValidationMessages";

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
                .Matches("^((?![0-9]).)*$").WithMessage(_localization[validationMessagePath + "." + "UserName.NotUseNumbers"]);

            RuleFor(x => new { x.id, x.userName })
                .Must(x => UniqueUsername(x.id, x.userName)).WithMessage(_localization[validationMessagePath + "." + "UserName.NotUnique"]);

            RuleFor(x => x.email)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "Email.NotNull"])
                .MaximumLength(150).WithMessage(_localization[validationMessagePath + "." + "Email.MaximumLength"])
                .EmailAddress().WithMessage(_localization[validationMessagePath + "." + "Email.NotValid"]);

            RuleFor(x => new { x.id, x.email })
                .Must(x => UniqueEmail(x.id, x.email)).WithMessage(_localization[validationMessagePath + "." + "Email.NotUnique"]);

            RuleFor(x => x.phoneNumber)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotNull"])
                .Matches("((\\(\\d{3}\\) ?)|(\\d{3}-)) ?\\d{3}-\\d{4}").WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotValid"])
                .Matches("^((?![a-zA-Z]).)*$").WithMessage(_localization[validationMessagePath + "." + "PhoneNumber.NotUseLetters"]);

            RuleFor(x => x.departmentId)
                .GreaterThan(0).WithMessage(_localization[validationMessagePath + "." + "DepartmentId.GreaterThanZero"]);
        }

        private bool UniqueUsername(int id, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return true;
            }

            var myUser = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();

            var otherUsers = _userManager.Users.Where(x => x.UserName != myUser.UserName).ToList();

            var user = otherUsers.Where(x => x.UserName == username).FirstOrDefault();

            if (user == null)
            {
                return true;
            }

            return false;
        }

        private bool UniqueEmail(int id, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }

            var myUser = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();

            var otherUsers = _userManager.Users.Where(x => x.Email != myUser.Email).ToList();

            var user = otherUsers.Where(x => x.Email == email).FirstOrDefault();

            if (user == null)
            {
                return true;
            }

            return false;
        }
    }
}
