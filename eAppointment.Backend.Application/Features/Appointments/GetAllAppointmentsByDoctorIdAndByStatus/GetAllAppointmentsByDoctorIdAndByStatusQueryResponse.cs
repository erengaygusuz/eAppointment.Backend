namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus
{
    public sealed record GetAllAppointmentsByDoctorIdAndByStatusQueryResponse(
        int id,
        DateTime startDate,
        DateTime endDate,
        string title);
}
