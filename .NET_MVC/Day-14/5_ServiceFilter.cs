using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 5_
    {
        
    }
}
------------------------------------------
ServiceFilter samjhte hain detail mein — seedhi bhaasha mein, jaise ab tak sab kuch samjha hai. Iska use hota hai jab hume kisi filter ko Dependency Injection ke through inject karna hota hai — especially jab filter ke constructor me services chahiye hoti hain.

🔍 Definition – What is ServiceFilter in ASP.NET Core?
ServiceFilter ek attribute hai jo ASP.NET Core me use hota hai taaki filters ko DI (Dependency Injection) ke zariye resolve kiya ja sake.

Yani agar tumhara filter kisi service ya configuration ko constructor me accept karta hai, to ServiceFilter se wo service inject ho jayegi.
-----------------------------------------------------
✅ Real-world Scenario:
Socho tumhare paas ek custom logging filter hai jisme tumhe ILogger inject karna hai.

Is logger se tum har action ke pehle aur baad log likhna chahte ho.

Ab agar normal [TypeFilter] use karoge to DI ka proper support nahi milega.

ServiceFilter is the solution.
------------------------------------------------------------------
🛠️ Step-by-Step Implementation
🔧 1. Custom Action Filter with DI constructor:

public class MyLogFilter : IActionFilter
{
    private readonly ILogger<MyLogFilter> _logger;

    public MyLogFilter(ILogger<MyLogFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("➡️ Action Started: " + context.ActionDescriptor.DisplayName);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("⬅️ Action Ended: " + context.ActionDescriptor.DisplayName);
    }
}
----------------------------------------------------------------
🧩 2. Register the filter in DI container (very important):

builder.Services.AddScoped<MyLogFilter>(); // 👈 Must do this
-------------------------------------------------------------------
🧪 3. Apply filter using ServiceFilter attribute:

[ServiceFilter(typeof(MyLogFilter))]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
⚠️ Agar tum MyLogFilter ko ServiceFilter ke saath use karte ho but DI me register nahi karte, to runtime error aayega — "Unable to resolve service for type..."
-------------------------------------------------------------
🎯 Use Cases of ServiceFilter:
Filters jisme constructor injection chahiye (ILogger, DbContext, Config, etc.)

Reusable filters across controllers with services

Secure filters (Authorization with RoleService)

Injected data sources for audit logs, DB save, caching, etc.


---------------------------------------------
💡 Bonus Tip:
Agar tumhare filter ko koi parameter pass karne hain, jaise [TypeFilter] me karte hain, to use TypeFilter.
Agar pure DI-based approach chahiye, use ServiceFilter.
------------------------------------------------
🧠 Summary:
✅ ServiceFilter filters ko DI se resolve karta hai

✅ Use it jab tumhara filter constructor me services lega

✅ Filter ko DI me AddScoped/AddTransient/AddSingleton karke register karna zaroori hai

✅ Clean and testable architecture ke liye best approach

