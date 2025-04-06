using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 4_Seed with custom claims
    {
        
    }
}
----------------------------------------------------
 Identity Seeding with Custom Claims — jaise tum Admin user ke saath kuch extra information attach karna chahte ho: like "Permission": "CanDeleteUser" ya "Department": "HR" — wo sab Claims ke through hota hai.

🔐 Real Use-Case
You want to:

Seed roles: Admin, User

Create default Admin user

Assign role: Admin

Add custom claims like:

"Permission": "CanDeleteUser"

"Department": "Management"

✅ Step-by-Step: Seeding Custom Claims
✅ Step 1: Identity Setup (Same as before)
Ensure you're using IdentityDbContext<IdentityUser> (or your custom ApplicationUser).
-------------------------------------------------------------
✅ Step 2: Extend Seeder Method with Claims
Update your seeding logic to add custom claims:


public static class IdentitySeed
{
    public static async Task SeedRolesAndAdminWithClaimsAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // 1. Seed Roles
        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Seed Admin User
        var adminEmail = "admin@site.com";
        var adminPassword = "Admin@123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
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
                await userManager.AddToRoleAsync(adminUser, "Admin");

                // 3. Add Custom Claims
                var claims = new List<Claim>
                {
                    new Claim("Permission", "CanDeleteUser"),
                    new Claim("Department", "Management"),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                foreach (var claim in claims)
                {
                    await userManager.AddClaimAsync(adminUser, claim);
                }
            }
        }
    }
}
-------------------------------------------
✅ Step 3: Call This in Program.cs

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeed.SeedRolesAndAdminWithClaimsAsync(services);
}
------------------------------------------------
🔎 How to Use These Claims in Code?

var hasClaim = User.HasClaim("Permission", "CanDeleteUser");
OR

var dept = User.FindFirst("Department")?.Value;
---------------------------------------
📦 Bonus Tip: Seed Role Claims
You can also add claims directly to roles using RoleManager<IdentityRole>:

await roleManager.AddClaimAsync(adminRole, new Claim("Permission", "CanManageAll"));
----------------------------------------------
✅ Summary
Task	Done
Seed Roles	✅
Create Admin User	✅
Assign Role	✅
Add Custom Claims	✅
Use in Controller/View	✅
