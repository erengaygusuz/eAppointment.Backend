using eAppointment.Backend.Domain.Enums;

namespace eAppointment.Backend.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        public AuditType? AuditType { get; set; }

        public string? TableName { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public string? AffectedColumns { get; set; }

        public string? KeyValues { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

}
