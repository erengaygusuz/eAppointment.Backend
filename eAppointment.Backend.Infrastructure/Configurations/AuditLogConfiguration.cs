using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TimeStamp).HasColumnType("datetime");

            builder.Property(p => p.Method).HasColumnType("varchar(7)");

            builder.Property(p => p.Path).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.QueryString).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.RequestBody).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.ResponseBody).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.StatusCode).HasColumnType("int");

            builder.Property(p => p.Headers).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.IPAddress).HasColumnType("varchar(40)");
        }
    }
}
