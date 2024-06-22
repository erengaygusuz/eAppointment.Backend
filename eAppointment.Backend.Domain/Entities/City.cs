namespace eAppointment.Backend.Domain.Entities
{
    public sealed class City
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<County>? Counties { get; set; }
    }
}
