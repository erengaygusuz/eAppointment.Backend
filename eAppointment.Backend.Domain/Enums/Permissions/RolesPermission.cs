using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class RolesPermission : SmartEnum<RolesPermission>
    {
        public static readonly RolesPermission GetAllRoles = 
            new("Permissions.Roles.GetAllRoles", 1);

        public RolesPermission(string name, int value) : base(name, value)
        {
        }
    }
}
