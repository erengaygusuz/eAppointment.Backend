namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Page
    {
        public int Id { get; set; }

        public string PageKey { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
