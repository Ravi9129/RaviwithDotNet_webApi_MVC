using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 7_IFilterFactory
    {
        
    }
}
-----------------------------------------------------
IFilterFactory ko todh ke samajhte hain — ekdum real-world developer style mein, bina table ke, 
direct concept se leke use case, example, and scenario.
-------------------------------------
🔍 Definition — What is IFilterFactory?
IFilterFactory ek interface hai jo tumhare custom filter class ko runtime par dynamically
 instance create karne ki power deta hai — especially jab tumhe filter me Dependency Injection (DI) chahiye hoti hai.

Normally [MyCustomFilter] attribute me DI nahi aata.
But agar tum IFilterFactory implement karte ho, to tumhara filter DI use kar sakta hai.
----------------------------------------------------
✅ Real-world Scenario:
Maan lo tum ek audit logging filter bana rahe ho jisme tumhe ILogger aur DbContext chahiye.

Lekin agar tum sirf [MyFilter] likhte ho to wo constructor injection allow nahi karega.

To tum IFilterFactory implement karoge, jisse DI container se dependencies leke filter instance create hoga.
-------------------------------------------------
🧠 Concept Breakdown:
Jab tum filter banate ho aur IFilterFactory implement karte ho:

Tumhara filter class ek factory ban jata hai.

Tum override karte ho CreateInstance(IServiceProvider) method ko.

Tum DI container se jo chahiye, wo lete ho.

Tum real filter object return karte ho jisme required dependencies hoti hain.
-------------------------------------------------------
🛠️ Step-by-Step Example:
✨ 1. Real Filter with DI

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("➡️ Action starting: " + context.ActionDescriptor.DisplayName);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("⬅️ Action completed.");
    }
}
-------------------------------------------
✨ 2. Factory Attribute Class

public class LogActionFilterFactoryAttribute : Attribute, IFilterFactory
{
    public bool IsReusable => false; // create new instance every time

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<LogActionFilter>();
    }
}
IFilterFactory implement karke tum DI se LogActionFilter create kar rahe ho.
--------------------------------------------------
✨ 3. Register Filter in DI

builder.Services.AddScoped<LogActionFilter>();
--------------------------------------------------------------
✨ 4. Apply to Controller/Action

[LogActionFilterFactory]
public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
------------------------------------------
⚠️ Important Notes:
IFilterFactory ka use karo jab tumhe filter ke andar services chahiyein ho.

Yeh tumhara filter attribute hone ke saath ek instance generator bhi hota hai.

Ye zyada flexible hai compared to ServiceFilter aur TypeFilter, lekin thoda advanced level ka concept hai.
-----------------------------------------------
✅ Why use IFilterFactory over TypeFilter?
TypeFilter aur ServiceFilter me runtime pe reflection aur wrapper use hota hai.

IFilterFactory me tum poori control le sakte ho ki filter ka instance kaise create ho.
---------------------------------------------
🔚 Summary:
IFilterFactory lets you create filters with DI support.

You use it when your filter needs services like ILogger, DbContext, etc.

Tum CreateInstance(IServiceProvider) override karte ho to manually instance banate ho.

Helpful for reusable, testable, DI-friendly custom filters.