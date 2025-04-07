using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 13_Result Filter
    {
        
    }
}
------------------------------------------------
Result Filter ki — ye thoda advanced topic hai lekin ekdum real-world mein kaam aane wala.

🧠 Definition: What is a Result Filter?
Result filters ASP.NET Core mein ek aise filters hote hain jo action method execute ho jaane ke baad aur response client ko bhejne se pehle kaam karte hain.

Inka kaam hota hai:

Modify karna action ke output ko

Wrap karna response

Logging, formatting, caching wagairah
------------------------------------------------------
📍 When Do Result Filters Run?

1. Request comes in
2. Action Filter runs (before action)
3. Controller Action runs
4. ✅ Result Filter runs (after action)
5. Response is returned to client
----------------------------------
💡 Real-World Use Cases:
Use Case	Explanation
✅ Wrapping API responses	Har response ko { status, message, data } format mein convert karna
🔁 Response Caching	Cache the action result
📝 Logging Responses	Action result ko log karna
🔒 Output Modification	Hide sensitive data before sending response
-----------------------------------------------------
🔧 How to Create a Result Filter

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class WrapResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // Action ke result ko check karna
        if (context.Result is ObjectResult objectResult)
        {
            var wrapped = new
            {
                success = true,
                data = objectResult.Value
            };

            context.Result = new JsonResult(wrapped)
            {
                StatusCode = objectResult.StatusCode
            };
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Response already rendered ho chuka hota hai
        // Logging, cleanup yahan kar sakte hain
    }
}
--------------------------------------
🛠️ Apply Filter Globally or per Controller/Action
Globally in Program.cs:

services.AddControllers(options =>
{
    options.Filters.Add<WrapResultFilter>();
});
-------------------------------------------
Per Controller/Action:

[ServiceFilter(typeof(WrapResultFilter))]
public class MyController : Controller
---------------------------------------------------------
🔁 Async Version: IAsyncResultFilter

public class CustomAsyncResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // Before sending response
        Console.WriteLine("Before result");

        await next(); // 👈 Response rendered

        // After sending response
        Console.WriteLine("After result");
    }
}
-----------------------------------------------
🧪 Real Example: Hide Sensitive Fields from Response

if (context.Result is ObjectResult result && result.Value is User user)
{
    user.Password = null; // Clean password before sending
}
------------------------------------------
🔐 Pro Tip
Result filter doesn't run if context.Result is already short-circuited (e.g., in Action Filter).

Best for presentation logic — never use it for auth or business rules.