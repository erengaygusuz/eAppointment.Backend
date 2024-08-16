namespace eAppointment.Backend.Domain.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionStackTrace { get; set; }

        public string InnerExceptionMessage { get; set; }

        public string InnerExceptionStackTrace { get; set; }

        public int AuditLogId { get; set; }

        public AuditLog AuditLog { get; set; }
    }
}
