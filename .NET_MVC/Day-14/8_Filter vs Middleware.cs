using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 8_Filter vs Middleware
    {
        
    }
}
---------------------------------------------
"Filter vs Middleware in ASP.NET Core?"
Samjhta hoon tu in-depth chah raha hai, toh chalo ekdam real-world style me, bina table ke, 
full practical explanation ke saath dekhte hain.
----------------------------------
🔥 Starting Point — What are they?
✅ Middleware:
Middleware ek pipeline ka part hota hai jo request aur response ke beech chalta hai.
-------------------------
🧠 Think of it like:

Gatekeepers jo har request ko process karte hain chahe controller chale ya na chale.

✅ Filters:
Filters sirf MVC layer ke andar kaam karte hain (Controllers, Actions, Results).
-------------------------------------
🧠 Think of it like:

Controllers ke andar ka custom logic inject karne ka way, jaise action ke pehle/baad kuch karna.

💡 Real-World Analogy:
Middleware: Toll plaza on the highway. Har gaadi (request) ko guzarna hi padega.

Filter: Office building ke andar ka guard. Sirf building me aake hi uska kaam shuru hota hai.
----------------------------------------------------
🔩 Middleware Details:
Runs before MVC pipeline and after response.

Can short-circuit the pipeline (like app.UseAuthentication()).
---------------------------------
Used for:

Global exception handling

Logging

Authentication

CORS

Custom headers

Static file serving
--------------------------------------
Example:

app.Use(async (context, next) =>
{
    Console.WriteLine("🌐 Middleware before");
    await next();
    Console.WriteLine("🌐 Middleware after");
});
-----------------------------------------------------------
🧩 Filter Details:
Runs inside the MVC framework.

Has different types:

AuthorizationFilter

ResourceFilter

ActionFilter

ResultFilter

ExceptionFilter
-----------------------------------
Used for:

Action logging

Validation

Modifying view data

Custom authentication

Exception wrapping in controllers
--------------------------
Example:

public class MyActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("🎯 Before action");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("🎯 After action");
    }
}
---------------------------------------
🚀 Execution Order (Middleware + Filter):
Middleware 1 (Before)

Middleware 2 (Before)

MVC Route matches

Authorization Filter

Resource Filter

Action Filter

Action Executes

Result Filter

Resource Filter (OnResult)

Middleware 2 (After)

Middleware 1 (After)
--------------------------------------
✅ Middleware vs Filter - When to Use?
Scenario	Use
Logging every request globally	Middleware
Validating input parameters before action	Filter
Authentication with tokens or cookies	Middleware
Authorization before action runs	Authorization Filter
Wrapping result or modifying response	Result Filter
Custom logic inside action flow	Action Filter
Catching global unhandled exceptions	Middleware or Exception Filter (MVC)
----------------------------------------------
🧪 Real Dev Tip:
✨ Use Middleware for cross-cutting concerns that apply to the whole app.
✨ Use Filters when you want to act on specific controllers or actions.
----------------------------------------------------------
🔚 Summary:
Middleware = Entire app ke liye, request-response pipeline pe.

Filters = Sirf MVC layer ke liye.

Middleware controller ke pehle aur baad dono me kaam karta hai.

Filter controller ke andar actions ke around kaam karta hai.

Middleware low-level global logic ke liye best hai.

Filters MVC-specific behavior customize karne ke liye best hain.