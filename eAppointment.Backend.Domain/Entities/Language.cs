namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Language
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public ICollection<MenuItemTranslation> MenuItemTranslations { get; set; }

        public ICollection<DepartmentTranslation> DepartmentTranslations { get; set; }
    }
}
