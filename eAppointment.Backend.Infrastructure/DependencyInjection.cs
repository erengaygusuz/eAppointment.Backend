using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Infrastructure.Concretes;
using eAppointment.Backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace eAppointment.Backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            DotNetEnv.Env.Load();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString__SqlServer"));
            });

            services.AddIdentity<User, Role>(action =>
            {
                action.Password.RequiredLength = 1;
                action.Password.RequireUppercase = false;
                action.Password.RequireLowercase = false;
                action.Password.RequireNonAlphanumeric = false;
                action.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.Scan(action => action
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }
    }
}
