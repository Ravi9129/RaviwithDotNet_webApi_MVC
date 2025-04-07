using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 11_async filters
    {
        
    }
}
-------------------------------------
async filters ek next-level concept hai jo modern ASP.NET Core apps mein zaroori ho gaya hai,
 specially jab tu async calls karta hai (like DB access, HTTP calls, etc). Chal step-by-step samajh lete hain bina kisi table ke,
  real-world examples ke saath:
------------------------------------
🔍 What is an Async Filter?
Async filter ek aisa custom filter hota hai jo awaitable operations (jaise ki database se data lena, 
logging to external service, etc.) ko support karta hai.
--------------------------------
ASP.NET Core mein tu async filters create kar sakta hai by implementing:

IAsyncActionFilter

IAsyncResultFilter

IAsyncResourceFilter

IAsyncExceptionFilter

Hum filhal IAsyncActionFilter ko focus karenge, kyunki yeh sabse commonly used hota hai.
---------------------------------------------------
💡 Real World Scenario
Tu chah raha hai ke har action ke execution ke pehle:

External logging service ko await karke request log kare,

Aur action ke baad bhi response log kare.

Yeh tabhi ho sakta hai jab filter async ho — warna blocking ho jaayega.
--------------------------------------------------
🔧 Syntax: How to Create an Async Action Filter

using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

public class MyAsyncLoggingFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Before Action executes
        await LogBeforeAsync(context);

        // Call the action
        var resultContext = await next();

        // After Action executes
        await LogAfterAsync(resultContext);
    }

    private async Task LogBeforeAsync(ActionExecutingContext context)
    {
        await Task.Delay(50); // simulate async logging
        Console.WriteLine("Logging before action");
    }

    private async Task LogAfterAsync(ActionExecutedContext context)
    {
        await Task.Delay(50); // simulate async logging
        Console.WriteLine("Logging after action");
    }
}
--------------------------------------------
📦 Registration
Tu is filter ko register kar sakta hai:
--------------
Option 1: Globally in Program.cs

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<MyAsyncLoggingFilter>();
});
--------------------------------------------
Option 2: Per Controller or Action

[ServiceFilter(typeof(MyAsyncLoggingFilter))]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
---------------------------------
🔄 Execution Flow

1️⃣ OnActionExecutionAsync (Before)
   ⬇️
2️⃣ Controller Action executes
   ⬆️
3️⃣ OnActionExecutionAsync (After)
Sab kuch async await pattern mein hota hai, non-blocking.
------------------------------------------------
🧪 More Real Use-Cases
🌐 External API request logging before & after action.

🔐 Async token validation in filters.

📊 Metrics capturing (start time, end time, total duration).

💬 Logging requests/responses to cloud services (like Seq, Elasticsearch, etc.).
---------------------------------------------
⚠️ Tips & Notes
Always use await next() — this is the line that actually calls the action.

Agar tu next() ko call nahi karega, toh action execute hi nahi hoga — short-circuit ho jaayega.

try/catch ke saath use kar sakta hai for error-handling inside filter.
------------------------------------------------------------------
✅ Final Words
Async filter is super powerful when:

You want to keep things non-blocking.

You rely on I/O-bound operations (API calls, DB access, etc).

You want clean pre/post-action logic without touching the controller.