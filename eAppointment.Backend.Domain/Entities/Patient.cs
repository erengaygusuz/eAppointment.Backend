namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Patient
    {
        public Patient() 
        { 
            Id = Guid.NewGuid();
        }    

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = new();

        public string IdentityNumber { get; set; } = string.Empty;

        public Guid? CountyId { get; set; }

        public County County { get; set; } = new ();

        public string? FullAddress { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
