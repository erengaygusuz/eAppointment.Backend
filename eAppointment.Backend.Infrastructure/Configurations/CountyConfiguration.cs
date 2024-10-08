﻿using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class CountyConfiguration : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CityId).HasColumnType("int");

            builder.HasIndex(x => x.CityId).IsUnique(false);

            builder.Property(p => p.Name).HasColumnType("varchar(50)");

            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.ModifiedDate).HasColumnType("datetime");

            builder
                .HasOne(e => e.City)
                .WithMany(e => e.Counties)
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
