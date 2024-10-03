namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Department : BaseEntity
    {
        public string DepartmentKey { get; set; } = string.Empty;

        public Doctor? Doctor { get; set; }

        public ICollection<DepartmentTranslation> DepartmentTranslations { get; set; }
    }
}
