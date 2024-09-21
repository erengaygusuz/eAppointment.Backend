namespace eAppointment.Backend.Domain.Entities
{
    public sealed class MenuItem
    {
        public int Id { get; set; }

        public string MenuKey { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public MenuItem? Parent { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<MenuItem>? Children { get; set; }

        public ICollection<Role> Roles { get; set; }

        public ICollection<Language> Translations { get; set; }
    }
}
