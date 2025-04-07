using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 8_Serilog 
    {
        
    }
}
-----------------------------------------
Serilog ek powerful structured logging library hai .NET ke liye. Ye tumhare logs ko readable + searchable banata hai aur logs ko multiple locations pe likhne deta hai — jaise console, file, database, Seq, ElasticSearch, etc.

🔥 Why Serilog?
💡 Structured logging (JSON-style data)

🔍 Easy filtering and searching

📂 Logs to multiple sinks (file, database, etc.)

⚙️ Flexible + production-ready

🛠️ Setup Serilog in ASP.NET Core (step-by-step)
---------------------------------------------------------
1. Install NuGet packages

dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
------------------------------------------------------------
2. Program.cs me configure karo

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Setup Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(); // 👈 Important

builder.Services.AddControllersWithViews();
var app = builder.Build();

// middleware, endpoints etc...
app.Run();
---------------------------------------------
3. ✅ Log karo controllers me

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Index page accessed at {Time}", DateTime.UtcNow);
        return View();
    }
}
------------------------------------------
📦 Optional: JSON-based configuration (appsettings.json)
Add this in appsettings.json:

"Serilog": {
  "MinimumLevel": "Information",
  "WriteTo": [
    { "Name": "Console" },
    {
      "Name": "File",
      "Args": {
        "path": "Logs/log-.txt",
        "rollingInterval": "Day"
      }
    }
  ],
  "Enrich": [ "FromLogContext" ]
}
-----------------------------------------
In Program.cs:

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
    ------------------------------------------------
📦 Popular Serilog Sinks
Sink	Use
Serilog.Sinks.File	Write logs to file
Serilog.Sinks.Console	Console output
Serilog.Sinks.Seq	Structured logs in Seq UI
Serilog.Sinks.MSSqlServer	Logs to SQL Server
Serilog.Sinks.Elasticsearch	Logs to Elastic Stack
Serilog.Sinks.ApplicationInsights	Azure Logs
--------------------------------------------------------
🧪 Output Example (Console):

[15:01:23 INF] Index page accessed at 4/2/2025 9:31:23 AM
💬 Want to log request/response body too?
----------------------------------------------------------------------
Add middleware:

app.UseSerilogRequestLogging(); // <-- auto logs HTTP requests
