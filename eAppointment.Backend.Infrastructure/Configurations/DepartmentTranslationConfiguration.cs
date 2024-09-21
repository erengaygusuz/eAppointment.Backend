using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class DepartmentTranslationConfiguration : IEntityTypeConfiguration<DepartmentTranslation>
    {
        public void Configure(EntityTypeBuilder<DepartmentTranslation> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.LanguageId).HasColumnType("int");

            builder.HasIndex(x => x.LanguageId).IsUnique(false);

            builder.Property(p => p.DepartmentId).HasColumnType("int");

            builder.HasIndex(x => x.DepartmentId).IsUnique(false);

            builder.Property(p => p.TranslationText).HasColumnType("varchar(100)");

            builder
                .HasOne<Department>(d => d.Department)
                .WithMany(a => a.Translations)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder
                .HasOne<Language>(d => d.Language)
                .WithMany(a => a.DepartmentTranslations)
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
