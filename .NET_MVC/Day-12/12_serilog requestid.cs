using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 12_serilog requestid
    {
        
    }
}
--------------------------------------------------------
Serilog me RequestId log karna kaafi useful hota hai — especially jab aapko ek specific request ke flow ko trace karna ho in logs. 🧠🔍

🔥 Why use RequestId?
Har incoming HTTP request ko ek unique RequestId assign hota hai.

Ye use karke aap easily trace kar sakte ho ki ek request ke through kya-kya hua.

Perfect for debugging distributed systems or microservices.

✅ Goal:
Log every request with its RequestId in Serilog (and show in Seq or file logs).
------------------------------------------------
🛠️ Step-by-Step Setup
🔹 1. Enable RequestId Middleware
ASP.NET Core automatically assigns a unique ID to each request (TraceIdentifier).

To log it with Serilog, we can enrich it.
-------------------------------------------------
🔹 2. Add Enricher Package
dotnet add package Serilog.Enrichers.AspNetCore
----------------------------------------------
Or for HttpContext enricher:

dotnet add package Serilog.Enrichers.HttpContext
---------------------------------------------
🔹 3. Configure in Program.cs
using Serilog;
using Serilog.Enrichers.AspNetCore;
using Serilog.Enrichers.HttpContext;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Configure Serilog with enrichment
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithHttpContext() // provides RequestId
    .Enrich.WithProperty("AppName", "MyApp")
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Optional: log the RequestId in middleware
app.Use(async (context, next) =>
{
    var requestId = context.TraceIdentifier;
    Log.ForContext("RequestId", requestId)
       .Information("🌐 Incoming request: {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);
    
    await next();
});

app.MapGet("/", () => "Hello with RequestId 🚀");

app.Run();
--------------------------------
🧪 Output Example (in Console or Seq):

[INF] 🌐 Incoming request: GET /
      RequestId: 0HMA1KLOEGRRC:00000001
      -------------------------------------------------------
🧠 Bonus: Add RequestId in Logs Automatically for All Requests
--------------------------------------------
Use app.UseSerilogRequestLogging():

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
    };
});
This will log every request with method, path, status, duration and the RequestId.
------------------------------------------
🧪 In Seq Query:
RequestId = '0HMA1KLOEGRRC:00000001'
---------------------------------------------------------
💡 Summary:
✅ Add RequestId per request
✅ Useful for tracking request lifecycle
✅ Helps in distributed/microservice debugging
✅ Easily viewed & filtered in Seq

