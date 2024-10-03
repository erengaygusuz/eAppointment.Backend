namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Page : BaseEntity
    {
        public string PageKey { get; set; } = string.Empty;

        public ICollection<Role> Roles { get; set; }
    }
}
