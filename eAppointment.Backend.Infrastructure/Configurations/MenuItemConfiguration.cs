using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.MenuKey).HasColumnType("varchar(50)");

            builder.Property(p => p.ParentId).HasColumnType("int");

            builder.HasIndex(x => x.ParentId).IsUnique(false);

            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETDATE()");

            builder.HasMany(e => e.Roles)
               .WithMany(e => e.MenuItems)
               .UsingEntity(
                   "RoleMenuItemMapping",
                   l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
                   r => r.HasOne(typeof(MenuItem)).WithMany().HasForeignKey("MenuItemId").HasPrincipalKey(nameof(MenuItem.Id)),
                   j => j.HasKey("RoleId", "MenuItemId"));
        }
    }
}
