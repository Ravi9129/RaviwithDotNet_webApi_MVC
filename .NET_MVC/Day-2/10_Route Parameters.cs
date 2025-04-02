using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 10_Route Parameters
    {
        
    }
}
-----------------------------------
Route Parameters in ASP.NET Core
1️⃣ Route Parameters Kya Hote Hain?
🔹 Route parameters ka use URL ke andar dynamic values pass karne ke liye hota hai.
🔹 Yeh curly braces {} ke andar likhe jate hain, jisme parameter ka naam hota hai.
🔹 Route parameters GET, POST, PUT, DELETE sabhi HTTP methods ke sath kaam karte hain.
🔹 Route parameters required hote hain, agar optional banana ho toh default value ya nullable type use karni padti hai.
-------------------------------------------------
2️⃣ Basic Route Parameter Example
Agar hume kisi user ka data id ke basis par lana ho, toh hum route parameter use kar sakte hain.

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/user/{id}", (int id) =>
{
    return $"User ID: {id}";
});

app.Run();
✅ Agar user /user/5 par GET request karega, toh response milega:
📝 "User ID: 5"
-----------------------------------------------------
3️⃣ Multiple Route Parameters
Agar hume ek se zyada parameters pass karne ho, toh hum multiple parameters define kar sakte hain.

app.MapGet("/order/{orderId}/user/{userId}", (int orderId, int userId) =>
{
    return $"Order ID: {orderId}, User ID: {userId}";
});
✅ Agar user /order/1001/user/50 par GET request karega, toh response milega:
📝 "Order ID: 1001, User ID: 50"
-------------------------------------------------------
4️⃣ Optional Route Parameters
Agar koi parameter optional banana ho, toh hum ? (nullable type) use kar sakte hain.

app.MapGet("/product/{id?}", (int? id) =>
{
    return id.HasValue ? $"Product ID: {id}" : "No Product ID provided";
});
✅ Agar user /product/10 par GET request karega, toh response milega:
📝 "Product ID: 10"

✅ Agar user /product par GET request karega (bina id ke), toh response milega:
📝 "No Product ID provided"
------------------------------------------
5️⃣ Default Value in Route Parameters
Agar hume kisi optional parameter ki default value set karni ho, toh hum default value specify kar sakte hain.

app.MapGet("/category/{name=General}", (string name) =>
{
    return $"Category: {name}";
});
✅ Agar user /category/Technology par GET request karega, toh response milega:
📝 "Category: Technology"

✅ Agar user /category par GET request karega, toh response milega (default value se):
📝 "Category: General"
-------------------------------------------------------
6️⃣ Route Parameters in MVC Controller
Agar hum ASP.NET Core MVC use kar rahe hain, toh route parameters controllers ke andar bhi define kiye ja sakte hain.

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        return Ok($"Product ID: {id}");
    }
}
✅ Agar user /api/products/20 par GET request karega, toh response milega:
📝 "Product ID: 20"
--------------------------------------------------------
7️⃣ Constraints on Route Parameters
Agar hume specific data type ya format enforce karna ho, toh route constraints use karte hain.

Example: Sirf Integer Values Allow Karna
app.MapGet("/employee/{id:int}", (int id) =>
{
    return $"Employee ID: {id}";
});
✅ Agar user /employee/5 par request karega, toh ye kaam karega.
❌ Agar user /employee/john likhega, toh error aayega.
--------------------------------------------
8️⃣ Route vs Query String
Difference between Route Parameters & Query Strings:

Feature	Route Parameter	Query String
Syntax	/user/{id}	/user?id=10
Use Case	Required values	Optional values
Readability	Clean & RESTful	Less readable
Security	More secure	Less secure (visible in URL)
Example:
🔹 Route Parameter: /user/10 (RESTful)
🔹 Query String: /user?id=10 (Less readable)
-----------------------------------------------
9️⃣ Conclusion
✔ Route parameters dynamic URLs banane ke liye use hote hain.
✔ {id} syntax ka use karke dynamic values pass ki jati hain.
✔ Multiple parameters, optional values, aur constraints bhi define kiye ja sakte hain.
✔ MVC controllers aur minimal APIs dono me route parameters ka use hota hai.