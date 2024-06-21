using Microsoft.AspNetCore.Identity;

namespace eAppointment.Backend.Domain.Entities
{
    public sealed class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public Patient? Patient { get; set; }

        public Doctor? Doctor { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public Guid RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
