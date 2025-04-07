using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 9_Serilog File Sink
    {
        
    }
}
----------------------------------------------
Serilog File Sink ka use hum tab karte hain jab hume logs file me save karne hote hain — jaise log.txt, daily logs, rolling files, etc. Yeh production me sabse common logging method hai.
----------------------------------------------------------
🔧 Step-by-Step Setup: Serilog File Sink
--------------
🔹 1. Install NuGet Package

dotnet add package Serilog.Sinks.File
-----------------------------------------------------------------
🔹 2. Configure File Sink in Program.cs

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // 👈 file sink
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(); // Use Serilog instead of default logger

var app = builder.Build();
app.Run();
📁 This will:

Create a Logs folder

Create daily log files: log-20250402.txt, log-20250403.txt, etc.
-------------------------------------------------
🔍 Options in File Sink
Option	Description
path	File path with optional date placeholder
rollingInterval	Day, Hour, Minute, etc.
retainedFileCountLimit	Number of log files to keep
fileSizeLimitBytes	Max size of one log file
rollOnFileSizeLimit	Split file if size exceeds limit
------------------------------------------------------
✅ Example with all options:

.WriteTo.File(
    path: "Logs/myapp-.log",
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 7,
    fileSizeLimitBytes: 10_000_000, // 10 MB
    rollOnFileSizeLimit: true,
    shared: true,
    flushToDiskInterval: TimeSpan.FromSeconds(1)
)
------------------------------------------
📦 File Log Example Output:

2025-04-02 10:23:14.350 +00:00 [INF] User logged in at 10:23 AM
2025-04-02 10:24:20.981 +00:00 [ERR] Failed to retrieve user info: NullReferenceException
------------------------------------------------
📁 File Location Tips
Use Path.Combine("Logs", ...) for cross-platform paths

You can also log to desktop: "C:\\Users\\YourName\\Desktop\\log.txt" (not recommended for production)