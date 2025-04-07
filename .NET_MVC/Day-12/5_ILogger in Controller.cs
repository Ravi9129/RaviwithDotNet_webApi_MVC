using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 5_ILogger in Controller
    {
        
    }
}
-------------------------------------------
ILogger controller ke andar logging messages, errors, warnings ya information capture karne ke liye use hota hai. Ye .NET ka built-in logging interface hai jo clean aur structured logging deta hai.
-------------------------------------------------------------
✅ ILogger in Controller
💡 Step-by-step Example:

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    // Constructor Injection
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Index action called at {time}", DateTime.UtcNow);
        return View();
    }

    public IActionResult Error()
    {
        try
        {
            // Some error-prone code
            throw new Exception("Sample exception!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong in Error action");
            return View("Error");
        }
    }
}
-------------------------------------------------
🧠 Log Levels (For Filtering)
Method	Description
LogTrace	Most detailed log (for debugging only)
LogDebug	For development/debugging info
LogInformation	General app flow info (startup, shutdown)
LogWarning	Unexpected events that are not errors
LogError	For runtime errors
LogCritical	Serious failures (app crash etc.)
-----------------------------------------------
🎯 Logging with Message Templates

_logger.LogInformation("User {UserName} logged in at {LoginTime}", "admin", DateTime.UtcNow);
------------------------------------------------
Logs will show:
info: User admin logged in at 2025-04-02T13:00:00Z
-------------------------------------
🛠 Tip: Filter Logs in appsettings.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
-------------------------------------------
🔥 Bonus: Use LoggingScope

using (_logger.BeginScope("TransactionId: {TransactionId}", Guid.NewGuid()))
{
    _logger.LogInformation("Doing work inside a scoped transaction.");
}