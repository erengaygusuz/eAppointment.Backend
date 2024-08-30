using System.Text;

namespace eAppointment.Backend.Domain.Helpers
{
    public class LazyLoadEvent
    {
        public long first;

        public long rows;

        public string sortField;

        public int sortOrder;

        public object multiSortMeta;

        public Dictionary<string, FilterMetadata> filters;

        public object globalFilter;

        public override string ToString()
        {
            string _null = "/null/";

            StringBuilder _return = new StringBuilder("LazyLoadEvent:[");

            _return.AppendFormat("first: {0}, rows: {1}, ", first, rows);
            _return.AppendFormat("sortField: {0}, sortOrder: {1}, ", sortField != null ? sortField : _null, sortOrder);
            _return.AppendFormat("multiSortMeta: {0}, ", multiSortMeta != null ? multiSortMeta.ToString() : _null);
            _return.AppendFormat("filters: ");

            if (filters != null)
            {
                foreach (KeyValuePair<string, FilterMetadata> metadata in filters)
                {
                    _return.AppendFormat("{0}-{1}:{2}:{3}, ", metadata.Key,
                        metadata.Value.matchMode, metadata.Value.value, metadata.Value.@operator != null ? metadata.Value.@operator : "/null/");
                }
            }

            else
            {
                _return.AppendFormat("{0}, ", _null);
            }

            _return.AppendFormat("globalFilter: {0}]", globalFilter != null ? globalFilter.ToString() : "/null/");

            return _return.ToString();
        }
    }
}
