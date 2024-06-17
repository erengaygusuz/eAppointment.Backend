using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums
{
    public sealed class AppointmentStatus : SmartEnum<AppointmentStatus>
    {
        public static readonly AppointmentStatus SuccessfullyCompleted = new("Successfully Completed", 1);
        public static readonly AppointmentStatus NotAttend = new("Not Attend", 2);
        public static readonly AppointmentStatus Canceled = new("Canceled", 3);
        public static readonly AppointmentStatus NotCompleted = new("Not Completed", 4);

        public AppointmentStatus(string name, int value) : base(name, value)
        {
        }
    }
}
