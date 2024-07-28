using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminById;
using eAppointment.Backend.Application.Features.Appointments.CreateAppointment;
using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById;
using eAppointment.Backend.Application.Features.Auth.Login;
using eAppointment.Backend.Application.Features.Doctors.CreateDoctor;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById;
using eAppointment.Backend.Application.Features.Patients.CreatePatient;
using eAppointment.Backend.Application.Features.Patients.UpdatePatientById;
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
            services.AddScoped<IValidator<CreateAppointmentCommand>, CreateAppointmentCommandValidator>();
            services.AddScoped<IValidator<CreateDoctorCommand>, CreateDoctorCommandValidator>();
            services.AddScoped<IValidator<CreatePatientCommand>, CreatePatientCommandValidator>();
            services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
            services.AddScoped<IValidator<UpdateAdminByIdCommand>, UpdateAdminByIdCommandValidator>();
            services.AddScoped<IValidator<UpdateAppointmentByIdCommand>, UpdateAppointmentByIdCommandValidator>();
            services.AddScoped<IValidator<UpdateDoctorByIdCommand>, UpdateDoctorByIdCommandValidator>();
            services.AddScoped<IValidator<UpdatePatientByIdCommand>, UpdatePatientByIdCommandValidator>();

            services.AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
            });

            return services;
        }
    }
}
