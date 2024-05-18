using eAppointment.Backend.Domain.Entities;

namespace eAppointment.Backend.Application
{
    public static class Constants
    {
        public static List<AppRole> GetRoles()
        {
            List<string> roles = new()
            {
                "Admin",
                "Doctor",
                "Patient"
            };

            return roles.Select(s => new AppRole()
            {
                Name = s,
            }).ToList();
        }
    }
}
