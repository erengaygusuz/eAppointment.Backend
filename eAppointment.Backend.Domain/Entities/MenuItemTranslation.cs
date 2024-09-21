namespace eAppointment.Backend.Domain.Entities
{
    public sealed class MenuItemTranslation
    {
        public int Id { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public int MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; }

        public string TranslationText { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }
    }
}
