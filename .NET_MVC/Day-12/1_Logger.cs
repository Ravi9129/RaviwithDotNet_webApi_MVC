using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 1_Logger
    {
        
    }
}
--------------------------------------------------------------
 Logger ki .NET ke context mein — Logging is 🔑 when you want to trace, debug, audit, or monitor your application’s behavior.

✅ Logger in ASP.NET Core
.NET Core uses built-in logging abstraction via ILogger<T> interface.
-----------------------------------------------------------
🔹 Injecting ILogger in a Controller / Service

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Index page requested.");
        _logger.LogWarning("This is a warning message.");
        _logger.LogError("Something went wrong!");

        return View();
    }
}
-----------------------------------------
🧠 Log Levels

_logger.LogTrace("Trace - most detailed info");
_logger.LogDebug("Debug - development debugging info");
_logger.LogInformation("Information - general flow");
_logger.LogWarning("Warning - something unexpected");
_logger.LogError("Error - something failed");
_logger.LogCritical("Critical - app crash!");
---------------------------------------------------------------------
✅ Configuration in appsettings.json

"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
}
---------------------------------------------
📦 Where Do Logs Go?
By default:

Console (when using dotnet run)

Debug window (in Visual Studio)
----------------------------------------------------
You can also:

Use Serilog, NLog, Log4Net for advanced logging

Write logs to File, Database, Elasticsearch, Application Insights, etc.
-------------------------------------------------------------
🔥 Example: Logger in a Service

public class ProductService
{
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    public void SaveProduct(Product product)
    {
        try
        {
            // Save logic...
            _logger.LogInformation($"Product {product.Name} saved.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving product.");
        }
    }
}
🛠️ Custom Logging Provider (Optional)
---------------------------------------------------------
You can plug in:

builder.Logging.AddFile("Logs/app.txt"); // Using 3rd party like Serilog/NLog
---------------------------------------
👊 Summary
Feature	How
Inject Logger	ILogger<T>
Log Levels	Trace to Critical
Configurable	via appsettings.json
Extendable	Serilog, NLog, etc
Use in Controllers & Services	✅ Easily injectable