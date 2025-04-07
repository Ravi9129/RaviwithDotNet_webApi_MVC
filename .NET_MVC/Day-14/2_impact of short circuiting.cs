using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 2_impact of short circuiting
    {
        
    }
}
------------------------------------
"Short-Circuiting" ke impact pe — ye concept action filters aur ASP.NET pipeline ke flow ko samajhne ke liye bohot crucial hai. Real-world example aur code ke sath smjhata hoon jaise pehle kiya tha 💪

🔥 What is Short-Circuiting in ASP.NET Filters?
Short-circuiting means: ek filter ya middleware ne request pipeline ko aage jaane se rok diya — yaani action method ya baaki filters tak request nahi pahunchegi.
---------------------------------------------
🔁 Filter executes → Returns a result → ❌ Skips rest of pipeline
🧠 Real-Life Example:
Tu office ja raha hai, lekin gatekeeper bolta hai “ID card nahi, tu andar nahi jaa sakta.”
Tu andar nahi jaata — yaani process yahin pe short-circuit ho gaya.
-----------------------------------
⚙️ Where It Happens?
In filters like:

IActionFilter

IAuthorizationFilter

IResourceFilter 

And in middleware too
----------------------------------------------
🔄 How to Short-Circuit?
Just set a Result on the context and mark context.Result != null.

public class MyAuthFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var isAuthorized = false; // Simulated check

        if (!isAuthorized)
        {
            context.Result = new ContentResult
            {
                Content = "Unauthorized access blocked by filter."
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // This will not be hit if short-circuited above
    }
}
------------------------------------------
💥 IMPACT of Short-Circuiting
1. Skips Controller Action Execution
Action method execute hi nahi hota. Bas filter se hi response chala jaata hai.

📌 Example:
A role-check filter finds user is not "Admin", sends a 403 response → action method doesn't run.
-----------------------------------------------------------
2. Downstream Filters Are Skipped
Agar ek early filter context.Result set kar de, toh baaki filters (ResultFilter, ExceptionFilter) skip ho jaate hain.

📌 Example:
Authorization filter short-circuits → result filter, action filter don't execute.
-------------------------------------------------
3. Better Performance
Agar condition pehle hi fail ho jaye, toh controller logic process hi nahi hota → CPU/memory save

📌 Example:
Rate-limiting middleware short-circuits suspicious IP requests → app fast rahega.
------------------------------------------
4. Can Bypass Important Logic (Risk!)
Galti se short-circuit kar diya toh logging, validation, DB logic sab miss ho jaata hai

📌 Example:
Custom filter blocks request before logger filter runs → koi log nahi banta
------------------------------------------------------
5. Use for Centralized Handling
Short-circuiting is useful for:

Global auth

Maintenance mode

Header/token checks

Feature flag-based blocking
-----------------------------------------
📌 Example:
App under maintenance → Middleware returns “Come back later” response without hitting any controller.

⚠️ Caution:
Pitfall	Impact
Overusing filters	Complex debugging
No logging before short-circuit	No trace of what happened
Unexpected skips	App behavior unpredictable
------------------------------------------
🧪 Test It Yourself

public class BlockAllFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.Result = new ContentResult
        {
            Content = "You are blocked. Short-circuited.",
            StatusCode = 403
        };
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
----------------------------------------------
Use it on controller:

[ServiceFilter(typeof(BlockAllFilter))]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View(); // Will never be hit
    }
}
