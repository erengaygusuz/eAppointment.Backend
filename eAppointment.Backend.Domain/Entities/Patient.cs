namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Patient
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string IdentityNumber { get; set; } = string.Empty;

        public int? CountyId { get; set; }

        public County County { get; set; }

        public string? FullAddress { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
