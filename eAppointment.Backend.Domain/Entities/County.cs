namespace eAppointment.Backend.Domain.Entities
{
    public sealed class County : BaseEntity
    {
        public int CityId { get; set; }

        public City City { get; set; } = new();

        public string Name { get; set; } = string.Empty;
    }
}
