using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(k => new { k.UserId, k.RoleId });

            builder
                .HasOne(d => d.User)
                .WithMany(a => a.Roles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder
                .HasOne(d => d.Role)
                .WithMany(a => a.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
