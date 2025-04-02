using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 3_Custom Conventional Middleware Class
    {
        
    }
}
-------------------------------------------
Custom Conventional Middleware Class in ASP.NET Core
Agar tum ASP.NET Core me ek custom middleware likhna chahte ho jo conventional tareeke se kaam kare, toh tumhe ek class banani hogi jo request pipeline me insert ho sake.

1️⃣ Conventional Middleware Kya Hota Hai?
✔ Request-Response Pipeline ka ek part hota hai.
✔ Custom Logic implement karta hai jaise logging, authentication, exception handling, etc.
✔ Middleware Class me Invoke method hota hai, jo request ko next middleware tak bhejta hai ya response modify karta hai.
------------------------------------------------------------
2️⃣ Step-by-Step: Custom Conventional Middleware
Step 1: Custom Middleware Class Banayein
Pehle ek middleware class banao jo request aur response ko process kare.

public class MyCustomMiddleware
{
    private readonly RequestDelegate _next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Request se pehle kaam karega
        Console.WriteLine("Before request processing: " + context.Request.Path);

        await _next(context); // Agla middleware ya endpoint ko call karega

        // Response ke baad kaam karega
        Console.WriteLine("After response processing: " + context.Response.StatusCode);
    }
}
✅ Ye middleware request ke pehle aur response ke baad log karega.
--------------------------------------
Step 2: Middleware Ko Register Karna (Program.cs)
Ab tumhe ye middleware request pipeline me register karna hoga.

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<MyCustomMiddleware>(); // 👈 Conventional Middleware Ko Add Karo

app.MapGet("/", () => "Hello from ASP.NET Core!");

app.Run();
✅ Ab har request ke pehle aur response ke baad console me log aayega! 🚀
------------------------------------------------------------------
3️⃣ Real-World Example – Authentication Middleware
Agar tum authentication middleware likhna chahte ho jo API requests me token check kare, toh aise likho:
--------------
Custom Authentication Middleware
public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Header me "Authorization" check karo
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Unauthorized Access");
            return;
        }

        await _next(context); // Agla middleware ya controller call karega
    }
}
✅ Agar request me Authorization header nahi hoga, toh request reject ho jayegi.
-----------------------------------------------------
Middleware Register Karna

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<AuthMiddleware>(); // 👈 Authentication Middleware

app.MapGet("/", () => "Welcome, Authenticated User!");

app.Run();
✅ Sirf authorized users ko response milega. 🔥
--------------------------------------------
4️⃣ Conclusion
1️⃣ Conventional Middleware ek class hoti hai jo request-response pipeline ko modify karti hai.
2️⃣ Invoke method request ko process karta hai ya next middleware tak bhejta hai.
3️⃣ Middleware authentication, logging, error handling ke liye useful hoti hai.
4️⃣ Register karne ke liye app.UseMiddleware<MyMiddleware>() use hota hai.