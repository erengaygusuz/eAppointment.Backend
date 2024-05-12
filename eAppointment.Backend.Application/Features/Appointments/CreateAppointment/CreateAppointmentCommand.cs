using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.CreateAppointment
{
    public sealed record CreateAppointmentCommand(
        string startDate,
        string endDate,
        Guid? patientId,
        Guid doctorId,
        string firstName,
        string lastName,
        string identityNumber,
        string city,
        string town,
        string fullAddress) : IRequest<Result<string>>;
}
