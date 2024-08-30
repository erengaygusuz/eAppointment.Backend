namespace eAppointment.Backend.Domain.Helpers
{
    public class FilterMetadata : IFilterMetadata
    {
        public object? value { set; get; }

        public string? matchMode { set; get; }

        public string? @operator { set; get; }
    }
}
