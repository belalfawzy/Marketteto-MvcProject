using Marketteto.Data.StaticMember;
using Marketteto.Models;
using Microsoft.AspNetCore.Identity;

namespace Marketteto.Data.Seed
{
    public class AppDbInitializer
    {
        public static async Task SeedUserAndRolesAsync(IApplicationBuilder builder)
        {
            using(var applicationService = builder.ApplicationServices.CreateScope())
            {
                var roleManager = applicationService.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Admin });
                }
                if(!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                var userManager = applicationService.ServiceProvider.GetRequiredService<UserManager<User>>();
                if(await userManager.FindByEmailAsync("admin@admin.com") == null)
                {
                    var adminUser = new User()
                    {
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                        FullName = "Admin User",
                        UserName = "Admin"
                    };
                    var result = await userManager.CreateAsync(adminUser, "Admin123@");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                        await userManager.AddToRoleAsync(adminUser, UserRoles.User);
                    }
                }

                if (await userManager.FindByEmailAsync("user@user.com") == null)
                {
                    var user = new User()
                    {
                        Email = "user@user.com",
                        EmailConfirmed = true,
                        FullName = "User",
                        UserName = "User"
                    };
                    var result = await userManager.CreateAsync(user, "Us@r123");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.User);
                    }
                }
            }

            
        }
    }
}
