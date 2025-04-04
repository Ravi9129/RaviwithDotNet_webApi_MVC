using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 3_FromBody
    {
        
    }
}
-----------------------------------------------
🔹 [FromBody] in ASP.NET Core
📌 Kya Hai?
[FromBody] attribute request body se JSON/XML data ko model me bind karne ke liye use hota hai. Jab API me POST, PUT, ya PATCH request aati hai, tab request body ka data ek model me map hota hai.

📌 Kab Use Karein?
✅ Jab request body se JSON/XML format me complex object receive karna ho.
✅ Jab Form-data ya Query-string se bind karne ki zaroorat na ho.
✅ Jab Large data ko send karna ho jo Query ya Route se possible nahi ho.
------------------------------------------------------
🛠 Example 1: Simple Model Binding with [FromBody]
👨‍💻 Model Class
public class UserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}
-----------------------------------------------------
🚀 API Controller
[HttpPost("create-user")]
public IActionResult CreateUser([FromBody] UserModel user)
{
    return Ok($"User {user.Name} with email {user.Email} created successfully!");
}
---------------------------------------------------
📤 Client Request (JSON Body)
{
    "name": "John Doe",
    "email": "john@example.com"
}
🎯 Response:
User John Doe with email john@example.com created successfully!
--------------------------------------------------------
🛠 Example 2: Complex Object Binding
👨‍💻 Model with Nested Object
public class Address
{
    public string City { get; set; }
    public string Country { get; set; }
}

public class UserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }  // Nested Object
}
----------------------------------------------
🚀 API Controller
[HttpPost("create-user")]
public IActionResult CreateUser([FromBody] UserModel user)
{
    return Ok($"User {user.Name} from {user.Address.City}, {user.Address.Country} created!");
}
-------------------------------------
📤 Client Request (JSON Body)
{
    "name": "Alice",
    "email": "alice@example.com",
    "address": {
        "city": "New York",
        "country": "USA"
    }
}
🎯 Response:
User Alice from New York, USA created!
---------------------------------------------------------
📌 Limitations of [FromBody]
❌ Multiple [FromBody] parameters allow nahi hote ek hi method me

public IActionResult CreateUser([FromBody] UserModel user, [FromBody] Address address) // ❌ ERROR
💡 Solution: Ek hi model me combine kar do ya DTO use karo.
❌ Only JSON/XML data accept hota hai, Query string ya Form-data nahi.
💡 Solution: Agar Form-data ya Query se data lena hai toh [FromQuery], [FromForm], ya [FromRoute] use karo.

🔹 Summary
✅ [FromBody] request body se JSON/XML data ko model me bind karne ke liye use hota hai.
✅ POST, PUT, PATCH requests ke liye best hai.
✅ Complex object aur nested properties bind kar sakta hai.
✅ Multiple [FromBody] parameters ek method me allow nahi hote.