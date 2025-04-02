using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 10_Model Binding
    {
        
    }
}
-----------------------------------
Model Binding in ASP.NET Core 🚀
1️⃣ Model Binding Kya Hai?
🔹 Model Binding ek process hai jo HTTP Request (Query String, Form Data, Route Parameters, JSON Body) ka data le kar C# objects me map karta hai.
🔹 Iska use Controllers aur Razor Pages me hota hai jisse hum HTTP request se directly C# objects receive kar sakein.

2️⃣ Kab Use Karein?
✅ Jab form submit ho aur data automatically controller ke method me bind ho jaye.
✅ Jab API ke request body me JSON data aaye aur C# object me convert ho.
✅ Jab query parameters ya route parameters ko directly method parameters me bind karna ho.
-----------------------------------------------------
3️⃣ Model Binding Ke Sources
Model Binding multiple sources se data fetch kar sakta hai:

Source	Example	Description
Query String	?id=10	URL parameters se data bind karega
Route Values	/user/10	Route parameters se data bind karega
Form Data	<form>	Form submission se data lega
JSON Body	{ "name": "Amit" }	API ke request body se JSON data lega
Header Values	Request.Headers["Authorization"]	Request headers se data lega
--------------------------------------------
4️⃣ Example: Model Binding in Action
✅ 1. Simple Model Binding (Query String)
🔹 URL: https://example.com/user?id=5&name=Amit

public IActionResult GetUser(int id, string name)
{
    return Content($"User ID: {id}, Name: {name}");
}
📌 Response: "User ID: 5, Name: Amit"
📌 Query String Parameters automatically bind ho jayenge.
------------------------------------------------
✅ 2. Model Binding with Route Parameters
🔹 URL: https://example.com/user/5
-------------------
[HttpGet("user/{id}")]
public IActionResult GetUserById(int id)
{
    return Content($"User ID: {id}");
}
📌 Response: "User ID: 5"
📌 Route Parameter {id} automatically bind hoga.
--------------------------------------------------------
✅ 3. Model Binding with Complex Objects (Form Data)
🔹 Form submit hone ke baad complex object bind hoga.
------------
🔹 Model Class
public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
----------------------------------------
🔹 Controller Method
[HttpPost]
public IActionResult CreateUser(UserModel user)
{
    return Content($"User Created: {user.Name} (ID: {user.Id})");
}
📌 Agar Form submit hota hai, toh data UserModel me automatically bind hoga.
------------------------------------------------------
✅ 4. Model Binding with JSON Body (API Example)
🔹 Client request JSON body bhejta hai:

{
    "id": 10,
    "name": "Amit"
}
------------------------------------------
🔹 Controller Method
[HttpPost]
public IActionResult CreateUser([FromBody] UserModel user)
{
    return Ok($"User Created: {user.Name} (ID: {user.Id})");
}
📌 Agar JSON request body aati hai, toh [FromBody] attribute use karna padta hai.
-----------------------------------------------------------
✅ 5. Model Binding from Headers
public IActionResult GetAuthToken([FromHeader] string Authorization)
{
    return Content($"Received Token: {Authorization}");
}
📌 Client agar Authorization Header bhejega, toh wo automatically bind ho jayega.
----------------------------------------------------------------
5️⃣ Model Binding Customization (Attributes)
Agar specific source se data lena ho, toh ye attributes use kar sakte hain:

Attribute	Source	Example
[FromQuery]	Query String	?id=10&name=Amit
[FromRoute]	Route Parameter	/user/10
[FromForm]	Form Data	<form>
[FromBody]	JSON Body	{ "id": 10 }
[FromHeader]	HTTP Headers	Authorization: Bearer xyz
---------------------------------------------
6️⃣ Real-World Scenario Example
🔹 E-commerce App me Order Placement API:

public class OrderModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

[HttpPost("order")]
public IActionResult PlaceOrder([FromBody] OrderModel order)
{
    if (order.Quantity <= 0)
        return BadRequest("Invalid quantity");

    return Ok($"Order placed for Product ID: {order.ProductId}, Quantity: {order.Quantity}");
}
📌 Client JSON Body bhejega, aur ye API automatic data bind karegi.
--------------------------------------------
7️⃣ Conclusion
✔ Model Binding automatically HTTP Request ke data ko C# objects me map karta hai.
✔ Query Strings, Route Parameters, Form Data, JSON Body, Headers sabhi sources se data fetch kar sakta hai.
✔ Customization ke liye [FromQuery], [FromRoute], [FromBody] jaise attributes use kar sakte hain.
✔ Best practice: APIs me [FromBody], [FromQuery], [FromHeader] use karein.