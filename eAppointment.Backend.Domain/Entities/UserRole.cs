using Microsoft.AspNetCore.Identity;

namespace eAppointment.Backend.Domain.Entities
{
    public sealed class UserRole : IdentityUserRole<Guid>
    {
        public User? User { get; set; }

        public Role? Role { get; set; }
    }
}
