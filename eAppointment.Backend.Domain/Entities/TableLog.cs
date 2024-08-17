using eAppointment.Backend.Domain.Enums;

namespace eAppointment.Backend.Domain.Entities
{
    public class TableLog
    {
        public int Id { get; set; }

        public TableChangeType TableChangeType { get; set; }

        public string TableName { get; set; }

        public string? OldValues { get; set; }

        public string NewValues { get; set; }

        public string? AffectedColumns { get; set; }

        public string KeyValues { get; set; }

        public int AuditLogId { get; set; }

        public AuditLog AuditLog { get; set; }  
    }
}
