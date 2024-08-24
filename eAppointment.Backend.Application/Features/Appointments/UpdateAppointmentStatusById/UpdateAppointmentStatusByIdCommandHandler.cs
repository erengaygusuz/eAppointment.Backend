using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentStatusById
{
    internal sealed class UpdateAppointmentStatusByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
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

                return Result<string>.Failure(localization[translatedMessagePath + "." + "NotFound"].Value);
            }

            mapper.Map(request, appointment);

            appointmentRepository.Update(appointment);

            await unitOfWork.SaveAsync(cancellationToken);

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
