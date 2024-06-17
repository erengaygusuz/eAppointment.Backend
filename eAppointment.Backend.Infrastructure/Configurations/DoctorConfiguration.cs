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

            builder.Property(p => p.UserId).HasColumnType("uniqueidentifier");

            builder.Property(p => p.DepartmentId).HasColumnType("uniqueidentifier");

            builder
                .HasOne(e => e.User)
                .WithOne(e => e.Doctor)
                .HasForeignKey<Doctor>(e => e.UserId)
                .IsRequired();

            builder
                .HasOne(e => e.Department)
                .WithOne(e => e.Doctor)
                .HasForeignKey<Doctor>(e => e.DepartmentId)
                .IsRequired();
        }
    }
}
