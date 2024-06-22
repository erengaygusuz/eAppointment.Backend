using Microsoft.AspNetCore.Identity;

namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Role : IdentityRole<int>
    {
        public User? User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }
    }
}
