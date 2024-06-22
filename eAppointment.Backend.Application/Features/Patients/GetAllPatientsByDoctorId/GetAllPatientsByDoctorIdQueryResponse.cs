namespace eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId
{
    public sealed record GetAllPatientsByDoctorIdQueryResponse(
        int id,
        string firstName,
        string lastName,
        string identityNumber,
        List<PatientAppointment> patientAppointments);

    public sealed record PatientAppointment()
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }
}
