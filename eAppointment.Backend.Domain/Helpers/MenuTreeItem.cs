namespace eAppointment.Backend.Domain.Helpers
{
    public class MenuTreeItem
    {
        public string Label { get; set; }

        public string Icon { get; set; }

        public string RouterLink { get; set; }

        public List<MenuTreeItem>? Items { get; set; }
    }
}
