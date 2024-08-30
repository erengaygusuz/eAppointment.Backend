using System.Text;

namespace eAppointment.Backend.Domain.Helpers
{
    public class LazyLoadEvent2
    {
        public long first { set; get; }

        public long rows { set; get; }

        public string sortField { set; get; }

        public int sortOrder { set; get; }

        public object multiSortMeta { set; get; }

        public Dictionary<string, FilterMetadata[]> filters { set; get; }

        public string globalFilter { set; get; }

        public override string ToString()
        {
            string _null = "/null/";

            StringBuilder _return = new StringBuilder("LazyLoadEvent2:[");

            _return.AppendFormat("first: {0}, rows: {1}, ", first, rows);
            _return.AppendFormat("sortField: {0}, sortOrder: {1}, ", sortField != null ? sortField : _null, sortOrder);
            _return.AppendFormat("multiSortMeta: {0}, ", multiSortMeta != null ? multiSortMeta.ToString() : _null);
            _return.AppendFormat("filters: ");

            if (filters != null)
            {
                foreach (KeyValuePair<string, FilterMetadata[]> metakey in filters)
                {
                    foreach (FilterMetadata metadata in metakey.Value)
                    {
                        _return.AppendFormat("{0}-{1}:{2}:{3}, ", metakey.Key,
                            metadata.matchMode, (metadata.value != null ? metadata.value : _null), metadata.@operator != null ? metadata.@operator : _null);
                    }
                }
            }

            else
            {
                _return.AppendFormat("{0}, ", _null);
            }

            _return.AppendFormat("globalFilter: {0}]", globalFilter != null ? globalFilter.ToString() : _null);

            return _return.ToString();
        }
    }
}
