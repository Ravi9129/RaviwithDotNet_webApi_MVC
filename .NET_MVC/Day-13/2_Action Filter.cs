using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 2_Action Filter
    {
        
    }
}
------------------------------------------------------
Action Filter ko deep level pe samjhte hain — with definition, real-world usage, code examples, aur kaise banaate hain custom action filters — sab kuch easy aur explainative tarike se 💯

💡 What is an Action Filter?
Action Filter ASP.NET Core ka ek built-in extension point hai jo controller action methods ke execute hone se pehle aur baad execute hota hai.

Ye 2 methods provide karta hai:

OnActionExecuting: Action method se pehle chalta hai

OnActionExecuted: Action method ke baad chalta hai
----------------------------------------------------
📌 It helps to:

Logging

Validation

Performance measuring

Auditing

Authorization (custom rules)
---------------------------------------------------
🎯 Real-World Scenario
Soch tu ek Admin Panel bana raha hai jahan har action ke execution ka time log karna hai — taaki performance monitor kiya ja sake.

Ya tu har API ke request parameters ko log karna chahta hai.

✅ In dono cases me ActionFilter tere liye best hai!

🔨 Built-in Interfaces for Action Filters
ASP.NET Core me Action Filters banane ke liye 2 options hote hain:
------------------------------------------------------
1. IActionFilter (Synchronous)

public class MyActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Before action runs
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // After action runs
    }
}
-----------------------------------------------
2. IAsyncActionFilter (Asynchronous)

public class MyAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Before
        Console.WriteLine("Before action executes");

        var resultContext = await next();

        // After
        Console.WriteLine("After action executes");
    }
}
----------------------------------------------------------------
🧪 Custom Action Filter Example
✅ Step 1: Create a Custom Filter

public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[Before] Action: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"[After] Action: {context.ActionDescriptor.DisplayName}");
    }
}
--------------------------------
✅ Step 2: Register in Program.cs or Startup.cs

builder.Services.AddScoped<LogActionFilter>();
------------------------------------------------
✅ Step 3: Apply to Controller or Action

[ServiceFilter(typeof(LogActionFilter))]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
--------------------------------
🧠 Use Cases
Use Case	Why Use Action Filter?
Logging	Log request/response data or action execution time
Input Validation	Check model state before action executes
Audit Trail	Log which user called which action and when
Caching	Intercept and serve from cache
Custom Authorization Logic	Add role/claim-based restrictions
Measuring Performance	Stopwatch before and after action
-----------------------------------------
🛡️ Filter Execution Order
ASP.NET Core executes filters in this order:

Authorization Filters

Resource Filters
✅ Action Filters

Exception Filters

Result Filters

So Action Filters are specifically for intercepting around controller action method execution.
--------------------------------------------------
🧠 Pro Tip: Global Action Filters
Agar tu sabhi controllers ke liye apply karna chahta hai:

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LogActionFilter>();
});
