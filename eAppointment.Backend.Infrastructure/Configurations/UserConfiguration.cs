using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName).HasColumnType("varchar(50)");

            builder.Property(p => p.LastName).HasColumnType("varchar(50)");

            builder.Property(p => p.Email).HasColumnType("varchar(150)");

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(p => p.UserName).HasColumnType("varchar(100)");

            builder.HasIndex(x => x.UserName).IsUnique();

            builder.Property(p => p.PhoneNumber).HasColumnType("varchar(50)");

            builder.Property(p => p.RoleId).HasColumnType("int");

            builder.HasIndex(x => x.RoleId).IsUnique(false);

            builder
               .HasOne(e => e.Role)
               .WithOne(e => e.User)
               .HasForeignKey<User>(e => e.RoleId);
        }
    }
}
