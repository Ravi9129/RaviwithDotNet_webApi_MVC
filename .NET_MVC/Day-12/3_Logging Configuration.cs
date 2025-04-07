using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 3_Logging Configuration
    {
        
    }
}
-------------------------------
logging configuration .NET apps mein super powerful hoti hai — aap decide kar sakte ho kya log karna hai, kaunse level pe karna hai, aur kahan store karna hai (Console, File, DB, etc.).

✅ Logging Configuration in .NET
.NET mein logging built-in hoti hai aur aap ise configure karte ho Program.cs, appsettings.json, ya custom providers ke through.
----------------------------------------------------
🔹 1. Basic Logging Configuration (appsettings.json)

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "MyAppNamespace": "Debug"
    }
  }
}
Key	Description
Default	Sabhi namespaces ke liye default log level
Microsoft.AspNetCore	Microsoft libs ke logs
MyAppNamespace	Aapke custom namespace ke logs
-----------------------------------------------------------
🔹 2. Program.cs Setup (for .NET 6/7/8+)

var builder = WebApplication.CreateBuilder(args);

// Optional: Add Console, Debug logging
builder.Logging.ClearProviders(); // Optional: clear defaults
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Optional: use appsettings.json config
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

var app = builder.Build();
--------------------------------------------------------
🔹 3. Log Levels
Level	Use For
Trace	Sabse detailed
Debug	Dev/debug info
Information	General info
Warning	Potential issue
Error	Error in app
Critical	System failure
--------------------------------------------
🔹 4. Custom Logging Providers (e.g., Serilog, NLog)
If you want logging to file, database, Elastic, etc., use:

Serilog

NLog

Elmah

Application Insights
-------------------------------------
Example with Serilog:

dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
-----------------------------------------------
// Program.cs
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
--------------------------------------------------------
🔹 5. Environment-Specific Logging
You can have different logging settings for environments:

appsettings.Development.json

"Logging": {
  "LogLevel": {
    "Default": "Debug"
  }
}
--------------------------------------
appsettings.Production.json

"Logging": {
  "LogLevel": {
    "Default": "Warning"
  }
}
------------------------------------------------
.NET will automatically pick based on:

ASPNETCORE_ENVIRONMENT=Development
----------------------------------------------------------------
🔹 6. Structured Logging

_logger.LogInformation("User {Username} logged in at {Time}", username, DateTime.Now);
🔹 7. Writing Logs to File without Serilog (Basic)
Use built-in file sink via [third-party providers] or manually with middleware, but generally Serilog is preferred for structured logging.

