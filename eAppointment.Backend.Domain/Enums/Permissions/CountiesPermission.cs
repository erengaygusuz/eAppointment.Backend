using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class CountiesPermission : SmartEnum<CountiesPermission>
    {
        public static readonly CountiesPermission GetAllCountiesByCityId = 
            new("Permissions.Counties.GetAllCountiesByCityId", 1);

        public CountiesPermission(string name, int value) : base(name, value)
        {
        }
    }
}
