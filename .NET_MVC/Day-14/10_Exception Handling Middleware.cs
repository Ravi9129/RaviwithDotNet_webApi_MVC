using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 10_Exception Handling Middleware
    {
        
    }
}
------------------------------------------
Exception Handling Middleware ASP.NET Core me ek powerful way hai runtime pe aayi 
hui errors ko centralized tareeke se handle karne ka.
Na ki har controller me try-catch daal ke ganda karna.

🔍 Definition:
Exception Handling Middleware ek custom middleware hota hai jo pipeline me sabse pehle lagta hai aur global level pe exceptions 
ko catch karta hai, log karta hai, aur custom response bhejta hai.
--------------------------------------------------------------
💥 Real-World Scenario:
Tu ek large-scale API bana raha hai. Kabhi DB down ho jata hai, kabhi koi parameter missing hota hai.
Har controller/action me try-catch likhne se acha hai ek central place bana lo jahan:

Error ko log kiya jaye (ILogger / Serilog)

Client ko standardized error response mile (JSON)

Sensitive details hide kiye jaye
-------------------------------------------------------------
🔨 Step-by-Step Implementation
✅ Step 1: Create ExceptionMiddleware.cs

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new
                {
                    statusCode = context.Response.StatusCode,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                }
                : new
                {
                    statusCode = context.Response.StatusCode,
                    message = "Internal Server Error"
                };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
---------------------------------------------
✅ Step 2: Register Middleware in Program.cs

app.UseMiddleware<ExceptionMiddleware>();
⚠️ Important: Ye middleware sabse pehle hona chahiye, UseRouting() se pehle bhi ho sakta hai.
------------------------------------------
✅ Optional: Hide Details in Production

// Use IHostEnvironment to show stackTrace only in dev
_env.IsDevelopment()
---------------------------------------------
✅ Bonus: Create Extension Method (Clean Code)

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
-----------------------------
And then use it like:

app.UseCustomExceptionMiddleware();
-------------------------------
📦 Real Use-Cases
Scenario	What Happens?
DB connection fails	Logs error, returns 500 JSON error
Invalid API input	Can be caught and shaped accordingly
Unhandled exception anywhere	One central place to log + respond
Logs to Serilog/File	✅ Works seamlessly with logger integrations
------------------------------
✅ Output Example (Dev)

{
  "statusCode": 500,
  "message": "Object reference not set...",
  "stackTrace": "at Namespace.Class..."
}
-------------------------------------------------
✅ Output Example (Prod)

{
  "statusCode": 500,
  "message": "Internal Server Error"
}
