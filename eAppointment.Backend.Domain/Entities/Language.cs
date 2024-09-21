namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Language
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
