namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus
{
    public sealed record GetAllAppointmentsByPatientIdAndByStatusQueryResponse(
        int id,
        DateTime startDate,
        DateTime endDate,
        string title);
}
