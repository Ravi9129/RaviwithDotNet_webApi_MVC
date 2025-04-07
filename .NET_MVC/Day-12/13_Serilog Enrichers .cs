using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 13_Serilog Enrichers 
    {
        
    }
}
---------------------------------------
Serilog Enrichers ka kaam hota hai: logs ke context me extra information (properties) add karna — automatically! 🧠📈

🔥 What is an Enricher?
Serilog Enricher = A component that adds custom properties to every log entry.

Example:

[INF] User logged in
      MachineName=DESKTOP-01
      RequestId=0HMA123ABC

---------------------------------------
🔧 Example: Adding Enrichers in Program.cs

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.WithProperty("AppName", "MyApp") // Custom property
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
    --------------------------------------
✨ Enrichers in Action

[INF] User login attempt
      MachineName=DESKTOP-XYZ
      ThreadId=12
      ProcessId=13456
      AppName=MyApp
      ---------------------------------------------
📦 How to Install Enrichers

dotnet add package Serilog.Enrichers.Environment
dotnet add package Serilog.Enrichers.Thread
dotnet add package Serilog.Enrichers.Process
dotnet add package Serilog.Exceptions
dotnet add package Serilog.Enrichers.AspNetCore
dotnet add package Serilog.Enrichers.HttpContext
-----------------------------------------------------
🧠 Bonus: Custom Enricher
You can write your own:

public class CustomEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory pf)
    {
        var prop = pf.CreateProperty("MyCustomInfo", "Hello123");
        logEvent.AddPropertyIfAbsent(prop);
    }
}
------------------------------------------------
Then add:

.Enrich.With<CustomEnricher>()
-----------------------------------
📌 Summary:
✅ Enrichers = Add context info to logs automatically
✅ Useful for debugging, filtering in Seq, Kibana, etc.
✅ Combine multiple enrichers for full tracing

