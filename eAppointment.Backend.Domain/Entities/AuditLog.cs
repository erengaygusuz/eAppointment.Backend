namespace eAppointment.Backend.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string Method { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }

        public string QueryParameters { get; set; }

        public string RequestHeaders { get; set; }

        public string RequestBody { get; set; }

        public int StatusCode { get; set; }

        public string ResponseHeaders { get; set; }

        public string ResponseBody { get; set; }

        public string UserName { get; set; }

        public string RemoteIpAddress { get; set; }

        public string LocalIpAddress { get; set; }

        public int? RemotePort { get; set; }

        public int? LocalPort { get; set; }

        public DateTime Timestamp { get; set; }

        public ICollection<TableLog>? TableLogs { get; set; }

        public ErrorLog? ErrorLog { get; set; }
    }

}
