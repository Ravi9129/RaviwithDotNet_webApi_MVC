using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 1_Exception Filter
    {
        
    }
}
--------------------------------
Exception Filter ki taraf — ye woh banda hota hai jo galti (exception) hone pe scene sambhalta hai. Jab controller ya action method me koi unhandled error aata hai, tab Exception Filter intervene karta hai, error ko log karta hai, custom message return karta hai, ya redirect bhi kar sakta hai.

❗ What is an Exception Filter?
Exception Filters are used to catch unhandled exceptions thrown during action execution and give a centralized response or logging mechanism.

Ye mainly error ko:

Handle karta hai gracefully

Log karta hai (File, DB, Seq, etc.)

Custom error response return karta hai

Global error handling ko easy banata hai
---------------------------------------------------------
🧠 Real-Life Example:
Tu kisi restaurant me khana khane gaya. Agar waiter garam chai gira de (error/exception), ek aur banda turant aake napkin deta hai, sorry bolta hai aur shayad free dessert bhi (custom response) de de.
--------------------------------------
🔁 Filter Pipeline Me Position

1. Authorization Filter
2. Resource Filter
3. Action Filter
4. Result Filter
5. ❌ Exception Filter (wraps everything else)
🔧 Interfaces:
IExceptionFilter – Synchronous

IAsyncExceptionFilter – Asynchronous
---------------------------------------------------------------
🛠️ Basic Example: Exception Logging Filter

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Unhandled exception occurred!");

        context.Result = new ObjectResult(new
        {
            Message = "Something went wrong! Please try again later.",
            Error = context.Exception.Message
        })
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}
--------------------------------------------------------
🔧 Registration
✅ Globally (Best Practice):

services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
----------------------------------------------------
✅ Per Controller (Not recommended globally):

[ServiceFilter(typeof(GlobalExceptionFilter))]
public class ProductController : Controller
{
}
-----------------------------------------
🛠️ Example: Exception Filter with Email Notification

public class NotifyAdminExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        // Email logic or webhook
        SendMailToAdmin(exception.Message);

        context.Result = new StatusCodeResult(500);
        context.ExceptionHandled = true;
    }

    private void SendMailToAdmin(string message)
    {
        // SMTP or third-party email logic here
    }
}
---------------------------------------
✅ Why Use It?
Centralize your error handling logic.

Return friendly error messages.

Avoid repetitive try-catch blocks in controllers.

Easily log exceptions to Serilog, ELK, Seq, DB etc.
---------------------------------------
⚠️ Limitations
Doesn’t catch exceptions during middleware or UseRouting pipeline.
----------------------------------------------
Use Middleware-based exception handling for full coverage:

app.UseExceptionHandler("/Home/Error");
------------------------------------------
🚀 Real World Scenarios
Scenario	Exception Filter Use
API crashes	Custom 500 JSON response
MVC error	Redirect to Error View
DB failure	Log and fallback message
3rd party failure	Send alert + return message
-------------------------------------------------
🔥 Bonus: Exception Filter for API

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new JsonResult(new
        {
            Success = false,
            Message = "Oops! Error occurred.",
            Details = context.Exception.Message
        });

        context.HttpContext.Response.StatusCode = 500;
        context.ExceptionHandled = true;
    }
}
------------------------------------------
🧵 Summary:
Catch all unhandled exceptions in filters

Provide a graceful response (HTML or JSON)

Log detailed error info for devs/admin

Register globally for consistent behavior

