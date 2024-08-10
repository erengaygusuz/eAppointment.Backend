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

            builder.Property(p => p.Message).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.MessageTemplate).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.Level).HasColumnType("nvarchar(128)");

            builder.Property(p => p.TimeStamp).HasColumnType("datetime");

            builder.Property(p => p.Exception).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.Properties).HasColumnType("nvarchar(MAX)");

            builder.Property(p => p.LogEvent).HasColumnType("xml");
        }
    }
}
