using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 10_HTTP GET vs POST
    {
        
    }
}
------------------------------------------
HTTP GET vs POST – Kya, Kyu, Aur Kab?
Jab client kisi server se baat karta hai, toh data bhejne ya retrieve karne ke liye HTTP methods use ki jati hain. GET aur POST sabse common hain, lekin dono ka kaam alag hai.

Agar GET ek khidki hai jisme se andar dekh sakte ho, toh POST ek darwaza hai jisme se naye cheezein andar bhej sakte ho.
----------------------------------------------
1. GET Request – Kya Hai Aur Kaise Kaam Karti Hai?
🔹 GET method ka use tab hota hai jab hume sirf data fetch karna ho, bina kisi modification ke.
🔹 URL me query string ke through data bhejti hai (?key=value).

Example:
https://example.com/products?category=mobile&price=10000
🔹 Yahan category=mobile aur price=10000 URL ke andar visible hain.

Code Example – GET in .NET Core
[HttpGet("products")]
public IActionResult GetProducts(string category, int price)
{
    return Ok($"Category: {category}, Price: {price}");
}
----------------------------------------
📌 Request:

GET /products?category=laptop&price=50000
-----------------------------------------------
📌 Response:

Category: laptop, Price: 50000
✔ Kab Use Karein?

Jab data ko sirf retrieve karna ho (e.g., Search, Filter, Paginate).

Jab data sensitive na ho (kyunki URL me visible hoga).

❌ Kab Nahi Use Karein?

Jab data modify ya save karna ho.

Jab sensitive information (e.g., passwords) bhejni ho.
---------------------------------------------------------------
2. POST Request – Kya Hai Aur Kaise Kaam Karti Hai?
🔹 POST method ka use tab hota hai jab hume naye data ko server par save ya modify karna ho.
🔹 Data request body me send hota hai, URL me nahi dikhta.

Example:
POST /products
Content-Type: application/json

{
    "name": "iPhone 14",
    "price": 90000
}
----------------------------------
Code Example – POST in .NET Core

[HttpPost("products")]
public IActionResult AddProduct([FromBody] Product product)
{
    return Ok($"Product {product.Name} added with price {product.Price}");
}
------------------------------
📌 Request:

POST /products
{
    "name": "iPhone 14",
    "price": 90000
}
--------------------------------------
📌 Response:

Product iPhone 14 added with price 90000
✔ Kab Use Karein?

Jab naya data create ya modify karna ho.

Jab sensitive data bhejna ho (kyunki URL me nahi dikhta).

❌ Kab Nahi Use Karein?

Jab sirf data retrieve karna ho.

Jab data caching ki zaroorat ho (GET request cache hoti hai, POST nahi).

----------------------------------------------------
4. Real-World Example – Login System
GET (Login Page Load)
GET /login
Sirf login page dikhane ke liye.

Data modify nahi hota.
-------------------------------------------
POST (Login Request)

POST /login
{
    "username": "john123",
    "password": "securepassword"
}
Sensitive data bhejne ke liye POST use hota hai.

Agar GET hota toh password URL me dikh jata (🚫 Bad Practice).
----------------------------------------------------------
5. Conclusion
1️⃣ GET read-only operations ke liye hota hai, aur POST data bhejne ke liye.
2️⃣ GET request cache hoti hai, POST nahi hoti.
3️⃣ Sensitive data bhejne ke liye POST use karein, GET nahi.
4️⃣ Pagination, filtering aur searching ke liye GET sahi hai.
5️⃣ Data create/update karne ke liye POST use karein.