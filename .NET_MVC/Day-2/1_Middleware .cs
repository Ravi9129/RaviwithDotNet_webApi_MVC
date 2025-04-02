using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 1_Middleware 
    {
        
    }
}
-------------------------------------------
Middleware – Kya, Kyu, Aur Kaise?
Agar aap ASP.NET Core application bana rahe ho, toh aapka request ka safar server tak direct nahi hota. Beech me kuch middlewares ka kaam hota hai jo request ko filter karte hain, modify karte hain ya stop kar dete hain.
------------------------------------------------------------------
1️⃣ Middleware Kya Hota Hai?
Middleware ek software component hota hai jo HTTP request aur response ke beech me kaam karta hai.

✔ Request ko process karta hai (authentication, logging, exception handling, routing).
✔ Response modify kar sakta hai (compression, caching, headers add karna).
✔ Multiple middlewares ek pipeline me kaam karte hain (Ek middleware se dusra middleware request pass kar sakta hai).
----------------------------------------------------------------------
2️⃣ Middleware Ka Real-World Example
Aap ek mall me gaye aur security check se guzar rahe ho:

1️⃣ Gatekeeper (Authentication Middleware) → Kya aapke pass valid entry pass hai?
2️⃣ Bag Checking (Logging Middleware) → Kya aap kuch suspicious carry to nahi kar rahe?
3️⃣ Metal Detector (Authorization Middleware) → Aapko VIP section me jaane ki permission hai ya nahi?
4️⃣ Mall Entry (Request Server Tak Pahuchti Hai)

Jab aap wapas ja rahe ho, toh ye reverse me hota hai aur response modify ho sakta hai.
-----------------------------------------------------------
3️⃣ Middleware Pipeline Kaise Kaam Karti Hai?

Ek ASP.NET Core application me request ka flow kuch aisa hota hai:

Client → Middleware 1 → Middleware 2 → Middleware 3 → Controller (Server)
---------------------------------------------------------
Aur response ka flow reverse hota hai:

Controller → Middleware 3 → Middleware 2 → Middleware 1 → Client
----------------------------------------------
4️⃣ Default Middlewares in ASP.NET Core
Agar aap ASP.NET Core application banate ho, toh kuch built-in middlewares automatically include hote hain:

✔ Authentication Middleware → User ka login check karta hai.
✔ Authorization Middleware → User ko access allow ya deny karta hai.
✔ Exception Handling Middleware → Agar koi error aaye toh handle karta hai.
✔ Routing Middleware → Request ko correct controller tak bhejta hai.
✔ Static Files Middleware → CSS, JS, images serve karta hai.
✔ CORS Middleware → Dusre domains se requests allow ya block karta hai.
-----------------------------------------------
5️⃣ Middleware Kaise Likhein? (Custom Middleware)
Agar aap apna khud ka middleware likhna chahte ho, toh aapko ek class banani hogi jo request process kare.
----------------------------------------
Step 1: Custom Middleware Banana
public class MyCustomMiddleware
{
    private readonly RequestDelegate _next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Request ke liye kaam karna
        Console.WriteLine("Custom Middleware: Request received");

        await _next(context); // Agla middleware call karega

        // Response modify karna
        Console.WriteLine("Custom Middleware: Response sent");
    }
}
-----------------------------------------
Step 2: Middleware Ko Register Karna (Program.cs me)

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<MyCustomMiddleware>(); // Custom Middleware Add Karo

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from ASP.NET Core!");
});

app.Run();
📌 Iska Matlab:
✔ Middleware request ke aate hi Console me log karega.
✔ Response bhejne se pehle wapas log karega.

6️⃣ Built-in Middleware Kaise Use Karein?
Agar aap ASP.NET Core ke default middlewares ko enable ya disable karna chahte ho, toh Program.cs file me modify kar sakte ho.
-------------------------------------------------------------------
Example 1: Exception Handling Middleware
Agar aap global error handling karna chahte ho, toh ye middleware use karo:
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseExceptionHandler("/error"); // Error page par redirect karega
app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware: Before Request");
    await next();
    Console.WriteLine("Middleware: After Response");
});

app.MapGet("/", () => "Hello World!");

app.Run();
✔ Ye middleware har request ke pehle aur baad me log karega.
✔ Agar koi exception aaye, toh /error page par redirect karega.
----------------------------------------------------------------
7️⃣ Real-World Scenario – Authentication Middleware
Maan lo ek E-commerce website hai, jisme sirf logged-in users checkout kar sakte hain.

Agar user login nahi hai, toh checkout middleware block kar dega:

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Unauthorized Access");
            return;
        }

        await _next(context);
    }
}
✔ Ye middleware request ko tabhi aage bhejega agar user authenticated hai.
✔ Agar user login nahi hai, toh 401 Unauthorized response dega.
--------------------------------------------
Register Middleware:

app.UseMiddleware<AuthMiddleware>();
------------------------------------
8️⃣ Conclusion
1️⃣ Middleware ek filter ki tarah kaam karta hai jo request aur response ko modify kar sakta hai.
2️⃣ ASP.NET Core me multiple built-in middlewares hote hain jaise Authentication, Routing, Exception Handling.
3️⃣ Custom Middleware likh ke aap request/response ko modify kar sakte ho.
4️⃣ Middleware pipeline ka order bahut important hota hai!
5️⃣ Real-world me authentication, logging, CORS, exception handling ke liye middleware ka use hota hai.