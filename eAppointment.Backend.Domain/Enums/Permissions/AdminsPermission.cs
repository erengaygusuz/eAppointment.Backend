using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class AdminsPermission : SmartEnum<AdminsPermission>
    {
        public const string CreateAdminPR = "Permissions.Admins.CreateAdmin";

        public static readonly AdminsPermission CreateAdmin = 
            new(CreateAdminPR, 1);

        public static readonly AdminsPermission GetAdminById = 
            new("Permissions.Admins.GetAdminById", 2);

        public static readonly AdminsPermission UpdateAdminById = 
            new("Permissions.Admins.UpdateAdminById", 3);

        public AdminsPermission(string name, int value) : base(name, value)
        {
        }
    }
}
