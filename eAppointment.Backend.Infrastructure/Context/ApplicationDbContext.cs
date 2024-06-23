using eAppointment.Backend.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace eAppointment.Backend.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : IdentityDbContext<User, Role, int>, IUnitOfWork
    {
        public DbSet<County> Counties { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseCollation("Turkish_CI_AS");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
