using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    public class UpdateAppointmentByIdCommandValidator : AbstractValidator<UpdateAppointmentByIdCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IStringLocalizer<object> _localization;

        public UpdateAppointmentByIdCommandValidator(IAppointmentRepository appointmentRepository, IStringLocalizer<object> localization)
        {
            _appointmentRepository = appointmentRepository;
            _localization = localization;

            var validationMessagePath = "Features.Appointments.UpdateAppointment.ValidationMessages";

            RuleFor(x => x.id)
                .GreaterThan(0).WithMessage(_localization[validationMessagePath + "." + "Id.GreaterThanZero"]);

            RuleFor(x => x.startDate)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "StartDate.NotNull"])
                .Must(IsValidDate).WithMessage(_localization[validationMessagePath + "." + "StartDate.IsValidDate"]);

            RuleFor(x => x.endDate)
                .NotNull().WithMessage(_localization[validationMessagePath + "." + "EndDate.NotNull"])
                .Must(IsValidDate).WithMessage(_localization[validationMessagePath + "." + "EndDate.IsValidDate"]);

            RuleFor(x => x.status)
                .GreaterThan(0).WithMessage(_localization[validationMessagePath + "." + "Status.GreaterThanZero"])
                .Must(IsValidStatus).WithMessage(_localization[validationMessagePath + "." + "Status.IsValidStatus"]);

            RuleFor(x => new { x.startDate, x.endDate, x.id })
                .Must(x => IsAvailable(x.startDate, x.endDate, x.id)).WithMessage(_localization[validationMessagePath + "." + "Composite.IsAvailable"]);
        }

        private bool IsValidDate(string date)
        {
            DateTime resultDate;

            var isValid = DateTime.TryParseExact(date, "dd.MM.yyyy HH:mm", null, System.Globalization.DateTimeStyles.AssumeUniversal, out resultDate);

            return isValid;
        }

        private bool IsValidStatus(int status)
        {
            var appointmentStatusList = AppointmentStatus.List.Select(x => x.Value).ToList();

            return appointmentStatusList.Contains(status);
        }

        private bool IsAvailable(string startDateStr, string endDateStr, int id)
        {
            Appointment? appointment = _appointmentRepository.Get(
               expression: p => p.Id == id,
               trackChanges: false,
               include: null,
               orderBy: null);

            DateTime startDate = DateTime.ParseExact(startDateStr, "dd.MM.yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(endDateStr, "dd.MM.yyyy HH:mm", null);

            bool isAppointmentDateNotAvailable = _appointmentRepository
                    .Any(p => p.DoctorId == appointment.DoctorId &&
                     (p.StartDate < endDate && p.StartDate >= startDate || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor
                     p.EndDate > startDate && p.EndDate <= endDate || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor
                     p.StartDate >= startDate && p.EndDate <= endDate || // Mevcut randevu, diğer randevu içinde tamamen
                     p.StartDate <= startDate && p.EndDate >= endDate) // Mevcut randevu, diğer randevuyu tamamen kapsıyor
                     );

            return !isAppointmentDateNotAvailable;
        }
    }
}
