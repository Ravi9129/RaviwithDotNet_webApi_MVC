using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 8_Custom Order Filters
    {
        
    }
}
----------------------------------------
Custom Order Filters ka scene dekhte hain — proper explanation ke saath, real-world example included.
 Bina table ke, seedha practical style mein 🔥
---------------------------------------
📌 Kya Hota Hai Filter Order?
ASP.NET Core me multiple filters hote hain:

Authorization filters

Resource filters

Action filters

Exception filters

Result filters

Lekin jab ek hi type ke multiple filters laga de, jaise do ActionFilters, toh question hota hai: Kaun pehle chalega?

Answer: Order define kar sakta hai!
--------------------------------------------------
🔄 Filter Execution Order (By Default)
Jaise agar tu 2 filters likhta:

public class FilterA : IActionFilter { ... }
public class FilterB : IActionFilter { ... }
------------------------------------------------
Aur controller pe laga diya:

[FilterA]
[FilterB]
public class HomeController : Controller { ... }
Toh execution order FilterA → FilterB hoga (top to bottom).
BUT tu chahe toh manually bhi order define kar sakta hai using IOrderedFilter.
-------------------------------------------------
🔧 How to Define Custom Order
✅ Step 1: Implement IOrderedFilter

public class CustomOrderFilter : IActionFilter, IOrderedFilter
{
    public int Order { get; set; }

    public CustomOrderFilter(int order)
    {
        Order = order;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[Filter {Order}] - OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"[Filter {Order}] - OnActionExecuted");
    }
}
---------------------------------------------
✅ Step 2: Register with Custom Order in Startup.cs or Program.cs

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new CustomOrderFilter(2)); // Will run after lower order
    options.Filters.Add(new CustomOrderFilter(1)); // Will run before
});
----------------------------------------------
🧠 Real-World Example
Tu chah raha:

Filter #1 → Logging filter (chale pehle)

Filter #2 → Metrics timing filter (chale baad me)
--------------------------------------------
Toh tu yeh karega:


options.Filters.Add(new LoggingFilter() { Order = 1 });
options.Filters.Add(new MetricsFilter() { Order = 2 });
-----------------------------------------------------------
🔁 Important Concept
💥 Execution vs. Unwinding
Filters do phase me chalte hain:

OnActionExecuting → Order wise (1 → 2 → 3)

OnActionExecuted → Reverse Order (3 → 2 → 1)

Toh ek filter agar pehle execute ho raha, toh uska response baad me unwind hoga.
---------------------------------------------
🚫 Agar Order Specify Nahi Kiya?
Default Order = 0

Agar sabka 0 hai, toh registration order ya attribute order se decide hota hai.
----------------------------------------------
🧪 Bonus: Attribute-based Custom Order

public class OrderedAttributeFilter : ActionFilterAttribute
{
    public OrderedAttributeFilter(int order)
    {
        Order = order;
    }
}
----------------------------------------------
Use on controller:

[OrderedAttributeFilter(2)]
[OrderedAttributeFilter(1)]
public class HomeController : Controller { ... }
-----------------------------------------
🎯 Summary
Use Order property via IOrderedFilter to control execution order of same-type filters.

Smaller Order value means earlier execution.

Useful for layered cross-cutting concerns like logging → auth → caching.

Execution is forward, unwinding is reverse.