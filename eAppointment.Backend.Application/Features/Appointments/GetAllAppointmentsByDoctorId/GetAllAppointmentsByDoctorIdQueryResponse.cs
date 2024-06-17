namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointments
{
    public sealed record GetAllAppointmentsByDoctorIdQueryResponse(
        Guid id,
        DateTime startDate,
        DateTime endDate,
        string title);
}
