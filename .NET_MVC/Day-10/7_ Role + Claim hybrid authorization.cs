using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 7_ Role + Claim hybrid authorization
    {
        
    }
}
---------------------------------------------------
Role + Claim hybrid authorization ki — jisme dono ko mila ke more secure & fine-grained control diya jata hai 🔐.

🔥 Scenario Example:
✅ User must be in the "Admin" role
✅ AND must have the claim "Permission" = "CanDelete"

✅ Step-by-Step Setup
---------------------------------------------------------------------- 
1️⃣ Seed Roles + Claims to User

await userManager.AddToRoleAsync(user, "Admin");
await userManager.AddClaimAsync(user, new Claim("Permission", "CanDelete"));
---------------------------------
2️⃣ Create Custom Requirement (Hybrid Check)

public class RoleAndClaimRequirement : IAuthorizationRequirement
{
    public string Role { get; }
    public string ClaimType { get; }
    public string ClaimValue { get; }

    public RoleAndClaimRequirement(string role, string claimType, string claimValue)
    {
        Role = role;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }
}
---------------------------------------------------
3️⃣ Create Authorization Handler

public class RoleAndClaimHandler : AuthorizationHandler<RoleAndClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RoleAndClaimRequirement requirement)
    {
        var hasRole = context.User.IsInRole(requirement.Role);
        var hasClaim = context.User.HasClaim(requirement.ClaimType, requirement.ClaimValue);

        if (hasRole && hasClaim)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
------------------------------------------------
4️⃣ Register Policy and Handler in Program.cs

builder.Services.AddSingleton<IAuthorizationHandler, RoleAndClaimHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminWithDeletePermission", policy =>
        policy.Requirements.Add(new RoleAndClaimRequirement("Admin", "Permission", "CanDelete")));
});
-------------------------------------------------
5️⃣ Use in Controller or Action

[Authorize(Policy = "AdminWithDeletePermission")]
public IActionResult DeleteSomething()
{
    return View();
}
---------------------------------------------
✅ Bonus: Check manually if needed

if (User.IsInRole("Admin") && User.HasClaim("Permission", "CanDelete"))
{
    // Allow delete
}
--------------------------------------------
📌 Recap:
🔍 Check	✅ How
Role	User.IsInRole("Admin")
Claim	User.HasClaim("Permission", "...")
Hybrid Policy	Custom IAuthorizationRequirement
