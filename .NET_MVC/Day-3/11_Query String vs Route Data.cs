using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 11_Query String vs Route Data
    {
        
    }
}
--------------------------------------
Query String vs Route Data in ASP.NET Core 🚀
1️⃣ Query String Kya Hai?
🔹 Query String URL ka ek part hota hai jo ? ke baad start hota hai aur key-value pairs me data bhejne ke liye use hota hai.
🔹 Ye optional hota hai aur & se multiple parameters pass kiye ja sakte hain.

✅ Example: Query String
URL:
https://example.com/products?category=electronics&brand=apple
---------------------------------------
Controller Method:
public IActionResult GetProducts(string category, string brand)
{
    return Content($"Category: {category}, Brand: {brand}");
}
📌 Query String me values dynamically change ho sakti hain aur optional hoti hain.

2️⃣ Route Data Kya Hai?
🔹 Route Data URL ke ek path segment me embedded hota hai.
🔹 Ye fixed structure follow karta hai jo route templates se define hota hai.
🔹 Query string ke mukable, ye SEO-friendly aur readable URLs banata hai.

✅ Example: Route Data
-----------------------------------------
URL:
https://example.com/products/electronics/apple
----------------------------------------------------
Controller Method:
[HttpGet("products/{category}/{brand}")]
public IActionResult GetProducts(string category, string brand)
{
    return Content($"Category: {category}, Brand: {brand}");
}
📌 Route Parameters URL ka ek part hote hain aur fixed path structure follow karte hain.

---------------------------------------------
4️⃣ Real-Life Example: Query String vs Route Data
Maan lo ek E-commerce website hai jisme do endpoints hain:
-------------------------------------------------------
✅ 1. Query String (Filtering Products)
URL:
https://example.com/products?category=mobile&priceRange=10000-20000
-----------------------------------------------------
Code:

public IActionResult GetProducts(string category, string priceRange)
{
    return Content($"Category: {category}, Price Range: {priceRange}");
}
📌 Query String optional hai, isliye bina parameters ke bhi request bhej sakte hain.
------------------------------------------------
✅ 2. Route Data (Product Details)
URL:
https://example.com/product/1234
------------------------------------
Code:

[HttpGet("product/{id}")]
public IActionResult GetProduct(int id)
{
    return Content($"Product ID: {id}");
}
📌 Route Data mostly required hota hai, kyunki id ke bina product fetch nahi ho sakta.
----------------------------------------------------
5️⃣ Kab Kya Use Karein?
✔ Query String:

Jab data optional ho, jaise filters, sorting, pagination.

Jab same route multiple filters handle kare.

Example: ?sort=price&order=asc&category=electronics
---------------------
✔ Route Data:

Jab data required ho, jaise product ID, user ID.

Jab SEO-friendly clean URLs chahiyein.

Example: /product/1234 instead of ?id=1234
---------------------------------------------------
6️⃣ Conclusion
✅ Query String flexible hai but SEO-friendly nahi.
✅ Route Data clean hai but fixed structure follow karta hai.
✅ Dono use cases ke hisaab se best hain, isliye APIs me dono ka combination use hota hai! 🚀