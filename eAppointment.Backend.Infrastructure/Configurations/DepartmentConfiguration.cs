using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasColumnType("varchar(100)");

            builder.HasMany(e => e.Translations)
               .WithMany(e => e.Departments)
               .UsingEntity<DepartmentTranslation>(
                   l => l.HasOne(mt => mt.Language).WithMany().HasForeignKey(mt => mt.LanguageId).HasPrincipalKey(l => l.Id),
                   r => r.HasOne(mt => mt.Department).WithMany().HasForeignKey(mt => mt.DepartmentId).HasPrincipalKey(l => l.Id),
                   j =>
                   {
                       j.Property(mt => mt.TranslationText).HasColumnType("varchar(50)");
                       j.HasKey(mt => new { mt.DepartmentId, mt.LanguageId });
                   });
        }
    }
}
