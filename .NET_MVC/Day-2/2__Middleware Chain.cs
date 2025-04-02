using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 2__Middleware Chain
    {
        
    }
}
----------------------------
Middleware Chain in ASP.NET Core
1️⃣ Middleware Chain Kya Hota Hai?
Middleware ASP.NET Core ka ek request pipeline system hai jisme multiple middleware components ek sequence me execute hote hain.

✔ Request middleware chain me enter hoti hai.
✔ Har middleware request ko modify kar sakta hai ya aage pass kar sakta hai.
✔ Response bhi isi chain ke reverse order me pass hota hai.
------------------------------------------------
2️⃣ Middleware Chain Ka Flow
🔹 Request -> Middleware 1 -> Middleware 2 -> Middleware 3 -> Controller
🔹 Response <- Middleware 3 <- Middleware 2 <- Middleware 1 <- Client

💡 Har middleware do kaam kar sakta hai:
✔ Request ko modify karna ya reject karna.
✔ Request ko aage pass karna (next() method se).
------------------------------------------------
3️⃣ Example: Middleware Chain Execution
👇 Ye ek basic example hai jo middleware chain ko dikhata hai:

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware 1
app.Use(async (context, next) =>
{
    Console.WriteLine("🚀 Middleware 1: Request received");
    await next(); // Next middleware call
    Console.WriteLine("🔙 Middleware 1: Response going back");
});

// Middleware 2
app.Use(async (context, next) =>
{
    Console.WriteLine("📦 Middleware 2: Processing request");
    await next();
    Console.WriteLine("📦 Middleware 2: Processing response");
});

// Middleware 3 (Terminal Middleware)
app.Run(async (context) =>
{
    Console.WriteLine("✅ Middleware 3: Generating Response");
    await context.Response.WriteAsync("Hello from Middleware 3!");
});

app.Run();
Execution Flow:
1️⃣ Request enters Middleware 1 (🚀 Middleware 1: Request received)
2️⃣ Middleware 1 passes request to Middleware 2 (📦 Middleware 2: Processing request)
3️⃣ Middleware 2 passes request to Middleware 3 (✅ Middleware 3: Generating Response)
4️⃣ Middleware 3 sends response
5️⃣ Middleware 2 processes response (📦 Middleware 2: Processing response)
6️⃣ Middleware 1 processes response (🔙 Middleware 1: Response going back)

✅ Final Response: "Hello from Middleware 3!"
-----------------------------------------------
4️⃣ Correct Middleware Execution Order
Middleware sequence matters, agar order galat ho toh application properly kaam nahi karegi.

💡 Best Practice Order:
1️⃣ Exception Handling Middleware (app.UseExceptionHandler())
2️⃣ Static Files Middleware (app.UseStaticFiles())
3️⃣ Routing Middleware (app.UseRouting())
4️⃣ Authentication & Authorization Middleware (app.UseAuthentication(), app.UseAuthorization())
5️⃣ Custom Middleware (Logging, Security, Caching, etc.)
6️⃣ Endpoint Middleware (app.UseEndpoints())
--------------------------------------------------------
5️⃣ Example: Correct Middleware Order

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseExceptionHandler("/error"); // ✅ Global Error Handling Middleware
app.UseStaticFiles(); // ✅ Serve Static Files (CSS, JS, Images)

app.UseRouting(); // ✅ Enable Routing
app.UseAuthentication(); // ✅ User Authentication Middleware
app.UseAuthorization(); // ✅ User Authorization Middleware

app.UseMiddleware<CustomLoggingMiddleware>(); // ✅ Custom Middleware

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // ✅ Controller Mapping
});

app.Run();
✅ Agar order galat hoga, toh authentication ya static files sahi se load nahi honge.
-----------------------------------
6️⃣ Middleware Chain Use Kab Karein?
✔ Logging → Request aur response track karne ke liye.
✔ Authentication & Authorization → User identity verify karne ke liye.
✔ Caching → Performance optimize karne ke liye.
✔ Exception Handling → Errors ko handle karne ke liye.
✔ Custom Business Logic → IP Filtering, Response Compression, etc.
-----------------------------------
7️⃣ Conclusion
✔ Middleware ek chain-based system hai jo request-response pipeline ko manage karta hai.
✔ Order bahut important hai, warna application galat behave karegi.
✔ app.Use(), app.Run() aur app.UseMiddleware<T>() middleware add karne ke tarike hain.