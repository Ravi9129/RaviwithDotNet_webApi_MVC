using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 11_Serilog + Seq
    {
        
    }
}
---------------------------------------------------
Serilog + Seq 🔍 ek powerful combo hai for structured log viewing.
Seq ek log server hai jo logs ko real-time visualize, search, and analyze karne deta hai.
---------------------------------------------
💡 What is Seq?
🧠 Think of it like:

Kibana, but made for Serilog

Real-time log dashboard

Rich querying

Instant filtering based on LogLevel, Source, Properties, etc.
---------------------------------------------------------
🔧 Step-by-Step: Serilog + Seq Setup
🔹 1. Install Seq Server (for local dev)
Install from 👉 https://datalust.co/download

After install:
🔗 http://localhost:5341
---------------------------------------------------
🔹 2. Install NuGet Package

dotnet add package Serilog.Sinks.Seq
---------------------------------------------------------
🔹 3. Configure in Program.cs

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341") // Seq server URL
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.MapGet("/", () =>
{
    Log.Information("🎉 Hello from Serilog + Seq!");
    Log.Warning("⚠️ This is a test warning");
    return "Hello Seq!";
});

app.Run();
--------------------------------------------
🔹 4. Run your app and visit:
📍 http://localhost:5341

You'll see logs like:

🎉 Hello from Serilog + Seq!
⚠️ This is a test warning
---------------------------------------
🧪 Result
You can:

Filter logs by levels: Information, Warning, Error
---------------------------------------------------
Search logs with queries:

Level = 'Error' and Exception like '%NullReferenceException%'
See custom properties (UserId, OrderId, etc.)
-----------------------------------------------------------
🔐 Bonus: Add custom properties

Log.Information("Order created {@Order}", new { OrderId = 123, Amount = 500 });
-------------------------------------------
🧠 Use-Cases
✅ Real-time dashboard during production
✅ Filters, search, monitoring
✅ DevOps & QA log visibility
✅ Supports alerts (paid version)
------------------------------------------------------
📦 Optional: appsettings.json Config

"Serilog": {
  "MinimumLevel": "Debug",
  "WriteTo": [
    {
      "Name": "Seq",
      "Args": { "serverUrl": "http://localhost:5341" }
    },
    {
      "Name": "Console"
    }
  ]
}
---------------------------------------------------------------------
And in Program.cs:

builder.Host.UseSerilog((ctx, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration));