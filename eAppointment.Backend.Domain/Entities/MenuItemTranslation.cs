namespace eAppointment.Backend.Domain.Entities
{
    public sealed class MenuItemTranslation : BaseEntity
    {
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public int MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; }

        public string TranslationText { get; set; }
    }
}
