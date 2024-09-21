namespace eAppointment.Backend.Domain.Entities
{
    public sealed class DepartmentTranslation
    {
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public string TranslationText { get; set; }
    }
}
