using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    internal sealed class UpdateAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<UpdateAppointmentByIdCommandHandler> logger) : IRequestHandler<UpdateAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.UpdateAppointment.Others";

            Appointment? appointment = await appointmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (appointment is null)
            {
                logger.LogError(localization[translatedMessagePath + "." + "NotFound"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "NotFound"]);
            }

            mapper.Map(request, appointment);

            appointmentRepository.Update(appointment);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
