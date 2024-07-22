using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace eAppointment.Backend.Application.Validators
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        private readonly UserManager<User> _userManager;

        public CreateAdminCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.firstName)
                .NotNull().WithMessage("Please fill the firstname")
                .MinimumLength(3).WithMessage("Please enter minimum 3 character for firstname")
                .MaximumLength(50).WithMessage("Please enter maximum 50 character for firstname")
                .Matches("^((?![0-9]).)*$").WithMessage("Please do not use numbers in the firstname");

            RuleFor(x => x.lastName)
                .NotNull().WithMessage("Please fill the lastname")
                .MinimumLength(3).WithMessage("Please enter minimum 3 character for lastname")
                .MaximumLength(50).WithMessage("Please enter maximum 50 character for lastname")
                .Matches("^((?![0-9]).)*$").WithMessage("Please do not use numbers in the lastname");

            RuleFor(x => x.userName)
                .NotNull().WithMessage("Please fill the username")
                .MinimumLength(3).WithMessage("Please enter minimum 3 character for username")
                .MaximumLength(100).WithMessage("Please enter maximum 50 character for username")
                .Matches("^((?![ ]).)*$").WithMessage("Please do not use spaces for username")
                .Matches("^((?![ğĞçÇşŞüÜöÖıİ]).)*$").WithMessage("Please do not use turkish characters for username")
                .Matches("^((?![A-Z]).)*$").WithMessage("Please do not use upper letters for username")
                .Matches("^((?![0-9]).)*$").WithMessage("Please do not use numbers in the username")
                .Must(UniqueUsername).WithMessage("Please choose a unique username");

            RuleFor(x => x.email)
                .NotNull().WithMessage("Please fill the e-mail")
                .MaximumLength(150).WithMessage("Please enter maximum 150 character for e-mail")
                .EmailAddress().WithMessage("Please enter a valid e-mail")
                .Must(UniqueEmail).WithMessage("Please choose a unique e-mail");

            RuleFor(x => x.phoneNumber)
                .NotNull().WithMessage("Please fill the phone number")
                .Matches("((\\(\\d{3}\\) ?)|(\\d{3}-)) ?\\d{3}-\\d{4}").WithMessage("Please enter a valid phone number")
                .Matches("^((?![a-zA-Z]).)*$").WithMessage("Please do not use letters in the phone number");

            RuleFor(x => x.password)
                .NotNull().WithMessage("Please fill the password")
                .MinimumLength(1).WithMessage("Please enter minimum 1 character for password")
                .MaximumLength(5).WithMessage("Please enter maximum 5 character for password");
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
