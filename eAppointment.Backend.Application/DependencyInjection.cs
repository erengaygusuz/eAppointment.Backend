using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace eAppointment.Backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddScoped<IValidator<CreateAdminCommand>, CreateAdminCommandValidator>();

            services.AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
            });

            return services;
        }
    }
}
