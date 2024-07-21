using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums
{
    public sealed class AppointmentStatus : SmartEnum<AppointmentStatus>
    {
        public static readonly AppointmentStatus SuccessfullyCompleted = new("SuccessfullyCompleted", 1);
        public static readonly AppointmentStatus NotAttended = new("NotAttended", 2);
        public static readonly AppointmentStatus Cancelled = new("Cancelled", 3);
        public static readonly AppointmentStatus NotCompleted = new("NotCompleted", 4);

        public AppointmentStatus(string name, int value) : base(name, value)
        {
        }
    }
}
