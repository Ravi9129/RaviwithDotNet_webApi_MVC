using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 2_Custom Middleware Extension
    {
        
    }
}
------------------------------------------------------------
Custom Middleware Extension in ASP.NET Core
Agar aap custom middleware likh rahe ho, toh har jagah app.UseMiddleware<MyMiddleware>() likhna thoda messy ho sakta hai. Is problem ka solution hai Extension Method use karna.

1️⃣ Custom Middleware Extensions Kyu Banate Hain?
✔ Code Reusability → Har jagah app.UseMiddleware<MyMiddleware>() likhne ki zaroorat nahi.
✔ Code Cleanliness → Middleware ko encapsulate karke ek extension method ban sakta hai.
✔ Easy Integration → Middleware ko sirf ek line me register kar sakte ho.
---------------------------------------------------------------
2️⃣ Step-by-Step Custom Middleware Extension
Step 1: Custom Middleware Class Banayein
public class MyCustomMiddleware
{
    private readonly RequestDelegate _next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Request ko handle karna
        Console.WriteLine("Custom Middleware: Request received");

        await _next(context); // Agla middleware call karega

        // Response modify karna
        Console.WriteLine("Custom Middleware: Response sent");
    }
}
✅ Ye middleware request aur response dono ko log karega.
-----------------------------------------------------------------------------
Step 2: Middleware Extension Method Banayein
Ab middleware ko simplify karne ke liye ek extension method likho.

public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyCustomMiddleware>();
    }
}
✅ Ab hume har jagah app.UseMiddleware<MyCustomMiddleware>() likhne ki zaroorat nahi!
-----------------------------------------------------------
Step 3: Middleware Ko Register Karein (Program.cs)
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMyCustomMiddleware(); // 👈 Custom Middleware ka Extension Method Use Karo

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from ASP.NET Core!");
});

app.Run();
✅ Ab sirf ek line me middleware register ho gaya! 🚀
-------------------------------------------------------------
3️⃣ Real-World Example – Logging Middleware Extension
Agar aap ek logging middleware likhna chahte ho jo request ka data log kare, toh aap aise likh sakte ho:

Middleware Class:

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

        await _next(context);

        _logger.LogInformation($"Response: {context.Response.StatusCode}");
    }
}
--------------------------------------------------
Extension Method:
public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
---------------------------------------------------
Register Middleware in Program.cs

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseLoggingMiddleware(); // 👈 Custom Logging Middleware

app.MapGet("/", () => "Welcome to ASP.NET Core!");

app.Run();
✅ Ab sabhi requests aur responses log ho jayenge! 🔥
----------------------------------------------------------
4️⃣ Conclusion
1️⃣ Custom Middleware Extensions se code zyada clean aur reusable ho jata hai.
2️⃣ Middleware ko ek extension method me wrap karna best practice hai.
3️⃣ Real-world me authentication, logging, error handling jaise tasks ke liye extensions useful hote hain.
4️⃣ Middleware register karne ka tarika simple ho jata hai → app.UseMyCustomMiddleware();