using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace eAppointment.Backend.WebAPI
{
    public static class Helper
    {
        public static async Task CreateUserAsync(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                if (!userManager.Users.Any())
                {
                    var result = await userManager.CreateAsync(new()
                    {
                        FirstName = "Eren",
                        LastName = "Gaygusuz",
                        Email = "gaygusuzeren@gmail.com",
                        UserName = "erengaygusuz",
                        PhoneNumber = "(555) 555-5555"
                    }, "12345");

                    if (result.Succeeded)
                    {
                        var addedUser = await userManager.FindByEmailAsync("gaygusuzeren@gmail.com");

                        await userManager.AddToRoleAsync(addedUser!, Roles.SuperAdmin);
                    }
                }
            }
        }

        public static async Task CreateRolesAsync(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                if (!roleManager.Roles.Any())
                {
                    var superAdminResult = await roleManager.CreateAsync(new()
                    {
                        Name = Roles.SuperAdmin
                    });

                    if (superAdminResult.Succeeded)
                    {
                        await AddClaimsToRolesAsync(roleManager, Roles.SuperAdmin);
                    }

                    var adminResult = await roleManager.CreateAsync(new()
                    {
                        Name = Roles.Admin
                    });

                    if (adminResult.Succeeded)
                    {
                        await AddClaimsToRolesAsync(roleManager, Roles.Admin);
                    }

                    var doctorResult = await roleManager.CreateAsync(new()
                    {
                        Name = Roles.Doctor
                    });

                    if (doctorResult.Succeeded)
                    {
                        await AddClaimsToRolesAsync(roleManager, Roles.Doctor);
                    }

                    var patientResult = await roleManager.CreateAsync(new()
                    {
                        Name = Roles.Patient
                    });

                    if (patientResult.Succeeded)
                    {
                        await AddClaimsToRolesAsync(roleManager, Roles.Patient);
                    }
                }
            }
        }

        private static async Task AddClaimsToRolesAsync(RoleManager<Role> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            var allClaims = await roleManager.GetClaimsAsync(role);

            var allPermissions = Permissions.GetAllPermissions(roleName);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
