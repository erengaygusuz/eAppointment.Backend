namespace eAppointment.Backend.Domain.Helpers
{
    public class SortMeta : ISortMeta
    {
        public string field { set; get; }

        public int order { set; get; }
    }
}
