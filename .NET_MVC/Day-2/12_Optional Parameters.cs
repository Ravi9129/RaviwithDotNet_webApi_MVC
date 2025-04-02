using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 12_Optional Parameters
    {
        
    }
}
--------------------------------------------
Optional Parameters in ASP.NET Core
1️⃣ Optional Parameters Kya Hote Hain?
🔹 Optional parameters woh parameters hote hain jo request me na bhi aaye toh error na aaye.
🔹 Ye method parameters aur route parameters dono me apply ho sakte hain.
🔹 Default Parameters aur Optional Parameters me farq:

Default Parameter (=): Agar koi value na aye toh ek predefined default value set hoti hai.

Optional Parameter (?): Agar parameter na aye toh null assign hota hai.
--------------------------------------------------------------
2️⃣ Optional Route Parameters in Minimal API
Agar hum route parameters ko optional banana chahein, toh {parameter?} syntax ka use karte hain.

Example: Optional Route Parameter

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/profile/{username?}", (string? username) =>
{
    return username != null ? $"Profile: {username}" : "Guest Profile";
});

app.Run();
✅ Agar user /profile/Ravi par request karega, toh response milega:
📝 "Profile: Ravi"

✅ Agar user /profile par request karega (koi username na de), toh response milega:
📝 "Guest Profile"
--------------------------------------------------------
3️⃣ Optional Parameters in Query Strings
Agar hum query parameters optional banana chahein, toh method ke parameters me nullable (?) use kar sakte hain.

Example: Optional Query Parameter

app.MapGet("/search", (string? category, int? page) =>
{
    return $"Category: {category ?? "Ravi"}, Page: {page ?? 1}";
});
✅ Agar user /search?category=Tech&page=2 par request karega, toh response milega:
📝 "Category: Tech, Page: 2"

✅ Agar user /search par request karega (koi parameter na de), toh response milega:
📝 "Category: Ravi, Page: 1"
-----------------------------------------------------------
4️⃣ Optional Parameters in MVC Controllers
Agar hum ASP.NET Core MVC Controllers me optional values dena chahein, toh parameters nullable (?) ya [FromQuery] attribute ka use kar sakte hain.

Example: Optional Parameter in Controller
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    [HttpGet("{id:int?}")]
    public IActionResult GetProduct(int? id)
    {
        return Ok($"Product ID: {id ?? 100}");
    }
}
✅ Agar user /api/products/50 par request karega, toh response milega:
📝 "Product ID: 50"

✅ Agar user /api/products par request karega (koi ID na de), toh response milega:
📝 "Product ID: 100"
--------------------------------------------------
5️⃣ Optional Parameters in Action Methods
Agar hum C# methods me optional parameters dena chahein, toh = operator ka use kar sakte hain.

Example: Optional Method Parameter
public IActionResult Greet(string name = "Guest")
{
    return Ok($"Hello, {name}!");
}
✅ Agar user /greet?name=Ali par request karega, toh response milega:
📝 "Hello, Ali!"

✅ Agar user /greet par request karega (koi name na de), toh response milega:
📝 "Hello, Guest!"
------------------------------------------
6️⃣ Conclusion
✔ Optional parameters se APIs aur routes flexible bante hain.
✔ Route parameters me ? use hota hai, query parameters me nullable (?) use hota hai.
✔ Agar koi default value chahiye, toh = operator use karna best practice hai.