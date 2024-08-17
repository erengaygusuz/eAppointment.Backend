using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;
using eAppointment.Backend.Domain.Helpers;
using GenericRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eAppointment.Backend.Infrastructure.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<User, Role, int>, IUnitOfWork
    {
        public DbSet<County> Counties { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<TableLog> TableLogs { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseCollation("Turkish_CI_AS");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var tableChangeEntries = new List<TableChangeEntry>();

            AuditLog auditLog = null;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ErrorLog || entry.Entity is AuditLog)
                {
                    continue;
                }

                if (entry.Entity is TableLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var tableChangeEntry = new TableChangeEntry(entry);

                tableChangeEntry.TableName = entry.Entity.GetType().Name;

                tableChangeEntries.Add(tableChangeEntry);

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

                        tableChangeEntry.KeyValues[propertyName] = property.CurrentValue;

                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:

                            auditLog = AuditLogs.OrderByDescending(e => e.Id).FirstOrDefault();

                            tableChangeEntry.TableChangeType = TableChangeType.Create;
                            tableChangeEntry.NewValues[propertyName] = property.CurrentValue;

                            break;

                        case EntityState.Deleted:

                            auditLog = AuditLogs.OrderByDescending(e => e.Id).FirstOrDefault();

                            tableChangeEntry.TableChangeType = TableChangeType.Delete;
                            tableChangeEntry.OldValues[propertyName] = property.OriginalValue;

                            break;

                        case EntityState.Modified:

                            auditLog = AuditLogs.OrderByDescending(e => e.Id).FirstOrDefault();

                            if (property.IsModified)
                            {
                                tableChangeEntry.AffectedColumns.Add(propertyName);
                                tableChangeEntry.TableChangeType = TableChangeType.Update;
                                tableChangeEntry.OldValues[propertyName] = property.OriginalValue;
                                tableChangeEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var tableChangeEntry in tableChangeEntries)
            {
                TableLogs.AddAsync(tableChangeEntry.ToTableLog(auditLog.Id));
            }
        }
    }
}
