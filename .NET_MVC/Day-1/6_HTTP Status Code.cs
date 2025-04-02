using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 6_HTTP Status Code
    {
        
    }
}
----------------------------------------------------
HTTP Status Codes – Kya, Kyu, Kaise?
Jab client (browser ya app) ek request bhejta hai, toh server ek response ke saath status code return karta hai. Ye status code bataata hai ki request successful thi ya nahi, aur agar nahi thi toh problem kya thi.

Real-Life Example
Aap Amazon pe ek product search karte ho.

1️⃣ Agar product mil gaya → Server 200 OK return karega.
2️⃣ Agar galat URL dala → Server 404 Not Found return karega.
3️⃣ Agar server crash ho gaya → Server 500 Internal Server Error return karega.

1. HTTP Status Codes Categories
1️⃣ 1xx - Informational → Request process ho rahi hai (kam use hote hain).
2️⃣ 2xx - Success → Request successful thi.
3️⃣ 3xx - Redirection → Client ko dusre URL pe bhejna hai.
4️⃣ 4xx - Client Errors → Request galat thi ya user ka access nahi hai.
5️⃣ 5xx - Server Errors → Server me kuch gadbad ho gayi hai.

2. 1xx (Informational Responses)
⚡ 100 Continue → Server bata raha hai ki request receive ho gayi hai, aage badh sakte ho.
⚡ 101 Switching Protocols → Client aur server alag protocol pe shift ho rahe hain.

👉 Kab Use Hota Hai?
Agar aap large file upload kar rahe ho, toh 100 Continue bataata hai ki upload start ho chuka hai.

3. 2xx (Success Responses)
✅ 200 OK → Sab kuch theek thaak hai, response mil gaya.
✅ 201 Created → Naya resource create ho gaya.
✅ 204 No Content → Request successful thi, par koi data return nahi ho raha.

👉 Example:
Agar aap Facebook pe ek naya post likhte ho, toh server 201 Created return karega.

4. 3xx (Redirection Responses)
🔄 301 Moved Permanently → Page permanently dusre URL pe chala gaya hai.
🔄 302 Found (Temporary Redirect) → Page temporarily move kiya gaya hai.
🔄 304 Not Modified → Data change nahi hua, toh browser cached version use karega.

👉 Example:
Agar aap http://example.com pe jaate ho aur vo https://example.com pe shift ho jata hai, toh 301 Moved Permanently status milega.

5. 4xx (Client Errors)
❌ 400 Bad Request → Request galat hai ya missing data hai.
❌ 401 Unauthorized → Authentication chahiye.
❌ 403 Forbidden → Access allowed nahi hai.
❌ 404 Not Found → Page ya resource exist nahi karta.
❌ 405 Method Not Allowed → Jo method request me hai (GET, POST) vo allowed nahi hai.

👉 Example:
Agar aap Google pe galat page dhoondhte ho (/abcxyz), toh 404 Not Found milega.

6. 5xx (Server Errors)
💥 500 Internal Server Error → Server me koi unknown problem ho gayi.
💥 502 Bad Gateway → Ek server dusre server se sahi response nahi le paya.
💥 503 Service Unavailable → Server busy hai ya maintenance me hai.

👉 Example:
Agar Amazon ka server down ho jaye, toh 503 Service Unavailable milega.
--------------------------------------------------------------
7. Real-World Scenarios
🟢 Scenario 1: Login Success (200 OK)
Agar aap correct username-password dalte ho:

HTTP/1.1 200 OK
Content-Type: application/json

{
    "message": "Login successful",
    "user": {
        "id": 101,
        "name": "Rahul"
    }
}
----------------------------------------------------
🔴 Scenario 2: Wrong Password (401 Unauthorized)
Agar password galat dalte ho:

HTTP/1.1 401 Unauthorized
Content-Type: application/json

{
    "error": "Invalid username or password"
}
---------------------------------------------------------------
🔵 Scenario 3: Page Not Found (404 Not Found)
Agar aap ek galat URL open karte ho:

HTTP/1.1 404 Not Found
Content-Type: application/json

{
    "error": "Page not found"
}
⚠ Scenario 4: Server Crash (500 Internal Server Error)
Agar server pe koi bug hai:

HTTP/1.1 500 Internal Server Error
Content-Type: application/json

{
    "error": "Something went wrong on the server"
}
----------------------------------------------------
8. Conclusion
1️⃣ HTTP Status Codes server ke response ko samajhne ke liye zaroori hote hain.
2️⃣ 200-series codes success show karte hain, 400-series client errors show karte hain, aur 500-series server issues dikhate hain.
3️⃣ Web development ya API development me HTTP status codes ka samajhna bahut zaroori hai.