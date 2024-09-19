using eAppointment.Backend.Application;
using eAppointment.Backend.Infrastructure;
using eAppointment.Backend.Infrastructure.Services;
using eAppointment.Backend.WebAPI.Filters;
using eAppointment.Backend.WebAPI.Helpers;
using eAppointment.Backend.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text;

namespace eAppointment.Backend.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.Providers.Add<BrotliCompressionProvider>();
                opt.Providers.Add<GzipCompressionProvider>();
                opt.MimeTypes = ResponseCompressionDefaults.MimeTypes;
            });

            builder.Services.Configure<BrotliCompressionProviderOptions>(opt =>
            {
                opt.Level = System.IO.Compression.CompressionLevel.Fastest;
            });

            builder.Services.Configure<GzipCompressionProviderOptions>(opt =>
            {
                opt.Level = System.IO.Compression.CompressionLevel.SmallestSize;
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<HttpContextEnricher>();

            builder.Host.UseSerilog((context, serviceProvider, loggerConfig) => 
            {
                var enricher = serviceProvider.GetRequiredService<HttpContextEnricher>();

                loggerConfig.Enrich.With(enricher);

                loggerConfig.ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                DotNetEnv.Env.Load();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer"),
                    ValidAudience = Environment.GetEnvironmentVariable("Jwt__Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt__SecretKey") ?? ""))
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            builder.Services.AddCors(options =>
            {
                DotNetEnv.Env.Load();

                options.AddPolicy("CorsSettings",
                    builder =>
                    {
                        builder.WithOrigins(
                            Environment.GetEnvironmentVariable("CORS__Origin1"),
                            Environment.GetEnvironmentVariable("CORS__Origin2"),
                            Environment.GetEnvironmentVariable("CORS__Origin3"))
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddLocalization();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("tr-TR")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup =>
            {
                var jwtSecuritySheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
            });

            var app = builder.Build();

            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("CorsSettings");

            app.UseHttpsRedirection();

            var supportedCultures = new[] { "en-US", "tr-TR" };

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMiddleware<EncryptionDecryptionMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.MapControllers();

            Helper.CreateRolesAsync(app).Wait();
            Helper.CreateUserAsync(app).Wait();

            app.Run();
        }
    }
}
