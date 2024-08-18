using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById
{
    internal sealed class UpdateAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<UpdateAppointmentByIdCommandHandler> logger) : IRequestHandler<UpdateAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.UpdateAppointment.Others";

            Appointment? appointment = await appointmentRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.id, cancellationToken);

            if (appointment is null)
            {
                logger.LogError(localization[translatedMessagePath + "." + "NotFound"].Value);

                return Result<string>.Failure(localization[translatedMessagePath + "." + "NotFound"]);
            }

            mapper.Map(request, appointment);

            appointmentRepository.Update(appointment);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);

            return localization[translatedMessagePath + "." + "SuccessfullyUpdated"].Value;
        }
    }
}
