namespace eAppointment.Backend.Domain.Entities
{
    public sealed class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<County>? Counties { get; set; }
    }
}
