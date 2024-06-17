namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Doctor
    {
        public Doctor() 
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = new();

        public Guid DepartmentId { get; set; }

        public Department Department { get; set; } = new ();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
