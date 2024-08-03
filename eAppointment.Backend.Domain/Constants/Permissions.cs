using eAppointment.Backend.Domain.Enums.Permissions;

namespace eAppointment.Backend.Domain.Constants
{
    public static class Permissions
    {
        public static List<string> GetAllPermissions(string roleName)
        {
            var allPermissions = new List<string>();

            var result = roleName switch
            {
                Roles.SuperAdmin => new List<string>()
                {
                    AdminsPermission.CreateAdmin.Name,
                    AdminsPermission.GetAdminById.Name,
                    AdminsPermission.UpdateAdminById.Name,
                    DoctorsPermission.CreateDoctor.Name,
                    DoctorsPermission.GetDoctorById.Name,
                    DoctorsPermission.UpdateDoctorById.Name,
                    PatientsPermission.CreatePatient.Name,
                    PatientsPermission.GetPatientById.Name,
                    PatientsPermission.UpdatePatientById.Name,
                    RolesPermission.GetAllRoles.Name,
                    UsersPermission.GetAllUsers.Name,
                    UsersPermission.DeleteUserById.Name,
                    CitiesPermission.GetAllCities.Name,
                    CountiesPermission.GetAllCountiesByCityId.Name
                },
                Roles.Admin => new List<string>()
                {
                    DoctorsPermission.CreateDoctor.Name,
                    DoctorsPermission.GetDoctorById.Name,
                    DoctorsPermission.UpdateDoctorById.Name,
                    PatientsPermission.CreatePatient.Name,
                    PatientsPermission.GetPatientById.Name,
                    PatientsPermission.UpdatePatientById.Name,
                    CitiesPermission.GetAllCities.Name,
                    CountiesPermission.GetAllCountiesByCityId.Name
                },
                Roles.Doctor => new List<string>()
                {
                    AppointmentsPermission.GetAllAppointmentsByDoctorId.Name,
                    AppointmentsPermission.UpdateAppointmentStatusById.Name
                },
                Roles.Patient => new List<string>()
                {
                    DepartmentsPermission.GetAllDepartments.Name,
                    DoctorsPermission.GetAllDoctorsByDepartmentId.Name,
                    AppointmentsPermission.CreateAppointment.Name,
                    AppointmentsPermission.CancelAppointmentById.Name,
                    AppointmentsPermission.GetAllAppointmentsByPatientId.Name,
                    AppointmentsPermission.UpdateAppointmentById.Name
                }
            };

            allPermissions.AddRange(result);

            return allPermissions;
        }
    }


}
