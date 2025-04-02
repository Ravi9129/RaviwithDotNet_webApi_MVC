using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 11_HTTP Request
    {
        
    }
}
-----------------------------------------
HTTP Request – Kya, Kyu, Aur Kaise?
Agar aap Google pe kuch search karte ho ya Facebook login karte ho, toh aapka browser ya client ek HTTP request bhejta hai.
Ye request server ko batati hai ki kaunsa data chahiye ya kya update karna hai.

1. HTTP Request Kyu Zaroori Hai?
✔ Client aur Server ke beech communication ka tareeka hai.
✔ Web pages load karne, API calls bhejne aur data fetch karne ke liye.
✔ Form submit karne, authentication aur CRUD operations me use hoti hai.

2. HTTP Request Ka Structure
Ek HTTP request me 5 cheezein hoti hain:
1️⃣ Request Line → Method + URL + HTTP Version
2️⃣ Headers → Extra info jaise User-Agent, Authentication
3️⃣ Body (optional) → Form data ya JSON payload (POST, PUT requests ke liye)
4️⃣ Query String Parameters → URL me extra data send karne ke liye
5️⃣ Cookies → User session maintain karne ke liye
----------------------------------------------------------
3. HTTP Request Example
Jab aap Facebook pe login karte ho, toh ek POST request bheji jati hai:
POST /login HTTP/1.1
Host: www.facebook.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)
Content-Type: application/json
Content-Length: 54

{
    "username": "john_doe",
    "password": "secure123"
}
📌 Breakdown:
✔ POST /login HTTP/1.1 → Login karne ke liye POST method use ho raha hai.
✔ Host: www.facebook.com → Request Facebook ke server par ja rahi hai.
✔ User-Agent: Mozilla/5.0... → Ye request ek Chrome browser se aayi hai.
✔ Content-Type: application/json → Body me JSON format ka data hai.
✔ Body → Username aur password bheja gaya hai.
----------------------------------------------------------------------------
4. HTTP Methods – Kaun Kya Kaam Karta Hai?
1️⃣ GET (Data Fetch Karne Ke Liye)
Sirf data retrieve karta hai, kuch modify nahi karta.

Example:
GET /products?category=mobile HTTP/1.1
✔ Kab use karein? → Jab sirf read karna ho (e.g., search, listing).
------------------------------------------------
2️⃣ POST (Naya Data Create Karne Ke Liye)
Server par naya data bhejne ke liye.

Example:
POST /users HTTP/1.1
Content-Type: application/json

{
    "name": "Rahul",
    "email": "rahul@example.com"
}
✔ Kab use karein? → Jab form submit karein ya data store karein.
----------------------------------------------------
3️⃣ PUT (Data Update Karne Ke Liye)
Pura object update karta hai.

Example:
PUT /users/123 HTTP/1.1
Content-Type: application/json

{
    "name": "Rahul Sharma",
    "email": "rahul@example.com"
}
✔ Kab use karein? → Jab poora data update karna ho.
------------------------------------------------------
4️⃣ PATCH (Partial Update Ke Liye)
Sirf ek field update karega.

Example:

PATCH /users/123 HTTP/1.1
Content-Type: application/json

{
    "name": "Rahul Sharma"
}
✔ Kab use karein? → Jab sirf ek field update karna ho.
------------------------------------------
5️⃣ DELETE (Data Delete Karne Ke Liye)
Server se data remove karne ke liye.

Example:
DELETE /users/123 HTTP/1.1
✔ Kab use karein? → Jab koi record delete karna ho.
------------------------------------------------------
5. HTTP Request .NET Core Me Kaise Handle Karein?
Agar aap ek ASP.NET Core API bana rahe ho jo HTTP Requests handle kare, toh aapke controller kuch aise honge:

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    // GET Request
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(new { message = "Users List Fetched" });
    }

    // POST Request
    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        return Ok(new { message = $"User {user.Name} created" });
    }

    // PUT Request
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        return Ok(new { message = $"User {id} updated" });
    }

    // DELETE Request
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        return Ok(new { message = $"User {id} deleted" });
    }
}
--------------------------------------
6. Real-World Example – E-commerce Website
Agar Amazon pe ek product search karein aur fir cart me add karein, toh kaise kaam hoga?

🔹 Search Request (GET)
GET /products?search=iphone HTTP/1.1
📌 Server response:

{
    "id": 101,
    "name": "iPhone 14",
    "price": 80000
}
✔ Yahan sirf data retrieve ho raha hai (safe operation).

🔹 Cart Me Product Add Karna (POST)
POST /cart HTTP/1.1
Content-Type: application/json

{
    "productId": 101,
    "quantity": 1
}
✔ Yahan naya data server me save ho raha hai.

🔹 Order Place Karna (POST)
POST /order HTTP/1.1
Content-Type: application/json

{
    "cartId": 234,
    "paymentMethod": "Credit Card"
}
✔ Yahan order create ho raha hai.

7. Conclusion
1️⃣ HTTP Request client-server communication ka base hai.
2️⃣ GET data fetch karta hai, POST naye records create karta hai.
3️⃣ PUT aur PATCH updates ke liye use hote hain.
4️⃣ DELETE server se data remove karta hai.
5️⃣ ASP.NET Core me HTTP requests ko controllers handle karte hain.