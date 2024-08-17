using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class TableLogConfiguration : IEntityTypeConfiguration<TableLog>
    {
        public void Configure(EntityTypeBuilder<TableLog> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TableChangeType)
                .HasConversion(v => v.Value, v => TableChangeType.FromValue(v))
                .HasColumnName("TableChangeType");

            builder.Property(p => p.TableName).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.OldValues).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.NewValues).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.AffectedColumns).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.KeyValues).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.AuditLogId).HasColumnType("int");

            builder.HasIndex(x => x.AuditLogId).IsUnique(false);

            builder
                .HasOne(e => e.AuditLog)
                .WithMany(e => e.TableLogs)
                .HasForeignKey(e => e.AuditLogId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
