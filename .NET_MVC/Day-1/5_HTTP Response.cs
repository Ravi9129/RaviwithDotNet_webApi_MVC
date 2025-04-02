using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 5_HTTP Response
    {
        
    }
}
-----------------------------------------------
HTTP Response Kya Hota Hai?
Jab client (browser ya app) server se request bhejta hai, toh server ek response return karta hai. Is response me data aur status code hota hai, jo bataata hai ki request successful thi ya nahi.

Real-World Example:
Aap ek E-commerce website pe product ka detail page open karte ho.
-------------------------------------------------
1️⃣ Aapka browser ek GET request bhejta hai:

GET /products/101 HTTP/1.1
Host: example.com
------------------------------------------------------
2️⃣ Server ek response return karta hai:

HTTP/1.1 200 OK
Content-Type: application/json

{
    "id": 101,
    "name": "iPhone 15",
    "price": 80000,
    "description": "Apple ka latest smartphone"
}
Yahan pe response me 3 cheezein hain:

✔ Status Code → 200 OK (Request successful)
✔ Headers → Content-Type: application/json (Data JSON format me hai)
✔ Body → { "id": 101, "name": "iPhone 15", ... } (Actual product details)

HTTP Response Ka Structure
Ek HTTP response 3 parts me divide hota hai:

1️⃣ Status Line

Status Code (Jaise 200, 404, 500)

Status Message (Jaise OK, Not Found, Internal Server Error)

2️⃣ Headers

Extra information jo response ke saath aati hai (Jaise Content-Type, Cache-Control)

3️⃣ Body

Actual data jo client ko milta hai (HTML, JSON, XML, ya plain text)

HTTP Response Codes (Status Codes)
1️⃣ 2xx (Success Responses)
✅ 200 - OK → Jab request successfully complete ho jaye
✅ 201 - Created → Jab naya record successfully create ho jaye
✅ 204 - No Content → Jab request successful ho but koi data return na ho

👉 Example: Jab aap user login karte ho aur sab sahi hota hai, toh 200 OK aata hai.

2️⃣ 3xx (Redirection Responses)
🔄 301 - Moved Permanently → Jab ek page hamesha dusre URL pe move ho gaya ho
🔄 302 - Found (Temporary Redirect) → Jab ek page temporary dusre jagah shift ho

👉 Example: Agar aap http://example.com pe jaane ki koshish karte ho, aur ye HTTPS pe redirect ho jaye (https://example.com), toh 301 Moved Permanently response milega.

3️⃣ 4xx (Client Errors)
❌ 400 - Bad Request → Jab galat data bheja jaye
❌ 401 - Unauthorized → Jab authentication required ho
❌ 403 - Forbidden → Jab access na ho
❌ 404 - Not Found → Jab requested resource na mile

👉 Example: Agar aap galat URL likhte ho (/product/9999) jo exist nahi karta, toh 404 Not Found response aayega.

4️⃣ 5xx (Server Errors)
💥 500 - Internal Server Error → Jab server me koi problem ho
💥 502 - Bad Gateway → Jab server dusre server se sahi response na le paye
💥 503 - Service Unavailable → Jab server busy ho ya maintenance me ho

👉 Example: Agar database crash ho jaye, toh 500 Internal Server Error response aayega.

Example: HTTP Response in Real Applications
--------------------------------------------
1. Login Success (200 OK)
Agar aap correct username-password dalte ho, toh response aisa hoga:

HTTP/1.1 200 OK
Content-Type: application/json

{
    "message": "Login successful",
    "user": {
        "id": 101,
        "name": "Amit"
    }
}
------------------------------------------------
2. Login Failed (401 Unauthorized)
Agar galat password dalte ho, toh response aisa hoga:

HTTP/1.1 401 Unauthorized
Content-Type: application/json

{
    "error": "Invalid username or password"
}
--------------------------------------------------
3. Product Not Found (404 Not Found)
Agar aap ek galat product ID daal dete ho jo exist nahi karti, toh response aisa hoga:

HTTP/1.1 404 Not Found
Content-Type: application/json

{
    "error": "Product not found"
}
----------------------------------------------------
Conclusion
1️⃣ HTTP response client ko bataata hai ki request successful thi ya nahi.
2️⃣ Ek response me status code, headers, aur body hoti hai.
3️⃣ 2xx success ke liye, 4xx client errors ke liye, aur 5xx server errors ke liye hote hain.
4️⃣ Web development ya API development me HTTP responses ka samajhna important hai.