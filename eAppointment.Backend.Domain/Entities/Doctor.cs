namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Doctor : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
