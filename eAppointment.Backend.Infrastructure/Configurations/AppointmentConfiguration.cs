using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DoctorId).HasColumnType("uniqueidentifier");

            builder.Property(p => p.PatientId).HasColumnType("uniqueidentifier");

            builder.Property(p => p.StartDate).HasColumnType("datetime");

            builder.Property(p => p.EndDate).HasColumnType("datetime");

            builder.Property(p => p.Status)
                .HasConversion(v => v.Value, v => AppointmentStatus.FromValue(v))
                .HasColumnName("Status");

            builder
                .HasOne(d => d.Doctor)
                .WithMany(a => a.Appointments)
                .HasForeignKey(e => e.DoctorId)
                .IsRequired();

            builder
                .HasOne(d => d.Patient)
                .WithMany(a => a.Appointments)
                .HasForeignKey(e => e.PatientId)
                .IsRequired();
        }
    }
}
