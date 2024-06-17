namespace eAppointment.Backend.Domain.Entities
{
    public sealed class County
    {
        public County()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid CityId { get; set; }

        public City City { get; set; } = new ();

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }
    }
}
