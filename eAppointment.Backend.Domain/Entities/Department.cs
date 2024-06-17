namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Department
    {
        public Department()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public Doctor? Doctor { get; set; }
    }
}
