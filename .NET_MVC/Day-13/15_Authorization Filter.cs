using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 15_Authorization Filter
    {
        
    }
}
------------------------------------------
Authorization Filter ki — ye ek security-focused filter hota hai jo controller ya 
action method ko execute hone se pehle hi authorize karta hai. 
Matlab yahi decide karta hai ki user ko aage jaane dena hai ya nahi.

🔐 What is an Authorization Filter?
Authorization Filter woh hota hai jo request pipeline ke sabse 
shuruat me (almost first) run karta hai aur verify karta hai ki user ke paas required permission hai ya nahi.

Ye filter action execute hone se pehle decision le leta hai:

Allow karna hai

Ya block karna hai (403 Forbidden ya redirect to login)
-------------------------------------------------------
🔄 Filter Order (Simplified)

1. Authorization Filter 🔒
2. Resource Filter ⏱️
3. Action Filter 🎯
4. Result Filter 📦
5. Exception Filter ❌
✨ Real-World Example:
Mall ke gate pe security guard (Authorization Filter) decide karta hai ki tu VIP band hai ya normal banda. 
Agar VIP pass hai to andar jaa. Nahi to return back. Baaki sab baad me hota hai.
--------------------------------------------------
🛠️ Interfaces Used:
IAuthorizationFilter – synchronous

IAsyncAuthorizationFilter – asynchronous
----------------------------------------------------
🔧 Example: Custom Authorization Filter

public class CustomAuthFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isAuthorized = context.HttpContext.User.Identity.IsAuthenticated;

        if (!isAuthorized)
        {
            context.Result = new UnauthorizedResult(); // 401 Unauthorized
        }
    }
}
---------------------------------------------------
🔧 Example: Check Specific Role

public class AdminOnlyFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.IsInRole("Admin"))
        {
            context.Result = new ForbidResult(); // 403 Forbidden
        }
    }
}
🚀 Registering the Filter
--------------------------------------
✅ Globally:

services.AddControllers(config =>
{
    config.Filters.Add<CustomAuthFilter>();
});
-----------------------------------------
✅ On Controller or Action:

[ServiceFilter(typeof(AdminOnlyFilter))]
public class AdminController : Controller
-----------------------------------------------
🔄 Alternative: Using [Authorize] Attribute
ASP.NET Core already provides a built-in attribute:

[Authorize(Roles = "Admin")]
public IActionResult SecretAction() { }
But agar custom logic chahiye (like checking JWT claims or headers), tab IAuthorizationFilter helpful hai.
--------------------------------------------------
🧠 Advanced Example: API Key Authorization

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly string apiKey = "supersecret123";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var key) ||
            key != apiKey)
        {
            context.Result = new ContentResult
            {
                StatusCode = 401,
                Content = "API Key is invalid or missing"
            };
        }
    }
}
--------------------------------------------------------
🧪 Real Scenario Use-Cases:
Scenario	Why Authorization Filter?
Admin-only section	Check for roles or custom claims
API Key security	Filter requests based on API key
Custom tenant-based auth	Multi-tenant checks before action executes
Secure 3rd party integrations	Validate tokens or headers
------------------------------------------------------
📝 Summary:
Authorization Filters are first gatekeepers

Execute before model binding or action

Used to check:

Authentication (logged in)

Authorization (roles/claims/headers)

Return UnauthorizedResult (401) or ForbidResult (403) to block