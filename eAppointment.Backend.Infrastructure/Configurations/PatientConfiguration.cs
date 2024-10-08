﻿using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).HasColumnType("int");

            builder.HasIndex(x => x.UserId).IsUnique(false);

            builder.Property(p => p.IdentityNumber).HasColumnType("varchar(11)");

            builder.HasIndex(x => x.IdentityNumber).IsUnique();

            builder.Property(p => p.FullAddress).HasColumnType("varchar(500)");

            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.ModifiedDate).HasColumnType("datetime");

            builder
                .HasOne(e => e.User)
                .WithOne(e => e.Patient)
                .HasForeignKey<Patient>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
