using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnost.Infrastructure;

public static class DbInit
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {   
        UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        IConfiguration cfg = serviceProvider.GetRequiredService<IConfiguration>();

        const string adminRoleName = "admin";
        string? adminEmail = cfg["Diagnost:DefaultAdminEmail"];
        string? adminPassword = cfg["Diagnost:DefaultAdminPassword"];

        if (string.IsNullOrEmpty((adminEmail)) || string.IsNullOrEmpty((adminPassword)))
        {
            Console.WriteLine("Error while getting default admin user from the appsettings");
            return;
        }

        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRoleName));
        }

        IdentityUser adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error creating user: {error.Description}");
                }
            }
        }
    }
}