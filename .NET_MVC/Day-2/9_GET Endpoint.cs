using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 9_GET Endpoint
    {
        
    }
}
----------------------------------------------------
GET Endpoint in ASP.NET Core
1️⃣ GET Endpoint Kya Hota Hai?
GET endpoint ek HTTP GET request handle karta hai. Iska main kaam server se data retrieve karna hota hai bina kisi data modification ke.

🔹 GET request safe & idempotent hoti hai (same request multiple times bhejne par bhi data change nahi hota).
🔹 Common Use Cases:
✔ API se data fetch karna (e.g., GetAllUsers, GetProductById)
✔ Static pages ya resources load karna
✔ Query parameters ke through filtering/sorting
------------------------------------------------------------
2️⃣ Basic GET Endpoint
Example: Simple GET Endpoint using MapGet()
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/hello", () => "Hello from GET endpoint!");

app.Run();
✅ Agar user /hello par GET request karega, toh response milega:
📝 "Hello from GET endpoint!"
------------------------------------------------
3️⃣ GET Endpoint with Parameters
Agar hume URL se ek parameter receive karna hai (e.g., kisi id ke basis pe user data lana ho), toh hum route parameters ya query strings use kar sakte hain.

Example: GET Endpoint with Route Parameter
app.MapGet("/user/{id}", (int id) =>
{
    return $"User ID: {id}";
});
✅ Agar user /user/5 par GET request karega, toh response milega:
📝 "User ID: 5"
------------------------------------------------
4️⃣ GET Endpoint with Query String
Agar hume multiple parameters pass karne hain, toh query string ka use karte hain.

Example: GET Endpoint with Query String
app.MapGet("/search", (string? name, int? age) =>
{
    return $"Search Results for: Name={name}, Age={age}";
});
✅ Agar user /search?name=Ali&age=25 par GET request karega, toh response milega:
📝 "Search Results for: Name=Ali, Age=25"
------------------------------------------------------------
5️⃣ GET Endpoint using Controller
Minimal APIs ke alawa hum MVC pattern bhi use kar sakte hain jisme Controller ke andar GET methods likhte hain.

Example: GET Endpoint inside a Controller
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        return Ok($"User ID: {id}");
    }
}
✅ Agar user /api/users/10 par GET request karega, toh response milega:
📝 "User ID: 10"

-------------------------------------------------------------
7️⃣ Conclusion
✔ GET endpoint sirf data fetch karne ke liye hota hai.
✔ MapGet() minimal API me simple aur lightweight approach hai.
✔ MVC Controllers me complex logic handle karna easy hota hai.
✔ GET request safe & fast hoti hai.