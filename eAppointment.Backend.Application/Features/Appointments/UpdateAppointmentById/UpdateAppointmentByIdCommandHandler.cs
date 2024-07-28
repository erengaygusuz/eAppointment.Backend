using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.Localization;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    internal sealed class UpdateAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<UpdateAppointmentByIdCommand> updateAppointmentByIdCommandValidator,
        IStringLocalizer<object> localization) : IRequestHandler<UpdateAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.UpdateAppointment.Others";

            var validationResult = await updateAppointmentByIdCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            Appointment? appointment = await appointmentRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.id, cancellationToken);

            if (appointment is null)
            {
                return Result<string>.Failure(localization[translatedMessagePath + "." + "NotFound"]);
            }

            mapper.Map(request, appointment);

            appointmentRepository.Update(appointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
