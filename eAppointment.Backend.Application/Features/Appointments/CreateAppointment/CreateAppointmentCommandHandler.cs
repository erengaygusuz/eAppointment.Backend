using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.CreateAppointment
{
    internal sealed class CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<object> localization,
        ILogger<CreateAppointmentCommandHandler> logger) : IRequestHandler<CreateAppointmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var translatedMessagePath = "Features.Appointments.CreateAppointment.Others";

            var appointment = mapper.Map<Appointment>(request);

            await appointmentRepository.AddAsync(appointment, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value);

            return localization[translatedMessagePath + "." + "SuccessfullyCreated"].Value;
        }
    }
}
