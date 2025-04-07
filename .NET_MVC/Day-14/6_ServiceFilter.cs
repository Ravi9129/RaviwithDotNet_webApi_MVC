using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 6_ServiceFilter
    {
        
    }
}
--------------------------------------------
Filter Attribute Class ka concept samajhte hain — puri clarity ke sath, 
real-world context mein. Ye ASP.NET Core mein custom filters banane ka core base hota hai.

🔍 Definition – What is a Filter Attribute Class?
Filter Attribute Class ek C# class hoti hai jo ASP.NET Core ke filter interfaces implement karti hai aur 
Attribute bhi hoti hai — taaki tum ise [MyFilter] jese decorate kar sako directly controller/action pe.

Yani:

[MyCustomFilter]
public IActionResult Get() { ... }
⚡ Ye class ek decorator + behavior modifier hoti hai.
-------------------------------------------
✅ Real-world Scenario:
Tum ek aisa feature chahte ho jahan har API call ke aane se pehle, ek custom logging ya validation ho jaye.
Ya kuch cases me audit log database me save ho, ya koi global timer start ho jaye.

Iske liye tum ek custom attribute filter banaoge jo automatic trigger ho.
----------------------------------
🛠️ Types of Filter Attribute Classes You Can Create:
ASP.NET Core me mainly 5 tarah ke filters hote hain jo tum attribute class me implement kar sakte ho:


-----------------------------------------------------------------------------------
✨ Example – Custom Action Filter Attribute Class
----------------------------------------------------
🔧 1. Create the Attribute Filter:

public class LogActionFilterAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("➡️ Action is about to execute: " + context.ActionDescriptor.DisplayName);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("⬅️ Action has executed: " + context.ActionDescriptor.DisplayName);
    }
}
⚠️ Note: Ye class Attribute bhi hai aur IActionFilter bhi — isliye tum ise directly decorate kar sakte ho.
-------------------------------------------------------------------
📌 2. Apply on Controller or Action:

[LogActionFilter]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
-----------------------------------------------------
💡 You Can Make It Async Too:

public class LogActionFilterAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("➡️ Before action");
        var resultContext = await next(); // Action executes here
        Console.WriteLine("⬅️ After action");
    }
}
--------------------------------------------------
💥 Powerful Use Cases:
✅ Input validation across actions

✅ Audit logging

✅ Authorization check

✅ Custom header checks

✅ Timer or performance metrics

✅ Inject tenant or user data before action executes
---------------------------------------------------------------------
🧠 Bonus Tip – DI with Filter Attribute Class
Normally, attribute class directly DI accept nahi karti constructor me.
Agar services inject karni hain, to use:

ServiceFilter

TypeFilter

or register as IFilterFactory
-------------------------------------------------------
Example using TypeFilter:

[TypeFilter(typeof(MyFilterWithDI))]
public class HomeController : Controller { ... }
-------------------------------------------------
✅ Summary:
Filter Attribute Class ek aisi class hai jo filter interface + Attribute inherit karti hai

Iska use tum controller ya action level pe [MyFilter] laga ke kar sakte ho

Use cases include: logging, auth, performance, validation

Agar services chahiye ho to use ServiceFilter ya TypeFilter