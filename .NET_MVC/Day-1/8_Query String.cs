using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 8_Query String
    {
        
    }
}
------------------------------------------------
Query String – Kya, Kyu, Kaise?
Jab bhi browser se server ko request bheji jati hai, toh URL ke andar ek extra information bhejne ka tareeka hota hai Query String. Ye key-value pairs ke form me hoti hai jo URL ke "?" ke baad likhi jati hai.

Agar HTTP request ek letter hai, toh query string us letter ka extra note hai, jo batata hai ki kisko bheja ja raha hai aur kya specific data chahiye.
----------------------------------------------------
1. Query String Kaam Kyu Aati Hai?
Filtering aur Sorting → Search results ya tables ko filter karne ke liye.

Pagination → Multiple pages ko manage karne ke liye.

Tracking → Users ke clicks ya campaigns track karne ke liye.

State Management → Web apps me temporary data store karne ke liye.
-------------------------------------------
2. Query String Ka Format
Query string hamesha URL ke andar "?" ke baad likhi jati hai. Multiple values "&" se separate hoti hain.

https://example.com/products?category=mobile&price=10000
🔹 Breakdown:

category=mobile → Category ko mobile set kar raha hai.

price=10000 → Price filter apply kar raha hai.
-----------------------------------------------------
3. Query String Kaise Kaam Karti Hai?
Agar aap Flipkart ya Amazon pe Mobile search karte ho, toh URL kuch aise dikhta hai:

https://www.amazon.in/s?k=iphone&sort=price-asc-rank
k=iphone → Search term iPhone hai.

sort=price-asc-rank → Price low to high sort ho raha hai.

Ye parameters backend API ko bheje jate hain, jisme filter aur sorting apply hoti hai.
------------------------------------------------
4. Query String Example in .NET Core
Agar aap ek ASP.NET Core Web API bana rahe ho jo products filter kare, toh controller kuch aise hoga:

[HttpGet("products")]
public IActionResult GetProducts(string category, int price)
{
    return Ok($"Category: {category}, Price: {price}");
}
--------------------------------------
Agar aap isko call karte ho:

https://localhost:5001/products?category=laptop&price=50000
🔹 Output:
Category: laptop, Price: 50000
-----------------------------------------------
5. Query String vs Route Parameters
Feature	Query String	Route Parameters
Position	URL ke "?" ke baad	URL ke andar hota hai
Use Case	Filters, Sorting, Pagination	Unique IDs, Resources
Example	/products?category=mobile	/products/123
---------------------------------------
6. Security Issues (Query String Ko Kaise Secure Karein?)
1️⃣ Sensitive Data Mat Bhejo
🚫 Galat:

https://bank.com/transfer?account=123456&amount=5000
✅ Sahi:
👉 POST request ya Authorization header use karo.
------------------------------------------
2️⃣ Query String Encode Karo

var encodedUrl = HttpUtility.UrlEncode("C# Programming");
---------------------------------
3️⃣ SQL Injection Se Bacho

var query = "SELECT * FROM Users WHERE Name = @name";
----------------------------------------------
7. Conclusion
1️⃣ Query string dynamic content fetch karne ke liye use hoti hai.
2️⃣ E-commerce, search engines, aur filtering me ye common hai.
3️⃣ Security ka dhyan rakhna zaroori hai, sensitive data mat bhejo.