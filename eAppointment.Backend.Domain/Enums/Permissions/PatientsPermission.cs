using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class PatientsPermission : SmartEnum<PatientsPermission>
    {
        public static readonly PatientsPermission CreatePatient = 
            new("Permissions.Patients.CreatePatient", 1);

        public static readonly PatientsPermission GetAllPatientsByDoctorId = 
            new("Permissions.Patients.GetAllPatientsByDoctorId", 2);

        public static readonly PatientsPermission GetPatientById = 
            new("Permissions.Patients.GetPatientById", 3);

        public static readonly PatientsPermission UpdatePatientById = 
            new("Permissions.Patients.UpdatePatientById", 4);

        public static readonly PatientsPermission UpdatePatientProfileById = 
            new("Permissions.Patients.UpdatePatientProfileById", 5);

        public PatientsPermission(string name, int value) : base(name, value)
        {
        }
    }
}
