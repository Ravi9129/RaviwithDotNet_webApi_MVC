using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 10_IOrderedFilter 
    {
        
    }
}
-------------------------------------
IOrderedFilter ko full deep detail mein samjhte hain — theory + example + real-life use-case. Bina table ke,
 straight-forward explain karta hoon jaise tu chaahta hai 💡
-------------------------------------------
📌 What is IOrderedFilter?
IOrderedFilter ek interface hai jo ASP.NET Core mein filters ko custom execution order dene ke liye use hota hai.

Normally, agar tu multiple filters lagata hai (like ActionFilters),
 toh woh order mein chalte hain jaise tu unhe register karta hai. 
 Lekin agar tu manually order control karna chahta hai, toh tu IOrderedFilter ka use karega.
-------------------------------------------
🧠 Why Use IOrderedFilter?
Imagine:

Tu chah raha hai ki LoggingFilter sabse pehle chale,

Uske baad chale ValidationFilter,

Aur sabse last mein chale MetricsFilter.

Aise cases mein execution order define karna important hota hai.
-----------------------------------------------
🔧 Syntax — How to Use
Step 1: Create a Filter Class and Implement IOrderedFilter

using Microsoft.AspNetCore.Mvc.Filters;

public class CustomOrderFilter : IActionFilter, IOrderedFilter
{
    public int Order { get; set; }

    public CustomOrderFilter(int order)
    {
        Order = order;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[Order: {Order}] - Before Action");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"[Order: {Order}] - After Action");
    }
}
-------------------------------------------
Step 2: Register Filters with Order
In Program.cs or Startup.cs:

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new CustomOrderFilter(1)); // Logging
    options.Filters.Add(new CustomOrderFilter(2)); // Validation
    options.Filters.Add(new CustomOrderFilter(3)); // Metrics
});
🔄 Execution Flow
🟢 OnActionExecuting runs in ascending order (1 → 2 → 3)
🔴 OnActionExecuted runs in descending order (3 → 2 → 1)

So tu jaise register karega, uska reverse bhi hona chahiye for after-action logic.
--------------------------------------------------
🧪 Real World Example
Example use-case:
Tu ek API bana raha hai jahan:

Logging hamesha pehle ho,

Authentication/authorization baad mein,

Metrics aur time capture last mein.
--------------------------------------------
So tu karega:

options.Filters.Add(new LoggingFilter() { Order = 1 });
options.Filters.Add(new AuthFilter() { Order = 2 });
options.Filters.Add(new MetricsFilter() { Order = 3 });
Yeh tera pipeline ban gaya.
------------------------------------------------
⚠️ Important Notes
Agar Order specify nahi kiya toh default hota hai 0.

Lower Order runs earlier.

Filters of different types (e.g., action vs. result) don’t overlap in order.

Useful jab tu layered logic implement kare (like middleware for filters).
------------------------------------------
🔥 Bonus — Attribute Style with Order
Tu ActionFilterAttribute inherit karke bhi Order de sakta hai:

public class OrderedActionFilter : ActionFilterAttribute
{
    public OrderedActionFilter(int order)
    {
        Order = order;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[Attr Order: {Order}] - OnActionExecuting");
    }
}
------------------------------------------------
Use it like:

[OrderedActionFilter(1)]
[OrderedActionFilter(2)]
public class HomeController : Controller { }
--------------------------------------------------
🎯 Summary
IOrderedFilter allows custom execution order of same-type filters.

Smaller Order runs earlier in executing phase, later in executed phase.

Extremely helpful for maintaining clean separation of concerns like logging, validation, error handling, metrics, etc.

