using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 14_Resource Filter
    {
        
    }
}
-------------------------------------
 Resource Filter ki — ye filters thode advanced level ke hain, 
 lekin agar samajh gaya to performance aur security dono level pe kaafi kaam aate hain. Jaisa ki pehle ke filters samjhe, 
 ye bhi ASP.NET Core ke filter pipeline ka ek powerful feature hai.

🔍 What is a Resource Filter?
Resource Filter wo filter hota hai jo sabse pehle execute hota hai — before model binding,
 before action filters, and before action execution.

Ye ek tarah ka wrapper ka kaam karta hai — jaise ek security gatekeeper ya performance booster jo request 
processing ka pehla aur aakhri hissa handle karta hai.
-----------------------------------------------------------
📈 Resource Filter Lifecycle

1. Resource Filter (OnResourceExecuting)
2. Model Binding
3. Action Filters
4. Action Method
5. Result Filters
6. Resource Filter (OnResourceExecuted)
-----------------------------------------------
✅ Real-World Use Cases
Use Case	Explanation
⏱️ Request timing/logging	Request start aur end ka time measure karna
🔐 Caching mechanism	Full response ko cache karke next request ke liye serve karna
❌ Request short-circuiting	Kisi condition pe aage process hi na hone dena
💥 Global error handling	Try-catch block before any controller runs
🧠 Resource Filter Interface
🔹 IResourceFilter (Synchronous)
🔹 IAsyncResourceFilter (Asynchronous)
---------------------------------------------------
🔧 Example 1: Logging Execution Time

public class ExecutionTimeResourceFilter : IResourceFilter
{
    private Stopwatch stopwatch = new Stopwatch();

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        stopwatch.Start();
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        stopwatch.Stop();
        Console.WriteLine($"Request took {stopwatch.ElapsedMilliseconds} ms");
    }
}
--------------------------------------------
🔧 Example 2: Async Version with IAsyncResourceFilter

public class AsyncLoggerResourceFilter : IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var start = DateTime.UtcNow;

        var resultContext = await next(); // Action executes

        var end = DateTime.UtcNow;
        var diff = end - start;

        Console.WriteLine($"Total time: {diff.TotalMilliseconds}ms");
    }
}
------------------------------------
⛔ Example 3: Short-Circuiting Request

public void OnResourceExecuting(ResourceExecutingContext context)
{
    if (!context.HttpContext.Request.Headers.ContainsKey("X-API-KEY"))
    {
        context.Result = new ContentResult()
        {
            StatusCode = 401,
            Content = "API Key is missing."
        };
    }
}
---------------------------------------
🛠️ Registering the Resource Filter
Globally:

services.AddControllers(options =>
{
    options.Filters.Add<ExecutionTimeResourceFilter>();
});
----------------------------------------------
Per Controller or Action:

[ServiceFilter(typeof(ExecutionTimeResourceFilter))]
public class ProductsController : Controller
--------------------------------------------
🤯 Real-World Analogy
Soch bhai tu kisi mall ke andar ja raha hai. Sabse pehle security guard tera bag check karta hai (resource filter), 
phir tu andar jaake shopping karta hai (action), phir billing hoti hai (result).
 Agar security guard ne hi rok diya to aage kuch nahi hota.
--------------------------------------------------------
📌 Summary
Runs before anything else (model binding se bhi pehle)

Best for caching, short-circuiting, or performance tracking

Can wrap the whole request pipeline

Use IAsyncResourceFilter for async logic