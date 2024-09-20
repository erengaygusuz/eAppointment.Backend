namespace eAppointment.Backend.Domain.Helpers
{
    public class MenuTreeItem
    {
        public string Key { get; set; }

        public List<MenuTreeItem>? Items { get; set; }
    }
}
