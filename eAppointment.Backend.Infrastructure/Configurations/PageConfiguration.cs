using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PageKey).HasColumnType("varchar(50)");

            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.ModifiedDate).HasColumnType("datetime");

            builder.HasMany(e => e.Roles)
               .WithMany(e => e.Pages)
               .UsingEntity(
                   "RolePageMappings",
                   l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
                   r => r.HasOne(typeof(Page)).WithMany().HasForeignKey("PageId").HasPrincipalKey(nameof(Page.Id)),
                   j => j.HasKey("RoleId", "PageId"));
        }
    }
}
