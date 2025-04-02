using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 6_Routing 
    {
        
    }
}
------------------------------------
Routing in ASP.NET Core
1️⃣ Routing Kya Hota Hai?
Routing ek mechanism hai jo incoming requests ko sahi controller/action method se map karta hai.

💡 Simpler Terms Me: Jab koi URL request aati hai, toh routing decide karti hai ki kaunsa controller aur action method execute hoga.

2️⃣ Types of Routing in ASP.NET Core
ASP.NET Core me 2 tarike ke routing hote hain:

Conventional Routing (MVC Controllers ke liye)

Attribute Routing (Specific Routes Define Karne ke liye)
----------------------------------------------------------------------
3️⃣ Conventional Routing (Default Routing)
✔ Ye MapControllerRoute() ke through set hota hai.
✔ Pattern-Based hota hai, jisme {controller}/{action}/{id?} jaise rules define hote hain.
✔ Mostly MVC applications me use hota hai.
---------------------------------------------
Example: Conventional Routing Setup

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting(); // 👈 Routing Enable Karna Zaroori Hai
-----------------------------
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"); // 👈 Default Routing Pattern
});

app.Run();
✅ Agar koi /products/details/10 request karega toh yeh route invoke hoga:

Controller: ProductsController
Action: Details(int id)
Id: 10
--------------------------------------------
4️⃣ Attribute Routing (Advanced Control)
✔ Controller ya Action ke upar [Route] attribute use karke routing define hoti hai.
✔ More flexible hai, kyunki har route ko manually define kar sakte ho.

Example: Attribute Routing
[Route("products")]
public class ProductsController : Controller
{
    [Route("details/{id}")] // 👈 Custom Route
    public IActionResult Details(int id)
    {
        return Content($"Product ID: {id}");
    }
}
✅ Agar user /products/details/10 ko request karega, toh yeh action invoke hoga.
------------------------------------------------------
5️⃣ Route Constraints (Validations in Routing)
✔ Constraints ka use specific types ya conditions enforce karne ke liye hota hai.
✔ Ye ensure karta hai ki incorrect data URL me na aaye.

Example: Sirf Numbers Allow Karna (int constraint)

[Route("products")]
public class ProductsController : Controller
{
    [Route("details/{id:int}")] // 👈 Sirf Integer ID Allow Karega
    public IActionResult Details(int id)
    {
        return Content($"Valid Product ID: {id}");
    }
}
❌ /products/details/abc (Invalid Request – 404 Error)
✅ /products/details/10 (Valid Request)
--------------------------------------------------------
6️⃣ Route Parameters & Optional Parameters
Agar kisi parameter ko optional banana ho, toh {id?} likhna padega.

[Route("user/profile/{id?}")] // 👈 ID Optional Hai
public IActionResult Profile(int? id)
{
    if (id == null)
        return Content("No User ID provided.");
    
    return Content($"User ID: {id}");
}
✅ /user/profile/10 → User ID: 10
✅ /user/profile → No User ID provided.
-----------------------------------------------------------
7️⃣ Route Prefix (Common Prefix for Controller)
Agar har route me ek common prefix ho, toh [RoutePrefix] define kar sakte ho.

Example:

[Route("api/products")]
public class ProductsController : Controller
{
    [HttpGet("all")]
    public IActionResult GetAllProducts()
    {
        return Content("All Products List");
    }

    [HttpGet("details/{id}")]
    public IActionResult GetProduct(int id)
    {
        return Content($"Product ID: {id}");
    }
}
✅ /api/products/all → GetAllProducts()
✅ /api/products/details/10 → GetProduct(10)
--------------------------------------------------------------
8️⃣ Endpoint Routing (New Way in .NET Core 3+)
✔ Endpoint routing .NET Core 3+ me introduce hua.
✔ Ye middleware based routing hai jo routes ko automatically detect karta hai.
-------------------------------------------
Example of Endpoint Routing
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "Hello from Home!");
    endpoints.MapGet("/about", () => "About Page");
});

app.Run();
✅ / → "Hello from Home!"
✅ /about → "About Page"
------------------------------------------------
💡 Rule of Thumb:
✔ Agar MVC Controller use kar rahe ho, toh Conventional Routing sahi hai.
✔ Agar API ya Dynamic Routes chahiye, toh Attribute Routing best hai.
----------------------------------------------------
🔟 Conclusion
✔ Routing requests ko sahi Controller ya API Endpoint tak le jaata hai.
✔ 2 tarike hain: Conventional & Attribute Routing.
✔ Attribute Routing APIs ke liye best hota hai.
✔ Endpoint Routing .NET Core 3+ me naya feature hai jo modern apps me use hota hai.