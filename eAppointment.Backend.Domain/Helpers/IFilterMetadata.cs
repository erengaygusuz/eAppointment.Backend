namespace eAppointment.Backend.Domain.Helpers
{
    public interface IFilterMetadata
    {
        object? value { set; get; }

        string? matchMode { set; get; }

        string? @operator { set; get; }
    }
}
