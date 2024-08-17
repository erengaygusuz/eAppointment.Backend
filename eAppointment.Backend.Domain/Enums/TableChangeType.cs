using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums
{
    public sealed class TableChangeType : SmartEnum<TableChangeType>
    {
        public static readonly TableChangeType Create = new("Create", 1);
        public static readonly TableChangeType Update = new("Update", 2);
        public static readonly TableChangeType Delete = new("Delete", 3);

        public TableChangeType(string name, int value) : base(name, value)
        {
        }
    }
}
