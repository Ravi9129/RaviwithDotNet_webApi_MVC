using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 3_IAlwaysRunResultFilter 
    {
        
    }
}
-----------------------------------
IAlwaysRunResultFilter ek special type ka filter hai ASP.NET Core me jo Result Execution phase me hamesha chalta hai, 
even if action ya pehle filters ne request ko short-circuit kar diya ho. Ye normal filters jaise nahi hota — isme ek 
important guarantee hoti hai: Ye kabhi skip nahi hota.

🔍 Definition – What is IAlwaysRunResultFilter?
IAlwaysRunResultFilter is an interface that ensures your result filter always runs, 
even if the request pipeline was short-circuited before reaching the action.
-----------------------------------------
🧠 Real-World Example:
Soch le ek global footer ya logging system hai jo response ke end me kuch always add karta hai — chahe koi bhi 
error ho ya short-circuit hua ho.


---------------------------------------------
🛠️ Syntax and Usage:

public class MyAlwaysRunResultFilter : IAlwaysRunResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // Always runs — even if action was not executed
        context.HttpContext.Response.Headers.Add("X-Custom-Header", "Filtered");
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // You can log timing, request info, etc.
    }
}
---------------------------------------
🧪 Register Globally (Optional):

services.AddControllersWithViews(options =>
{
    options.Filters.Add<MyAlwaysRunResultFilter>();
});
------------------------------------------
Or on specific controller/action:

[ServiceFilter(typeof(MyAlwaysRunResultFilter))]
public IActionResult Index()
{
    return View();
}
-------------------------------------------
💥 Use Cases:
✅ Logging headers or diagnostics always

✅ Adding CORS headers / metadata always

✅ Tracking responses even for short-circuited actions (like unauthorized, rate-limited)

✅ Returning post-processing info (e.g. timestamp, status markers)

