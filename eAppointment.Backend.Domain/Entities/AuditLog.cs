namespace eAppointment.Backend.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public int StatusCode { get; set; }
        public string Headers { get; set; }
        public string IPAddress { get; set; }
    }

}
