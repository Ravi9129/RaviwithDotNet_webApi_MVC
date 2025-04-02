using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 9_HTTP Request Header
    {
        
    }
}
-----------------------------------
HTTP Request Header – Kya, Kyu, Kaise?
Jab bhi browser ya client kisi server ko request bhejta hai, toh uske saath extra information bhejna padta hai, jo HTTP Request Headers ke through bheji jati hai.

Ye headers batate hain:
✔ Request kis format me hai?
✔ Kaunsa authentication token hai?
✔ Client ka browser kaunsa hai?
✔ Kaunsa data type accept karega server?

Agar HTTP request ek parcel hai, toh headers uske upar likha hua address aur instructions hain.
--------------------------------------------------------
1. HTTP Request Headers Kyu Zaroori Hain?
Security → Authentication aur Authorization manage karne ke liye.

Content Negotiation → Kaunsa data format (JSON, XML) accept hai.

Caching → Data ko fast retrieve karne ke liye.

Tracking & Analytics → User behavior track karne ke liye.
---------------------------------------------------------------
2. HTTP Request Headers Kaise Kaam Karte Hain?
Jab aap Google pe kuch search karte ho, toh browser ek HTTP request bhejta hai jo kuch aise dikhti hai:

GET /search?q=iphone HTTP/1.1
Host: www.google.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)
Accept: text/html,application/xhtml+xml
🔹 Breakdown:

GET /search?q=iphone HTTP/1.1 → Request iPhone search ke liye hai.

Host: www.google.com → Ye request Google ke server ke liye hai.

User-Agent: Mozilla/5.0 ... → Ye request Chrome browser se aayi hai.

Accept: text/html, application/xhtml+xml → Client HTML pages accept kar sakta hai.
---------------------------------------------------------
3. Common HTTP Request Headers
1️⃣ Host Header
🔹 Server ka address specify karta hai.
Host: www.example.com
Kyu?
Server ko batane ke liye ki request kaunsa domain handle karega.
-----------------------------------------------------------------
2️⃣ User-Agent Header
🔹 Client ka browser aur OS batata hai.

User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)
Kyu?
Server ko batane ke liye ki kaunsa browser ya device request bhej raha hai.
-----------------------------------------------
3️⃣ Authorization Header
🔹 Authentication details bhejne ke liye use hota hai.

Authorization: Bearer <token>
Kyu?
Jab secure API call karni ho, toh server ko token validate karne ke liye diya jata hai.
---------------------------------------------
4️⃣ Accept Header
🔹 Client batata hai ki kaunsa data format accept karega.

Accept: application/json
Kyu?
Agar API request hai, toh server JSON ya XML format me response bhejega.
---------------------------------
5️⃣ Content-Type Header
🔹 Request body ka format specify karta hai.

Content-Type: application/json
Kyu?
Server ko batane ke liye ki data kis format me aayega.
------------------------------------------
6️⃣ Referer Header
🔹 Pehle kaunsa page visit kiya gaya tha, ye batata hai.

Referer: https://www.google.com
Kyu?
Website owners analytics ke liye use karte hain.
---------------------------------------------------
7️⃣ Cookie Header
🔹 User ka session maintain karne ke liye use hota hai.

Cookie: sessionId=abc123
Kyu?
Login hone ke baad user ka session track karne ke liye.
--------------------------------------------------------
4. HTTP Request Headers in .NET Core
Agar aap ek ASP.NET Core API bana rahe ho jo Authorization Header check kare, toh controller kuch aise hoga:

[HttpGet("secure-data")]
public IActionResult GetSecureData([FromHeader] string Authorization)
{
    if (Authorization == "Bearer my-secret-token")
    {
        return Ok("Access granted!");
    }
    return Unauthorized("Invalid Token");
}
Agar aap request bhejo:

GET /secure-data HTTP/1.1
------------------------------------------
Authorization: Bearer my-secret-token
🔹 Output:
Access granted!
5. Conclusion
1️⃣ HTTP Headers extra information provide karte hain.
2️⃣ Security, authentication, aur data negotiation ke liye zaroori hain.
3️⃣ Authorization, Accept, aur Content-Type headers APIs me common hain.