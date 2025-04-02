using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 5_JsonResult 
    {
        
    }
}
-----------------------------------------
JsonResult in ASP.NET Core 🚀
1️⃣ JsonResult Kya Hai?
🔹 JsonResult ek built-in action result type hai jo JSON format me response return karne ke liye use hota hai.
🔹 Yeh IActionResult se inherit hota hai aur JSON data serialization ka support karta hai.
🔹 Yeh APIs me tab use hota hai jab hume structured JSON response return karna ho.

2️⃣ JsonResult Ka Basic Syntax

public JsonResult GetData()
{
    return new JsonResult(new { Name = "John", Age = 30 });
}
📌 Request: GET /api/user
--------------------------------------------
📌 Response (JSON format):

{
  "Name": "John",
  "Age": 30
}
3️⃣ JsonResult Kab Use Karein?
🔹 Jab API JSON data return kare bina manual serialization ke.
🔹 Jab dynamic ya anonymous objects return karne ho.
🔹 Jab custom JSON settings apply karni ho (e.g., camelCase conversion, ignoring null values).
-------------------------------------------------------
4️⃣ JsonResult Ke Example
✅ 1. Basic JSON Response
[HttpGet]
public JsonResult GetUser()
{
    var user = new { Id = 1, Name = "Alice", Role = "Admin" };
    return new JsonResult(user);
}
📌 Response:
{
  "Id": 1,
  "Name": "Alice",
  "Role": "Admin"
}
--------------------------------------
✅ 2. JSON Response from Object
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

[HttpGet]
public JsonResult GetProduct()
{
    Product product = new Product { Id = 101, Name = "Laptop", Price = 799.99M };
    return new JsonResult(product);
}
------------------------------
📌 Response:
{
  "Id": 101,
  "Name": "Laptop",
  "Price": 799.99
}
----------------------------------------------------
✅ 3. JSON Settings Customize Karna
🔹 Agar hume JSON formatting customize karni ho to JsonSerializerOptions ka use kar sakte hain.

[HttpGet]
public JsonResult GetFormattedJson()
{
    var data = new { Message = "Hello, JSON!", Status = true };

    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // camelCase properties
        WriteIndented = true // Pretty-print JSON output
    };

    return new JsonResult(data, options);
}
------------------------------------
📌 Response:
{
  "message": "Hello, JSON!",
  "status": true
}
------------------------------------------
-------------------------------------------------------
6️⃣ Real-World Scenario Example
🔹 E-commerce API jo product details return kare:

[HttpGet("{id}")]
public JsonResult GetProductById(int id)
{
    var product = new { Id = id, Name = "Smartphone", Price = 699.99 };
    return new JsonResult(product);
}
📌 Request: GET /api/products/5
-----------------------------
📌 Response:
{
  "Id": 5,
  "Name": "Smartphone",
  "Price": 699.99
}
---------------------------------
7️⃣ Conclusion
✔ JsonResult structured JSON response return karne ke liye best hai.
✔ Ye automatic serialization handle karta hai, lekin Ok() zyada preferred hai flexibility ke liye.
✔ JSON formatting customizable hoti hai JsonSerializerOptions se.
✔ APIs aur web services me ye bahut useful hota hai.