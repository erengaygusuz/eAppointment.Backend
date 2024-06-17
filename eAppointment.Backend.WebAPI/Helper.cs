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

                if (!userManager.Users.Any())
                {
                    await userManager.CreateAsync(new()
                    {
                        FirstName = "Eren",
                        LastName = "Gaygusuz",
                        UserName = "gaygusuzeren@gmail.com"
                    }, "1");
                }
            }
        }
    }
}
