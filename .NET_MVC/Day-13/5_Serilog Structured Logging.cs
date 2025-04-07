using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 5_Serilog Structured Logging
    {
        
    }
}
-----------------------------------------
Serilog Structured Logging ko samjhte hain — real world ke examples ke saath, bina table ke, jaise tu chah raha hai. 
Yeh topic important hai jab tu production level monitoring, debugging aur log analysis tools (jaise Seq, Kibana, Splunk) use karta hai.
---------------------------------------------------
🔥 What is Structured Logging?
Structured Logging ka matlab hota hai:

Logs ko plain text ke bajaye key-value pair format me likhna, 
taki logs searchable, filterable, and parsable ho jaayein kisi bhi logging system me.

Serilog me yeh bahut easy hai — aur isi wajah se Serilog popular hai.
---------------------------------------------------
🎯 Why Structured Logging?
🚫 Traditional Logging:

logger.LogInformation("User John logged in at 10:45 PM");
😐 Hard to filter by username ya time.
----------------------------------------------
✅ Structured Logging:

logger.LogInformation("User {Username} logged in at {LoginTime}", "John", DateTime.Now);
😎 Easily searchable by Username, LoginTime.
----------------------------------------------------
🛠 How to Use Structured Logging in Serilog
Step 1: Install Serilog

dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
-----------------------------------------------
Step 2: Configure in Program.cs

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
-----------------------------------------
✅ Example with Structured Logs

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login(string username)
    {
        _logger.LogInformation("User {Username} tried to login at {Time}", username, DateTime.UtcNow);

        // Imagine login logic...
        return View();
    }
}
-------------------------------------------
🔍 Log Output:

[10:45:01 INF] User "john.doe" tried to login at "2025-04-02T10:45:00Z"
This output is structured internally, even if Console shows as text. Tools like Seq or ElasticSearch treat these as JSON properties.
------------------------------------------
📦 Serilog Sink: JSON File for Structured Logging

.WriteTo.File(
  new Serilog.Formatting.Json.JsonFormatter(),
  "logs/log.json"
)
----------------------------------------------
Log file me aise dikhega:

{
  "Timestamp": "2025-04-02T10:45:00Z",
  "Level": "Information",
  "MessageTemplate": "User {Username} tried to login at {Time}",
  "Properties": {
    "Username": "john.doe",
    "Time": "2025-04-02T10:45:00Z"
  }
}
-----------------------------------------------
🔧 Use with Request Info
Tu enrichers se request data bhi structured log me add kar sakta hai:

.Enrich.WithMachineName()
.Enrich.WithClientIp()
.Enrich.WithCorrelationId()
-----------------------------------------------
🧠 Real-Life Usage
Audit Logs: Log.Information("Order {OrderId} placed by {UserId}", 1234, "john")

Security Logs: Log.Warning("Unauthorized access attempt by {IP}", ipAddress)

API Debugging: Log.Debug("Received payload: {@Payload}", jsonObject)

@ in {@Payload} means serialize as JSON instead of .ToString().
-----------------------------------------------------------
✅ Benefits of Structured Logging
🚀 Easily searchable: where Username = 'john'

🔍 Better filtering in tools like Seq/Splunk

🧾 Great for audit trails

📊 Supports dashboards & analytics

