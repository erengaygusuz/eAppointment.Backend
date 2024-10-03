using eAppointment.Backend.Domain.Enums;

namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Appointment : BaseEntity
    {
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.NotCompleted;
    }
}
