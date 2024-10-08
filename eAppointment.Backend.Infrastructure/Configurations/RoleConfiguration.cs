﻿using eAppointment.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Backend.Infrastructure.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasColumnType("varchar(50)");

            builder.Navigation(p => p.MenuItems).AutoInclude();

            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.ModifiedDate).HasColumnType("datetime");
        }
    }
}
