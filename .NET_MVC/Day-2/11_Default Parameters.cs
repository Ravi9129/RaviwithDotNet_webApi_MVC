using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 11_Default Parameters
    {
        
    }
}
-------------------------------------------------
Default Parameters in ASP.NET Core
1️⃣ Default Parameters Kya Hote Hain?
🔹 Default parameters ka use tab hota hai jab koi route parameter ya function argument provide na ho.
🔹 Isse optional behavior add kiya jata hai taaki agar user parameter na de, toh ek predefined default value use ho sake.
🔹 Default parameters route parameters aur method parameters dono me apply ho sakte hain.
-------------------------------------------------------
2️⃣ Default Route Parameters in Minimal API
Agar hum route parameters me default value set karna chahein, toh hum {parameter=defaultValue} syntax ka use kar sakte hain.

Example: Default Route Parameter

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/category/{name=General}", (string name) =>
{
    return $"Category: {name}";
});

app.Run();
✅ Agar user /category/Technology par request karega, toh response milega:
📝 "Category: Technology"

✅ Agar user /category par request karega (koi value na de), toh response milega:
📝 "Category: General"
--------------------------------------------------
3️⃣ Default Parameters in Method Arguments
Agar hum C# method parameters me default values set karna chahein, toh hum = operator ka use kar sakte hain.

Example: Default Method Parameter
app.MapGet("/greet", (string name = "Guest") =>
{
    return $"Hello, {name}!";
});
✅ Agar user /greet?name=Ravi par request karega, toh response milega:
📝 "Hello, Ravi!"

✅ Agar user /greet par request karega (koi name na de), toh response milega:
📝 "Hello, Guest!"
--------------------------------------------------------
4️⃣ Default Parameters in MVC Controllers
Agar hum ASP.NET Core MVC Controllers me default values dena chahein, toh method ke parameters me default value define kar sakte hain.

Example: Default Parameter in Controller
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    [HttpGet("{id:int?}")]
    public IActionResult GetProduct(int id = 100)
    {
        return Ok($"Product ID: {id}");
    }
}
✅ Agar user /api/products/50 par request karega, toh response milega:
📝 "Product ID: 50"

✅ Agar user /api/products par request karega (koi ID na de), toh response milega:
📝 "Product ID: 100"
---------------------------------------------------------
5️⃣ Default Values with Query Strings
Agar hum query parameters ke liye default values dena chahein, toh method parameters me default values define kar sakte hain.

Example: Default Query Parameter

app.MapGet("/search", (string category = "All", int page = 1) =>
{
    return $"Category: {category}, Page: {page}";
});
✅ Agar user /search?category=Tech&page=2 par request karega, toh response milega:
📝 "Category: Tech, Page: 2"

✅ Agar user /search par request karega (koi query string na de), toh response milega:
📝 "Category: All, Page: 1"
---------------------------------------------------------
6️⃣ Default Parameter vs Optional Parameter
🔹 Default Parameter → Jab parameter missing ho, toh ek predefined value use hoti hai.
🔹 Optional Parameter (?) → Parameter ho bhi sakta hai aur nahi bhi, lekin null assign hota hai.

Example: Optional Route Parameter
app.MapGet("/profile/{username?}", (string? username) =>
{
    return username != null ? $"Profile: {username}" : "Guest Profile";
});
✅ Agar user /profile/Ahmed par request karega, toh response milega:
📝 "Profile: Ahmed"

✅ Agar user /profile par request karega (koi username na de), toh response milega:
📝 "Guest Profile"
-------------------------------------------------
7️⃣ Conclusion
✔ Default parameters ka use tab hota hai jab ek predefined value chahiye agar user kuch na provide kare.
✔ Minimal APIs aur MVC Controllers dono me default values set ki ja sakti hain.
✔ Default values se routes aur APIs flexible aur user-friendly banti hain.
✔ Agar optional banana ho, toh ? use kar sakte hain, lekin agar ek specific default value chahiye, 
toh = operator use karna best practice hai.