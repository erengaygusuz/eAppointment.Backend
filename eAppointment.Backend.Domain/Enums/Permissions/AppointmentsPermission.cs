using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class AppointmentsPermission : SmartEnum<AppointmentsPermission>
    {
        public static readonly AppointmentsPermission CancelAppointmentById = new("Permissions.Appointments.CancelAppointmentById", 1);

        public static readonly AppointmentsPermission CreateAppointment = 
            new("Permissions.Appointments.CreateAppointment", 2);

        public static readonly AppointmentsPermission GetAllAppointmentsByDoctorId = 
            new("Permissions.Appointments.GetAllAppointmentsByDoctorId", 3);

        public static readonly AppointmentsPermission GetAllAppointmentsByDoctorIdAndByStatus = 
            new("Permissions.Appointments.GetAllAppointmentsByDoctorIdAndByStatus", 4);

        public static readonly AppointmentsPermission GetAllAppointmentsByPatientId = 
            new("Permissions.Appointments.GetAllAppointmentsByPatientId", 5);

        public static readonly AppointmentsPermission GetAllAppointmentsByPatientIdAndByStatus = 
            new("Permissions.Appointments.GetAllAppointmentsByPatientIdAndByStatus", 6);

        public static readonly AppointmentsPermission UpdateAppointmentById = 
            new("Permissions.Appointments.UpdateAppointmentById", 7);

        public static readonly AppointmentsPermission UpdateAppointmentStatusById = 
            new("Permissions.Appointments.UpdateAppointmentStatusById", 8);

        public AppointmentsPermission(string name, int value) : base(name, value)
        {
        }
    }
}
