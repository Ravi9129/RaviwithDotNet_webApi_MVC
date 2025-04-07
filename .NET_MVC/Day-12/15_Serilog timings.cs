using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 15_Serilog timings
    {
        
    }
}
------------------------------------------
 Serilog timings ka matlab hota hai:
"Request ya code block kitna time le raha hai — uska track rakhna aur log karna."
--------------------------------
ASP.NET Core me Serilog aise log karta hai:

HTTP GET /home/index responded 200 in **32.1234 ms**
Ye timing kaha se aayi? Ye automatically log hoti hai jab tu UseSerilogRequestLogging() middleware use karta hai.
-----------------------------------------
✅ Setup for Request Timing (Basic)
Program.cs me:

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "Handled {RequestPath} in {Elapsed:0.0000} ms";
});
🔍 Elapsed = Time taken to handle the request
🕐 Format {Elapsed:0.0000} = 4 decimal places (ms)
-----------------------------------------
🔥 Custom Timing for Any Code Block
Agar tu kisi method ya DB call ka specific time measure karna chahta hai:

using Serilog;
using System.Diagnostics;

public class MyService
{
    private readonly ILogger<MyService> _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public async Task DoWorkAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        // Simulate work
        await Task.Delay(300);

        stopwatch.Stop();
        _logger.LogInformation("DoWorkAsync took {ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
    }
}
----------------------------------
🧠 OR Serilog ka Log.ForContext & OperationTiming

using (LogContext.PushProperty("Scope", "PaymentService"))
using (var timer = SerilogTimings.Operation.Begin("Processing Payment"))
{
    // Simulated work
    await Task.Delay(500);

    timer.Complete(); // Log this only if successful
}
-------------------------------
Output:

[08:40:21 INF] Processing Payment completed in 503.46 ms
----------------------------------------------
👉 Requires SerilogTimings NuGet package

dotnet add package SerilogTimings