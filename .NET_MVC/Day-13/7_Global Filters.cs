using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 7_Global Filters
    {
        
    }
}
--------------------------------------------
Global Filters ka scene dekhte hain — proper explanation ke saath, bina kisi boring table ke, real-world practical examples ke base pe.

📌 What are Global Filters?
Global filters woh filters hote hain jo poore application pe ek hi baar register kar diye jaate hain, aur phir har controller/action pe apply ho jaate hain automatically — bina bar bar likhe.
----------------------------------------------------
✅ Kab Use Karte Hain?
Jab tu chah raha ho ki koi common logic (jaise logging, error handling, authorization, response formatting) har controller/action pe apply ho:

Jaise: Request logging

Jaise: Exception logging

Jaise: Token validation

Jaise: Multitenancy

Jaise: Caching
------------------------------------------------------------
🔨 How to Register a Global Filter
Step 1: Create the Filter

public class GlobalLogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[GlobalFilter] Starting: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"[GlobalFilter] Ended: {context.ActionDescriptor.DisplayName}");
    }
}
-----------------------------------------
Step 2: Register in Startup.cs or Program.cs (ASP.NET Core 6+)
If using Program.cs (minimal hosting):

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalLogActionFilter>();
});
-------------------------------------
Ya agar tu ActionFilterAttribute use kar raha:

public class GlobalLogFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("[Global Attribute Filter] Action Starting");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("[Global Attribute Filter] Action Ended");
    }
}
-----------------------------
options.Filters.Add(new GlobalLogFilterAttribute());
-----------------------------------------
🧠 Real-World Scenario
Soch: Tu ek large scale application bana raha — jahan audit logging har action ke start/end pe chahiye (e.g., for security compliance).

Tu har controller me [AuditLog] likhna nahi chahta.

Solution? — Ek global filter laga de.
--------------------------------------------------
💡 Use with Dependency Injection
Agar tu DI services use kar raha (jaise logger ya database access), toh tu normal class banake filter inject kar sakta hai.

public class GlobalLoggerFilter : IActionFilter
{
    private readonly ILogger<GlobalLoggerFilter> _logger;

    public GlobalLoggerFilter(ILogger<GlobalLoggerFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Action Starting: " + context.ActionDescriptor.DisplayName);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action Ended: " + context.ActionDescriptor.DisplayName);
    }
}
--------------------------------------
Register:

options.Filters.Add<GlobalLoggerFilter>();
--------------------------------------------------------
🧪 Bonus: Global Exception Handling via Filter

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Unhandled exception occurred.");
        context.Result = new JsonResult(new { error = "Something went wrong." })
        {
            StatusCode = 500
        };
    }
}
-----------------------------------------------
Register it globally too:

options.Filters.Add<GlobalExceptionFilter>();
------------------------------------------------------
🎯 Summary
Global Filters apply across all controllers and actions.

Best for shared concerns like logging, error handling, validation.

You can register class-based filters using DI, or attribute-based filters directly.

Clean code — no repetition — centralized logic 🔥