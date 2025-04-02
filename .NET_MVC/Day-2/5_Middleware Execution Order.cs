using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 4_Middleware Execution Order
    {
        
    }
}
-------------------------------------
Middleware Execution Order in ASP.NET Core
Middleware execution order bohot important hota hai, kyunki ye request aur response ko modify kar sakta hai. Agar middleware ka order galat ho, toh unexpected behavior aa sakta hai.
------------------------------------------
1️⃣ Middleware Execution Flow
ASP.NET Core me middleware ek pipeline ki tarah kaam karta hai. Jab ek request aati hai, toh:

First Middleware chalta hai.

Ye middleware next middleware ko call karta hai.

Sabhi middlewares execute hote hain jab tak request endpoint tak nahi pahunchti.

Response wapas middlewares ke through flow hota hai.

💡 Middleware ek “chain” ki tarah kaam karta hai, jisme har middleware request ko aage bhej sakta hai ya response modify kar sakta hai.
--------------------------------------------------------------
2️⃣ Correct Order of Middleware
✅ Middleware execution ka sahi order ye hota hai:
Exception Handling Middleware (Sabse pehle errors handle kare)

Static Files Middleware (CSS, JS, images serve kare)

Routing Middleware (Request ka sahi endpoint identify kare)

Authentication Middleware (User authentication check kare)

Authorization Middleware (User ke access rights check kare)

Business Logic Middlewares (Logging, Caching, Rate Limiting, etc.)

Endpoint Middleware (Actual Controller ya API call)

Response Modification Middleware (Response ko modify kare, jaise Compression)
-----------------------------------------------------
3️⃣ Example of Correct Middleware Order
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 1️⃣ Exception Handling Middleware (Sabse Pehle Error Handle Kare)
app.UseExceptionHandler("/error");

// 2️⃣ Serve Static Files (wwwroot me se CSS, JS, Images serve kare)
app.UseStaticFiles();

// 3️⃣ Routing Middleware (Request ka Route Determine kare)
app.UseRouting();

// 4️⃣ Authentication Middleware (User ko Authenticate kare)
app.UseAuthentication();

// 5️⃣ Authorization Middleware (User ke access rights check kare)
app.UseAuthorization();

// 6️⃣ Custom Middleware (Logging ya Custom Processing)
app.UseMiddleware<MyLoggingMiddleware>();

// 7️⃣ Map Routes (Controllers, API Endpoints)
app.MapControllers(); 

app.Run();
✅ Iss order me middleware correctly execute honge!
-----------------------------------------------
4️⃣ Wrong Order and Its Issues
💀 Agar middleware ka order galat ho, toh problems ho sakti hain.
------------------------------
❌ Example of Wrong Order

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();  // 🔴 Routing sabse pehle aagaya
app.UseAuthorization();  // ❌ Authentication ke bina authorization check ho raha hai
app.UseAuthentication(); // ❌ Authentication baad me ho raha hai

app.UseStaticFiles(); // 🔴 Static files ko pehle hona chahiye

app.UseExceptionHandler("/error"); // ❌ Exception handling sabse last me, jo sahi nahi hai

app.MapControllers();

app.Run();
🔥 Issues in Wrong Order:
❌ Static files middleware baad me hai, toh CSS/JS properly load nahi honge.
❌ Authorization pehle ho raha hai authentication ke bina, toh error aayega.
❌ Exception handling last me hone se errors properly handle nahi honge.
---------------------------------------------------------
5️⃣ Conclusion
✔ Middleware pipeline ek top-down order me execute hoti hai.
✔ Pehle request handle hone wali middlewares aani chahiye.
✔ Authentication hamesha Authorization se pehle aayega.
✔ Static files aur exception handling sabse pehle honi chahiye.
✔ Middleware order sahi na ho toh security aur performance issues ho sakte hain.