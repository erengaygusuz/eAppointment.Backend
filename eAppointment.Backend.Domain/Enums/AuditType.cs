using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums
{
    public sealed class AuditType : SmartEnum<AuditType>
    {
        public static readonly AuditType Create = new("Create", 1);
        public static readonly AuditType Update = new("Update", 2);
        public static readonly AuditType Delete = new("Delete", 3);

        public AuditType(string name, int value) : base(name, value)
        {
        }
    }
}
