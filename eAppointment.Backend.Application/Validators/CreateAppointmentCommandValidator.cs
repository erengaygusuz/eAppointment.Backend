using eAppointment.Backend.Application.Features.Appointments.CreateAppointment;
using eAppointment.Backend.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Validators
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IStringLocalizer<object> _localization;

        public CreateAppointmentCommandValidator(IAppointmentRepository appointmentRepository, IStringLocalizer<object> localization)
        {
            _appointmentRepository = appointmentRepository;
            _localization = localization;

            var validationMessagePath = "Features.Appointments.CreateAppointment.ValidationMessages";

            RuleFor(x => x.startDate)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "StartDate.NotNull"])
                .Must(IsValidDate).WithMessage(_localization[validationMessagePath + "." + "StartDate.NotValid"]);

            RuleFor(x => x.endDate)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "EndDate.NotNull"])
                .Must(IsValidDate).WithMessage(_localization[validationMessagePath + "." + "EndDate.NotValid"]);

            RuleFor(x => x.patientId)
                .GreaterThan(0).WithMessage(_localization[validationMessagePath + "." + "PatientId.GreaterThanZero"]);

            RuleFor(x => x.doctorId)
                .GreaterThan(0).WithMessage(_localization[validationMessagePath + "." + "DoctorId.GreaterThanZero"]);

            RuleFor(x => new { x.startDate, x.endDate, x.doctorId })
                .Must(x => IsAvailable(x.startDate, x.endDate, x.doctorId)).WithMessage(_localization[validationMessagePath + "." + "Composite.IsAvailable"]);
        }

        private bool IsAvailable(string startDateStr, string endDateStr, int doctorId)
        {
            DateTime startDate = DateTime.ParseExact(startDateStr, "dd.MM.yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(endDateStr, "dd.MM.yyyy HH:mm", null);

            bool isAppointmentDateNotAvailable = _appointmentRepository
                    .Any(p => p.DoctorId == doctorId &&
                     ((p.StartDate < endDate && p.StartDate >= startDate) || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor
                     (p.EndDate > startDate && p.EndDate <= endDate) || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor
                     (p.StartDate >= startDate && p.EndDate <= endDate) || // Mevcut randevu, diğer randevu içinde tamamen
                     (p.StartDate <= startDate && p.EndDate >= endDate)) // Mevcut randevu, diğer randevuyu tamamen kapsıyor
                     );

            return isAppointmentDateNotAvailable;
        }

        private bool IsValidDate(string date)
        {
            DateTime resultDate;

            var isValid = DateTime.TryParseExact(date, "dd.MM.yyyy HH:mm", null, System.Globalization.DateTimeStyles.AssumeUniversal, out resultDate);

            return isValid;
        }
    }
}
