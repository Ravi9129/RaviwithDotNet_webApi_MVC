using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 6_Claim-based Authorization
    {
        
    }
}
----------------------------------------------------------
Claim-based Authorization ki taraf – jo ki fine-grained control deta hai user ke access par, based on claims, instead of just roles.
--------------------------------------------------------
🧠 Pehle ye samajh:
Claims are key-value pairs that represent user information (like "Department": "HR", "Permission": "CanEdit").
They’re more flexible than roles and can come from tokens, identity providers, or manually added.

🔥 Real-life Example
“Only users with a claim of Department = HR can access the HR Dashboard.”
---------------------------------------------------------------
✅ Step-by-Step Claim-based Authorization in ASP.NET Core
✅ Step 1: Add Claim to a User
Usually done during user creation or seeding.

await userManager.AddClaimAsync(user, new Claim("Department", "HR"));
await userManager.AddClaimAsync(user, new Claim("Permission", "CanEdit"));
---------------------------------------
✅ Step 2: Define Policy in Program.cs or Startup.cs

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HRDepartmentOnly", policy =>
        policy.RequireClaim("Department", "HR"));

    options.AddPolicy("CanEditOnly", policy =>
        policy.RequireClaim("Permission", "CanEdit"));
});
---------------------------------------------
✅ Step 3: Use Policy in Controller or Action

[Authorize(Policy = "HRDepartmentOnly")]
public IActionResult HRDashboard()
{
    return View();
}
-------------------------------------------------
Or apply globally:

services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter("HRDepartmentOnly"));
});
-----------------------------------------------
✅ Step 4: Check Claim in Code (Manually)

if (User.HasClaim("Department", "HR"))
{
    // logic for HR user
}
----------------------------------------------
✅ Bonus: Custom Requirement for Complex Logic
If you need complex checks, like multiple claims:
-----------------------
🧱 Create Requirement

public class DepartmentRequirement : IAuthorizationRequirement
{
    public string Department { get; }

    public DepartmentRequirement(string department)
    {
        Department = department;
    }
}
------------------------------------------
🔧 Create Handler

public class DepartmentHandler : AuthorizationHandler<DepartmentRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        DepartmentRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "Department" && c.Value == requirement.Department))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
-------------------------------------
✅ Register It

services.AddSingleton<IAuthorizationHandler, DepartmentHandler>();

services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyHR", policy =>
        policy.Requirements.Add(new DepartmentRequirement("HR")));
});
------------------------------------------------
Use in Controller

[Authorize(Policy = "OnlyHR")]
public IActionResult ConfidentialHRData() => View();
-------------------------------------------------------------------
📌 Recap Table (No table 😉 — listed format)
✅ Claims = User attributes (key-value pairs)

✅ Add claims via UserManager

✅ Use RequireClaim() in policy

✅ Use [Authorize(Policy = "...")] to enforce

✅ Create custom IAuthorizationHandler for logic-based claims
-------------------------------------------------------------
🧪 Useful Methods
User.HasClaim("Permission", "CanEdit")

User.FindFirst("Department")?.Value

