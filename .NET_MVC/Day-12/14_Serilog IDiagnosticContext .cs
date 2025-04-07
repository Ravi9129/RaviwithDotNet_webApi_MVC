using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 14_Serilog IDiagnosticContext 
    {
        
    }
}
------------------------------------------------
IDiagnosticContext Serilog ka ek special context object hai jo request ke dauraan custom log properties 
add karne ke liye use hota hai — especially with Serilog + ASP.NET Core + Request Logging Middleware. 💡
------------------------------------------------
🔥 What is IDiagnosticContext?
Part of Serilog.AspNetCore

Allows you to push key-value pairs into the request log

Works with UseSerilogRequestLogging()

Automatically logs enriched values once per HTTP request
------------------------------------------
✅ Use Case

app.UseSerilogRequestLogging(); // Enable request logging
-------------------------------------
Default logs:

HTTP GET /api/values responded 200 in 34ms
-----------------------------------------------
With IDiagnosticContext:

HTTP GET /api/values responded 200 in 34ms
UserId=123  OrderId=999  CustomInfo=Yes
------------------------------------------------------
📦 How to Inject & Use IDiagnosticContext

public class MyController : Controller
{
    private readonly IDiagnosticContext _diagnosticContext;

    public MyController(IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = diagnosticContext;
    }

    public IActionResult Index()
    {
        _diagnosticContext.Set("UserId", 123);
        _diagnosticContext.Set("CustomInfo", "Some value");

        return View();
    }
}
-------------------------------------------------------
✅ These values will be logged when request ends, if UseSerilogRequestLogging() is enabled.
--------------------
🛠️ Real Example in Program.cs

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"]);
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
    };
});
-------------------------------------------------
📈 Output Log (With Enrichments)

HTTP GET /home/index responded 200 in 30.1234 ms
UserAgent=Mozilla/5.0 RequestHost=localhost:5001
