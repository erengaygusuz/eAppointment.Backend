using eAppointment.Backend.Domain.Enums;

namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Appointment
    {
        public Appointment() 
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid DoctorId { get; set; }

        public Doctor Doctor { get; set; } = new ();

        public Guid PatientId { get; set; }

        public Patient Patient { get; set; } = new ();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.NotCompleted;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }
    }
}
