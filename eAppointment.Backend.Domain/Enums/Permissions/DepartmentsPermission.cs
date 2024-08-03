using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class DepartmentsPermission : SmartEnum<DepartmentsPermission>
    {
        public static readonly DepartmentsPermission CreateDepartment = 
            new("Permissions.Departments.CreateDepartment", 1);

        public static readonly DepartmentsPermission DeleteDepartmentById = 
            new("Permissions.Departments.DeleteDepartmentById", 2);

        public static readonly DepartmentsPermission GetAllDepartments = 
            new("Permissions.Departments.GetAllDepartments", 3);

        public static readonly DepartmentsPermission GetDepartmentById = 
            new("Permissions.Departments.GetDepartmentById", 4);

        public static readonly DepartmentsPermission UpdateDepartmentById = 
            new("Permissions.Departments.UpdateDepartmentById", 5);

        public DepartmentsPermission(string name, int value) : base(name, value)
        {
        }
    }
}
