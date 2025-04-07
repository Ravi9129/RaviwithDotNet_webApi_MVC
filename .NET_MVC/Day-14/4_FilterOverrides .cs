using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 4_FilterOverrides 
    {
        
    }
}
-----------------------------------------
FilterOverrides ka concept samjhte hain bilkul practical aur real-world style me, bina kisi table ke — step by step, 
jaise pehle karte aaye hain. Yeh topic useful hai jab aap globally ya controller level par koi filter laga dete ho,
 but kisi specific action pe usko override (ya hata) dena chahte ho.

🔍 Definition – What is FilterOverride in ASP.NET Core?
FilterOverride ASP.NET Core ka mechanism hai jisse aap ek higher-level filter (Global/Controller) ko disable kar 
sakte ho for a specific controller or action.

Agar globally ya controller level pe koi filter laga hai, aur aap chahte ho ki kisi ek specific action me wo filter 
execute na ho, tab aap FilterOverride ka use karte ho.
----------------------------------------------
🧠 Real-World Scenario:
Socho ki tumne ek Global Logging Filter laga diya hai jo har action ki timing log karta hai.

Lekin ek particular action me tumhara sensitive kaam ho raha hai — jahan tum nahi chahte ki uski timing ya details log ho.

Tum chahte ho ki baaki sab jagah filter chale, lekin ek action me na chale — toh uske liye FilterOverride ka use hota hai.
---------------------------------------------------
🛠️ Kaise kaam karta hai?
1. Sabse pehle ek global filter bana lo:

public class MyLoggingFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("➡️ Logging Before Action");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("⬅️ Logging After Action");
    }
}
------------------------------------
2. Global filter ko Startup.cs me add karo:

services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(MyLoggingFilter)); // Global level
});
-------------------------------------------------
3. Ab ek FilterOverride apply karo specific action me:

[TypeFilter(typeof(MyLoggingFilter))]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(); // LoggingFilter chalega
    }

    [ServiceFilter(typeof(FilterCollectionOverride<MyLoggingFilter>))]
    public IActionResult NoLog()
    {
        return View(); // LoggingFilter YAHAN nahi chalega
    }
}
---------------------------------------------
🔎 Important Note:
FilterCollectionOverride<TFilter> ek predefined class nahi hoti. Iska kaam khud karna padta hai.

ASP.NET Core directly koi FilterOverride attribute nahi deta (jaise purane ASP.NET MVC me hota tha).

Lekin tum IFilterMetadata ya dummy filters se manually override kar sakte ho.
-----------------------------------------------------------
✅ Custom Override Approach (ASP.NET Core style):

public class SkipMyLoggingFilter : IFilterMetadata { }
------------------------------------------------
Then inside your MyLoggingFilter:

public void OnActionExecuting(ActionExecutingContext context)
{
    if (context.Filters.OfType<SkipMyLoggingFilter>().Any())
    {
        return; // Skip logic if override marker found
    }

    Console.WriteLine("Logging this action");
}
-------------------------------------------
And apply like this:

[TypeFilter(typeof(SkipMyLoggingFilter))]
public IActionResult NoLog()
{
    return View(); // Logging skipped!
}
----------------------------------------------
💡 Summary (Seedha Point Style):
🔹 Use FilterOverrides to stop a filter from running at a specific place.

🔹 Useful when a global/controller filter shouldn’t apply somewhere.

🔹 ASP.NET Core me manual override karna padta hai using marker interfaces or custom logic.

🔹 Real-world use: logging skip, authentication disable, validation bypass, etc.

