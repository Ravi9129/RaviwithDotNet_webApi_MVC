using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 4_Logging Providers
    {
        
    }
}
--------------------------------------
Logging Providers .NET mein wo components hote hain jo logs ko kaha aur kaise store karna hai ye decide karte hain — Console, File, Debug output, Cloud, Database, sab kuch cover hota hai.

🔥 Logging Providers in .NET
.NET out of the box kuch built-in providers ke saath aata hai, aur aap custom ya third-party providers bhi add kar sakte ho.
-------------------------------------------------------
✅ 1. Built-in Logging Providers
Provider	Description
Console	Logs terminal/command prompt pe dikhte hain
Debug	Logs Debug output window (Visual Studio etc.) mein dikhte hain
EventSource	Windows Event Tracing ke liye
EventLog (Windows only)	Windows Event Viewer ke liye
TraceSource	System.Diagnostics ke saath integrate hota hai
AzureAppServices	Azure ke App Services ke logs
---------------------------------------------------
⚙️ Add Built-in Providers

builder.Logging.ClearProviders(); // optional
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventLog(); // Windows only
-----------------------------------------------------
🌟 2. Popular Third-Party Logging Providers
Provider	Features
Serilog	Structured logging, sinks (file, DB, Elastic, etc.)
NLog	Powerful rules, formats, multiple targets
log4net	Apache-style logging system
Elmah	Web app error logging
Application Insights	Microsoft Azure monitoring & telemetry
Seq	Structured log server
----------------------------------------
🔥 Serilog Example

dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
---------------------------------------------
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
------------------------------------------
🧠 Custom Provider Example
Aap apna khud ka logging provider bhi bana sakte ho:

public class MyCustomLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new MyCustomLogger();
    public void Dispose() { }
}
-----------------------------------------------------
public class MyCustomLogger : ILogger
{
    public IDisposable BeginScope<TState>(TState state) => null;
    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        File.AppendAllText("mylogs.txt", formatter(state, exception));
    }
}
-----------------------------------
Register:

builder.Logging.AddProvider(new MyCustomLoggerProvider());
---------------------------------------------
🔒 Provider Selection by Environment
Use different providers in different environments:

if (app.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();
}
else
{
    builder.Logging.AddFile("prodlogs.txt");
}
--------------------------------------
🔚 Summary Table
Provider Type	Example Method
Console	AddConsole()
Debug	AddDebug()
File (via Serilog)	AddSerilog() with File Sink
Windows EventLog	AddEventLog()
Azure App Insights	AddApplicationInsights()
Custom	AddProvider(new MyLogger())