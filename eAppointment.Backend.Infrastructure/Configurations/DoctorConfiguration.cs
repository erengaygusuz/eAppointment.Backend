using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).HasColumnType("int");

            builder.HasIndex(x => x.UserId).IsUnique(false);

            builder.Property(p => p.DepartmentId).HasColumnType("int");

            builder.HasIndex(x => x.DepartmentId).IsUnique(false);

            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.ModifiedDate).HasColumnType("datetime");

            builder
                .HasOne(e => e.User)
                .WithOne(e => e.Doctor)
                .HasForeignKey<Doctor>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(e => e.Department)
                .WithOne(e => e.Doctor)
                .HasForeignKey<Doctor>(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
