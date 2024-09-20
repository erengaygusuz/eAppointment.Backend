using Microsoft.AspNetCore.Identity;

namespace eAppointment.Backend.Domain.Entities
{
    public sealed class Role : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public ICollection<Page> Pages { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
