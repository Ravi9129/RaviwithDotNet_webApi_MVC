using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 2_ILogger
    {
        
    }
}
-------------------------------------------------------------
ILogger<T> .NET ka built-in logging abstraction hai, jo strongly-typed logging provide karta hai. Chalo easy bhasha mein samajhte hain:

✅ ILogger<T> Kya Hota Hai?
ILogger<T> ek generic interface hai jahan T usually aapka class name hota hai (like controller ya service).

Isse aap log messages likh sakte ho with log levels (Info, Warning, Error, etc).

Ye strongly typed hota hai: har class ka apna logger context hota hai.
------------------------------------------------------------
🔹 Injecting ILogger<T> in a Controller

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Index page accessed.");
        return View();
    }
}
-----------------------------------------
🔹 Injecting ILogger<T> in a Service

public class ProductService
{
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    public void Save()
    {
        _logger.LogInformation("Product saved successfully.");
    }
}
----------------------------------------------
🧠 Common Log Methods

_logger.LogTrace("Trace log");
_logger.LogDebug("Debug log");
_logger.LogInformation("Information log");
_logger.LogWarning("Warning log");
_logger.LogError("Error log");
_logger.LogCritical("Critical error log");
---------------------------------------------------
🧪 Logging with Exception

try
{
    // some risky code
}
catch (Exception ex)
{
    _logger.LogError(ex, "An error occurred while processing.");
}
----------------------------------------
🔐 Benefits of ILogger<T>
Feature	Description
Strongly Typed	Logs contain source class info
Dependency Injection	Easily inject anywhere
Pluggable Providers	Use Console, File, DB, etc.
Configurable Levels	Control logging in appsettings.json
---------------------------------------------------
🛠️ Configure in Program.cs (.NET 6+)

var builder = WebApplication.CreateBuilder(args);

// Logging config is already added by default
// You can add providers:
builder.Logging.AddConsole();
builder.Logging.AddDebug();
---------------------------------------------------
Example in appsettings.json:

"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning",
    "MyAppNamespace": "Debug"
  }
}
------------------------------------------
📦 Tip: File Logging with Serilog
If you want logging to go in files, use Serilog:


dotnet add package Serilog.AspNetCore
--------------------------------
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));
