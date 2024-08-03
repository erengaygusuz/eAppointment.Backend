using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class DoctorsPermission : SmartEnum<DoctorsPermission>
    {
        public static readonly DoctorsPermission CreateDoctor = 
            new("Permissions.Doctors.CreateDoctor", 1);

        public static readonly DoctorsPermission GetAllDoctorsByDepartmentId = 
            new("Permissions.Doctors.GetAllDoctorsByDepartmentId", 2);

        public static readonly DoctorsPermission GetDoctorById = 
            new("Permissions.Doctors.GetDoctorById", 3);

        public static readonly DoctorsPermission UpdateDoctorById = 
            new("Permissions.Doctors.UpdateDoctorById", 4);

        public static readonly DoctorsPermission UpdateDoctorProfileById = 
            new("Permissions.Doctors.UpdateDoctorProfileById", 5);

        public DoctorsPermission(string name, int value) : base(name, value)
        {
        }
    }
}
