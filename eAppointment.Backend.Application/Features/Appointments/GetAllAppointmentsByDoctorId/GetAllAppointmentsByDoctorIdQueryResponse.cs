namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorId
{
    public sealed record GetAllAppointmentsByDoctorIdQueryResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}
