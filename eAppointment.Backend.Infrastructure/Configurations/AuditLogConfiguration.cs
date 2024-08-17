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

            builder.Property(p => p.Method).HasColumnType("varchar(10)");

            builder.Property(p => p.Url).HasColumnType("nvarchar(2048)");

            builder.Property(p => p.Path).HasColumnType("nvarchar(2048)");

            builder.Property(p => p.QueryParameters).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.RequestHeaders).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.RequestBody).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.StatusCode).HasColumnType("int");

            builder.Property(p => p.ResponseHeaders).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.ResponseBody).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.UserName).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.RemoteIpAddress).HasColumnType("nvarchar(45)");

            builder.Property(p => p.LocalIpAddress).HasColumnType("varchar(45)");

            builder.Property(p => p.RemotePort).HasColumnType("int");

            builder.Property(p => p.LocalPort).HasColumnType("int");

            builder.Property(p => p.Timestamp).HasColumnType("datetime");
        }
    }
}
