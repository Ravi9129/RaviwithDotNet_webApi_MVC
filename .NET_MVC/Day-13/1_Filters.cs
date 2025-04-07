using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 1_Filters
    {
        
    }
}
------------------------
🔥 What are Filters in ASP.NET Core?
Filters wo middleware-like components hote hain jo request/response pipeline me intercept karke extra logic add karne ke liye use hote hain.
Jaise:

Kisi action method ke execute hone se pehle kuch karna

Response return hone se pehle kuch modify karna

Exception handle karna

Authorization check karna

In short:
🎯 "Filters allow you to plug in custom logic at different points of the execution pipeline."
--------------------------------------------------
🛠 Real-Life Example:
Soch tu ek ecommerce site bana raha hai.

✅ User jab product purchase karta hai, toh ek LogPurchaseFilter laga dete hain jo har purchase ko database me log kare.

✅ Kisi bhi action method ke run hone se pehle authentication check karwana ho — toh tu AuthorizationFilter use karega.

✅ User jab koi galat input de, ya code me exception aaye, toh tu ExceptionFilter use karega taki friendly message ya redirect ho sake.
---------------------------------------------
🧩 Types of Filters
1. Authorization Filter
Execute hota hai sabse pehle.

Ye check karta hai ki user authorized hai ya nahi.

Agar user unauthorized hai toh aage ki pipeline chalu hi nahi hoti.
--------------------------------
Example:

[Authorize]
public IActionResult Dashboard()
{
    return View();
}
Real-life: Tu Admin panel banaya, Authorize filter ensure karega ki sirf login-admin hi access kare.
-----------------------------------------------------
2. Resource Filter
Ye execute hota hai Authorization ke baad, Action se pehle.

Mostly caching, or request context set karne ke liye use hota hai.
--------------------
Example:

public class CacheResourceFilter : IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        // Check if cache exists
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        // Save result in cache
    }
}
Real-life: User ne product details dekhe, agar woh same details already cache me hain, toh DB call skip kar de.
----------------------------------------------------
3. Action Filter
Execute hota hai action method ke pehle aur baad.

Input ko validate karna, logging, model state check karna — sab isme hota hai.
-----------------------------------
Example:

public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Log input
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Log response
    }
}
Real-life: Tu chahta hai sabhi controller actions ke inputs aur outputs automatically log ho jayein — yahi kaam action filter karega.
----------------------------------------------
4. Exception Filter
Ye handle karta hai action method me agar koi uncaught exception aaye.

Tujhe generic error page ya JSON message dena ho, toh yeh best hai.
----------------------------------
Example:

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new JsonResult(new
        {
            Message = "Something went wrong.",
            Error = context.Exception.Message
        });
    }
}
Real-life: User kisi form me galat input deta hai, backend crash hota hai. Instead of red screen, tu ek accha sa error message de sakta hai.
-------------------------------------
5. Result Filter
Ye execute hota hai jab action result ban gaya ho, lekin client tak nahi gaya.

Mostly response manipulate karne ya audit trail maintain karne ke liye use hota hai.
-----------------------------
Example:

public class ModifyResponseFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // Add headers, cookies, etc.
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Final logging
    }
}
Real-life: Tu response me custom header add karna chahta hai jaise X-Processed-By: MyAppFilter.
---------------------------------------------
💡 Filter Lagane ke 3 Tareeke
At Action Level

[ServiceFilter(typeof(LogActionFilter))]
public IActionResult Index() { }
-----------------------------------------
At Controller Level
----------------------
[ServiceFilter(typeof(GlobalExceptionFilter))]
public class HomeController : Controller { }
------------------------------------------------
Globally (Startup.cs / Program.cs)

services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(LogActionFilter));
});
----------------------------------------------
📦 Custom Filter Banana

public class LogFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Before Action");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("After Action");
    }
}
------------------------------------------
Use in DI:

services.AddScoped<LogFilter>();
---------------------------------
Apply:

[ServiceFilter(typeof(LogFilter))]
----------------------------------------------------
🧠 Real-World Combo Example:
Tu ek controller bana raha hai jisme:

User login hona chahiye ⇒ Authorization Filter

Input valid hona chahiye ⇒ Action Filter

Exception ka proper response aaye ⇒ Exception Filter

Response header me version aana chahiye ⇒ Result Filter

Tere controller par ye 4 filters lag gaye aur saari logic centralized ho gayi 🔥

