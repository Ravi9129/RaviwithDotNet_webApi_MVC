using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 3_Data Seeding with Identity
    {
        
    }
}
--------------------------------------------------------
Data Seeding with Identity — iska matlab hai jab hum ASP.NET Core Identity use karte hain (for login, roles, users etc.), to default user (jaise Admin) aur default roles (Admin, User, Manager) automatically create karwana.
------------------------------------------------------
🔐 Real-World Scenario
Socho tum ek admin dashboard bana rahe ho. First time jab app chale,
 to ek admin@site.com email wala Admin user aur role Admin database me hona chahiye. Ye hi hai Identity Seeding.
-------------------
⚙️ Step by Step Guide — Data Seeding with Identity
✅ Step 1: Identity Setup Hona Chahiye
-------------------
Ensure your project is already using:

dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
------------------------------------------
And your DbContext inherits from IdentityDbContext:

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
------------------------------------------------------
✅ Step 2: Create a Seed Method
Yeh method roles and default users banayegi:

public static class IdentitySeed
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // 1. Roles
        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Admin User
        var adminEmail = "admin@site.com";
        var adminPassword = "Admin@123";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
--------------------------------------------
✅ Step 3: Call This in Program.cs

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeed.SeedRolesAndAdminAsync(services);
}
--------------------------------------------
⚠️ await use karne ke liye Main ko async bana do:

public static async Task Main(string[] args)
-------------------------------------
📦 Optional: Seeding More Users/Roles
You can also seed:

Manager role

Test users with roles

Admin claims
--------------------------------------------
✅ Checkpoint: Verify Seed
Start your app, and check DB:

Email	Role
admin@site.com	Admin
-------------------------------------
Bonus: If You Have a Custom IdentityUser

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
----------------------------------
Then modify accordingly:

var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
---------------------------------------
✅ Summary
Task	Done
Create Roles	✅
Create Admin User	✅
Assign Role to User	✅
Call Seeder in Program	✅