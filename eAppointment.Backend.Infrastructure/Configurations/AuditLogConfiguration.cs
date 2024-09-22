using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.AuditType)
                .HasConversion(v => v.Value, v => AuditType.FromValue(v))
                .HasColumnName("AuditType");

            builder.Property(p => p.TableName).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.OldValues).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.NewValues).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.AffectedColumns).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.KeyValues).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
        }
    }
}
