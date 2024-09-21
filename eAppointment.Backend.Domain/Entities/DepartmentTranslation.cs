namespace eAppointment.Backend.Domain.Entities
{
    public sealed class DepartmentTranslation
    {
        public int Id { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public string TranslationText { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }
    }
}
