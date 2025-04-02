using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 12_[FromQuery] vs [FromRoute]
    {
        
    }
}
----------------------------------------
[FromQuery] vs [FromRoute] in ASP.NET Core 🚀
1️⃣ [FromQuery] Kya Hai?
🔹 [FromQuery] attribute request ke query string se values extract karta hai.
🔹 Ye mostly optional parameters ke liye use hota hai, jaise filtering, sorting, pagination, etc.

✅ Example: [FromQuery]
URL:
https://example.com/products?category=electronics&brand=apple
----------------------------------------
Controller Method:
[HttpGet("products")]
public IActionResult GetProducts([FromQuery] string category, [FromQuery] string brand)
{
    return Content($"Category: {category}, Brand: {brand}");
}
📌 Query string optional hoti hai, isliye bina parameters ke bhi request bhej sakte hain.

2️⃣ [FromRoute] Kya Hai?
🔹 [FromRoute] attribute request ke URL path (route parameters) se values extract karta hai.
🔹 Ye mostly required parameters ke liye use hota hai, jaise product ID, user ID, etc.

✅ Example: [FromRoute]
URL:
https://example.com/product/1234
---------------------------------------------------
Controller Method:
[HttpGet("product/{id}")]
public IActionResult GetProduct([FromRoute] int id)
{
    return Content($"Product ID: {id}");
}
📌 Route parameters mostly required hote hain, bina id ke request fail ho sakti hai.
----------------------------------------------------
4️⃣ Real-Life Example: [FromQuery] vs [FromRoute]
✅ 1. [FromQuery] for Filtering Products
URL:
https://example.com/products?category=mobile&priceRange=10000-20000
------------------------------------------------
Code:
[HttpGet("products")]
public IActionResult GetProducts([FromQuery] string category, [FromQuery] string priceRange)
{
    return Content($"Category: {category}, Price Range: {priceRange}");
}
📌 Query string optional hai, isliye bina parameters ke bhi request bhej sakte hain.
------------------------------------------------------------------
✅ 2. [FromRoute] for Product Details
URL:
https://example.com/product/1234
-----------------------------------------------
Code:
[HttpGet("product/{id}")]
public IActionResult GetProduct([FromRoute] int id)
{
    return Content($"Product ID: {id}");
}
📌 Route Data mostly required hota hai, kyunki id ke bina product fetch nahi ho sakta.
-------------------------------------------------------
5️⃣ [FromQuery] vs [FromRoute] Kab Use Karein?
✔ [FromQuery] Use Karein Jab:

Optional parameters ho, jaise filters, sorting, pagination.

Multiple parameters ho jo dynamically change ho sakein.

Example: /products?sort=price&order=asc&category=electronics
--------------------------------------
✔ [FromRoute] Use Karein Jab:

Required parameters ho, jaise product ID, user ID.

SEO-friendly clean URLs chahiyein.

Example: /product/1234 instead of ?id=1234
------------------------------------------
6️⃣ Conclusion
✅ [FromQuery] flexible hai but optional.
✅ [FromRoute] clean hai but mostly required.
✅ Dono use cases ke hisaab se best hain, isliye APIs me dono ka combination use hota hai! 🚀