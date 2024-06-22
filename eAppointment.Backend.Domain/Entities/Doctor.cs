namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Doctor
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = new();

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = new ();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
