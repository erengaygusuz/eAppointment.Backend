namespace eAppointment.Backend.Domain.Entities
{
    public sealed class County
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public City City { get; set; } = new ();

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }
    }
}
