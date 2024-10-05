using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using eAppointment.Backend.Domain.Helpers;
using System.Net;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentStatusById
{
    internal sealed class UpdateAppointmentStatusByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<UpdateAppointmentStatusByIdCommandHandler> logger) : IRequestHandler<UpdateAppointmentStatusByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAppointmentStatusByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.UpdateAppointmentStatus.Others";

            Appointment? appointment = await appointmentRepository.GetAsync(
               expression: p => p.Id == request.id,
               trackChanges: false,
               include: null,
               orderBy: null,
               cancellationToken);

            if (appointment is null)
            {
                logger.LogError(localization[translatedMessagePath + "." + "NotFound"].Value);

                return Result<string>.Failure((int)HttpStatusCode.NotFound, localization[translatedMessagePath + "." + "CouldNotFound"].Value);
            }

            mapper.Map(request, appointment);

            appointmentRepository.Update(appointment);

            return Result<string>.Succeed((int)HttpStatusCode.OK, localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value);
        }
    }
}
