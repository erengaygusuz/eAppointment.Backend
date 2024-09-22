using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class MenuItemTranslationConfiguration : IEntityTypeConfiguration<MenuItemTranslation>
    {
        public void Configure(EntityTypeBuilder<MenuItemTranslation> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.LanguageId).HasColumnType("int");

            builder.HasIndex(x => x.LanguageId).IsUnique(false);

            builder.Property(p => p.MenuItemId).HasColumnType("int");

            builder.HasIndex(x => x.MenuItemId).IsUnique(false);

            builder.Property(p => p.TranslationText).HasColumnType("varchar(100)");

            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.ModifiedDate).HasColumnType("datetime");

            builder
                .HasOne<MenuItem>(d => d.MenuItem)
                .WithMany(a => a.MenuItemTranslations)
                .HasForeignKey(e => e.MenuItemId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder
                .HasOne<Language>(d => d.Language)
                .WithMany(a => a.MenuItemTranslations)
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Navigation(p => p.Language).AutoInclude();
        }
    }
}
