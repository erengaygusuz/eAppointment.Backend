using eAppointment.Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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
                        PhoneNumber = "5555555555"
                    }, "12345");

                    if (result.Succeeded)
                    {
                        var addedUser = await userManager.FindByEmailAsync("gaygusuzeren@gmail.com");

                        await userManager.AddToRoleAsync(addedUser!, "Admin");
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
                    await roleManager.CreateAsync(new()
                    {
                        Name = "SuperAdmin"
                    });

                    await roleManager.CreateAsync(new()
                    {
                        Name = "Admin"
                    });

                    await roleManager.CreateAsync(new()
                    {
                        Name = "Doctor"
                    });

                    await roleManager.CreateAsync(new()
                    {
                        Name = "Patient"
                    });
                }
            }
        }
    }
}
