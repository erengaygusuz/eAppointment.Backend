using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ExceptionMessage).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.ExceptionStackTrace).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.InnerExceptionMessage).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.InnerExceptionStackTrace).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.AuditLogId).HasColumnType("int");

            builder.HasIndex(x => x.AuditLogId).IsUnique(false);

            builder
                .HasOne(e => e.AuditLog)
                .WithOne(e => e.ErrorLog)
                .HasForeignKey<ErrorLog>(e => e.AuditLogId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
