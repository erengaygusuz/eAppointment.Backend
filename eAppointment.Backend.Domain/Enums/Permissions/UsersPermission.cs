using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class UsersPermission : SmartEnum<UsersPermission>
    {
        public static readonly UsersPermission DeleteUserById = 
            new("Permissions.Users.DeleteUserById", 1);

        public static readonly UsersPermission GetAllUsers = 
            new("Permissions.Users.GetAllUsers", 2);

        public UsersPermission(string name, int value) : base(name, value)
        {
        }
    }
}
