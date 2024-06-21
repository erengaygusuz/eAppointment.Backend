namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus
{
    public sealed record GetAllAppointmentsByDoctorIdAndByStatusQueryResponse(
        Guid id,
        DateTime startDate,
        DateTime endDate,
        string title);
}
