namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Patient : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public string IdentityNumber { get; set; } = string.Empty;

        public int? CountyId { get; set; }

        public County County { get; set; }

        public string? FullAddress { get; set; } = string.Empty;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
