using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eAppointment.Backend.Infrastructure.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<County> Counties { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<MenuItemTranslation> MenuItemTranslations { get; set; }

        public DbSet<DepartmentTranslation> DepartmentTranslations { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseCollation("Turkish_CI_AS");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateChangeTracker();

            OnBeforeSaveChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry);

                auditEntry.TableName = entry.Entity.GetType().Name;

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        int propValue = (int)property.CurrentValue;

                        if (propertyName == "Id" && propValue < 0)
                        {
                            property.CurrentValue = 0;
                        }

                        auditEntry.KeyValues[propertyName] = property.CurrentValue;

                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:

                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;

                            break;

                        case EntityState.Deleted:

                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;

                            break;

                        case EntityState.Modified:

                            if (property.IsModified)
                            {
                                auditEntry.AffectedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.AddAsync(auditEntry.ToAuditLog());
            }
        }

        public void UpdateChangeTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                BaseEntity entityReference = null;
                User userReference = null;
                Role roleReference = null;

                if (item.Entity is BaseEntity)
                {
                    entityReference = (BaseEntity)item.Entity;
                }

                else if (item.Entity is User)
                {
                    userReference = (User)item.Entity;
                }

                else if (item.Entity is Role)
                {
                    roleReference = (Role)item.Entity;
                }

                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            if (entityReference != null)
                            {
                                Entry(entityReference).Property(x => x.ModifiedDate).IsModified = false;
                                entityReference.CreatedDate = DateTime.Now;
                            }

                            if (userReference != null)
                            {
                                Entry(userReference).Property(x => x.ModifiedDate).IsModified = false;
                                userReference.CreatedDate = DateTime.Now;
                            }

                            if (roleReference != null)
                            {
                                Entry(roleReference).Property(x => x.ModifiedDate).IsModified = false;
                                roleReference.CreatedDate = DateTime.Now;
                            }

                            break;
                        }
                    case EntityState.Modified:
                        {
                            if (entityReference != null)
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.ModifiedDate = DateTime.Now;
                            }

                            if (userReference != null)
                            {
                                Entry(userReference).Property(x => x.CreatedDate).IsModified = false;
                                userReference.ModifiedDate = DateTime.Now;
                            }

                            if (roleReference != null)
                            {
                                Entry(roleReference).Property(x => x.CreatedDate).IsModified = false;
                                roleReference.ModifiedDate = DateTime.Now;
                            }

                            break;
                        }
                }
            }

        }
    }
}
