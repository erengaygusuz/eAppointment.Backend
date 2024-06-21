namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus
{
    public sealed record GetAllAppointmentsByPatientIdAndByStatusQueryResponse(
        Guid id,
        DateTime startDate,
        DateTime endDate,
        string title);
}
