namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Department
    {
        public int Id { get; set; }

        public string DepartmentKey { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public Doctor? Doctor { get; set; }

        public ICollection<DepartmentTranslation> DepartmentTranslations { get; set; }
    }
}
