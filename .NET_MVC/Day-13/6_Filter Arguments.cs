using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 6_Filter Arguments
    {
        
    }
}
-------------------------------------------------
Filter Arguments samajhte hain .NET Core MVC / .ASP.NET Core ke context me — jaise tu chah raha tha: deep explanation, real-world examples ke saath, bina table ke.

📌 What are Filter Arguments?
Filter Arguments ka matlab hota hai:

Jab tu custom filter (jaise ActionFilter, ResultFilter, AuthorizationFilter, etc.) banata hai,
 toh unme tu constructor parameters ya properties ke through values pass kar sakta hai — inhe hi filter arguments kehte hain.

🤔 Real-World Example Scenario
Soch le ek real app jahan tu har action ke start/end me log karna chahta hai — lekin har controller me alag log message chahiye.

Toh tu ek ActionFilter banayega jisme tu log message ko constructor me parameter ke form me pass karega. 
That parameter is the filter argument.
---------------------------------------------
🧪 Custom Action Filter with Arguments
Step 1: Create the Filter

public class LogActionFilter : ActionFilterAttribute
{
    private readonly string _message;

    public LogActionFilter(string message)
    {
        _message = message;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[START] {_message} - {DateTime.Now}");
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"[END] {_message} - {DateTime.Now}");
    }
}
--------------------------------------------------
Step 2: Apply Filter with Argument

[LogActionFilter("Executing Home Controller")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
-------------------------------------------
🧠 Explanation:

"Executing Home Controller" is the filter argument.

Ye argument constructor ke through filter me jata hai.
-----------------------------------------------
🔁 Reuse with Different Arguments

[LogActionFilter("Executing Account Controller")]
public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}
Aise tu same filter ko different places me reuse kar sakta hai — bas arguments change karke.
------------------------------------------------
🔒 Use Case with Authorization Logic
Tu ek PermissionFilter bana sakta hai jisme tu required permission as an argument pass kare:

public class PermissionFilter : ActionFilterAttribute
{
    private readonly string _permission;

    public PermissionFilter(string permission)
    {
        _permission = permission;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Imagine permission checking logic here
        Console.WriteLine($"Permission required: {_permission}");
    }
}
-------------------------------------
Use it like:

[PermissionFilter("Admin.Read")]
public IActionResult Dashboard()
{
    return View();
}
----------------------------------------------------------
🤝 Dependency Injection Issue & Solution
Filters with arguments can't be constructor-injected via DI directly, kyunki attribute constructor compile-time run hota hai.

🛠 Solution:

Use TypeFilter or ServiceFilter.
---------------------------------------------
TypeFilter Example:

public class CustomFilterWithDI : IActionFilter
{
    private readonly ILogger<CustomFilterWithDI> _logger;
    private readonly string _param;

    public CustomFilterWithDI(ILogger<CustomFilterWithDI> logger, string param)
    {
        _logger = logger;
        _param = param;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Param: " + _param);
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
------------------------------------------
Register:

[TypeFilter(typeof(CustomFilterWithDI), Arguments = new object[] { "hello" })]
public class MyController : Controller
{
    public IActionResult Index() => View();
}
--------------------------------------------
🔚 Summary
Filter arguments allow you to customize behavior per use.

You can pass strings, booleans, anything serializable.

For dependency-injected filters, use TypeFilter or ServiceFilter.

Real world: use them for logging, permissions, tenant filtering, custom behavior, etc.

