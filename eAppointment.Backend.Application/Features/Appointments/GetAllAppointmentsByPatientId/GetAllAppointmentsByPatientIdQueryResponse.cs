namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId
{
    public sealed record GetAllAppointmentsByPatientIdQueryResponse
    {
        public string DepartmentName { get; set; }
        public string DoctorName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}
