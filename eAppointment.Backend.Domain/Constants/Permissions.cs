namespace eAppointment.Backend.Domain.Constants
{
    public static class Permissions
    {
        public const string CreateAdmin = "Permissions.Admins.CreateAdmin";
        public const string GetAdminById = "Permissions.Admins.GetAdminById";
        public const string GetAdminProfileById = "Permissions.Admins.GetAdminProfileById";
        public const string UpdateAdminById = "Permissions.Admins.UpdateAdminById";
        public const string UpdateAdminProfileById = "Permissions.Admins.UpdateAdminProfileById";

        public const string CreateDoctor = "Permissions.Doctors.CreateDoctor";
        public const string GetDoctorById = "Permissions.Doctors.GetDoctorById";
        public const string UpdateDoctorById = "Permissions.Doctors.UpdateDoctorById";
        public const string GetAllDoctorsByDepartmentId = "Permissions.Doctors.GetAllDoctorsByDepartmentId";
        public const string UpdateDoctorProfileById = "Permissions.Patients.UpdateDoctorProfileById";

        public const string CreatePatient = "Permissions.Patients.CreatePatient";
        public const string GetPatientById = "Permissions.Patients.GetPatientById";
        public const string GetAllPatientsByDoctorId = "Permissions.Patients.GetAllPatientsByDoctorId";
        public const string UpdatePatientById = "Permissions.Patients.UpdatePatientById";
        public const string UpdatePatientProfileById = "Permissions.Patients.UpdatePatientProfileById";

        public const string GetAllRoles = "Permissions.Roles.GetAllRoles";

        public const string GetAllUsers = "Permissions.Users.GetAllUsers";
        public const string DeleteUserById = "Permissions.Users.DeleteUserById";

        public const string GetAllCities = "Permissions.Cities.GetAllCities";

        public const string GetAllCountiesByCityId = "Permissions.Counties.GetAllCountiesByCityId";

        public const string GetAllDepartments = "Permissions.Departments.GetAllDepartments";

        public const string GetAllAppointmentsByDoctorId = "Permissions.Appointments.GetAllAppointmentsByDoctorId";
        public const string UpdateAppointmentStatusById = "Permissions.Appointments.UpdateAppointmentStatusById";
        public const string CreateAppointment = "Permissions.Appointments.CreateAppointment";
        public const string CancelAppointmentById = "Permissions.Appointments.CancelAppointmentById";
        public const string GetAllAppointmentsByPatientId = "Permissions.Appointments.GetAllAppointmentsByPatientId";
        public const string UpdateAppointmentById = "Permissions.Appointments.UpdateAppointmentById";

        public static List<string> GetAllPermissions(string roleName)
        {
            var allPermissions = new List<string>();

            var result = roleName switch
            {
                Roles.SuperAdmin => new List<string>()
                {
                    CreateAdmin,
                    GetAdminById,
                    UpdateAdminById,
                    CreateDoctor,
                    GetDoctorById,
                    UpdateDoctorById,
                    CreatePatient,
                    GetPatientById,
                    UpdatePatientById,
                    GetAllRoles,
                    GetAllUsers,
                    DeleteUserById,
                    GetAllCities,
                    GetAllCountiesByCityId,
                    GetAllDepartments
                },
                Roles.Admin => new List<string>()
                {
                    GetAdminById,
                    GetAdminProfileById,
                    UpdateAdminProfileById,
                    CreateDoctor,
                    GetDoctorById,
                    UpdateDoctorById,
                    CreatePatient,
                    GetPatientById,
                    UpdatePatientById,
                    GetAllCities,
                    GetAllCountiesByCityId
                },
                Roles.Doctor => new List<string>()
                {
                    GetAllAppointmentsByDoctorId,
                    UpdateAppointmentStatusById,
                    GetAllPatientsByDoctorId,
                    GetDoctorById,
                    UpdateDoctorProfileById
                },
                Roles.Patient => new List<string>()
                {
                    GetAllDepartments,
                    GetAllDoctorsByDepartmentId,
                    CreateAppointment,
                    CancelAppointmentById,
                    GetAllAppointmentsByPatientId,
                    UpdateAppointmentById,
                    GetPatientById,
                    UpdatePatientProfileById
                },
                _ => new List<string>()
            };

            allPermissions.AddRange(result);

            return allPermissions;
        }
    }
}
