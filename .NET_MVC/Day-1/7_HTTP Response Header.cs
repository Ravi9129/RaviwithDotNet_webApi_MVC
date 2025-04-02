using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 7_HTTP Response Header
    {
        
    }
}
-----------------------------------------------
HTTP Response Headers – Kya, Kyu, Kaise?
Jab bhi server client ko response bhejta hai, toh response headers ke through extra information deta hai. Ye headers metadata hote hain jo response ke sath aate hain.

Agar HTTP response ek parcel hai, toh response headers us parcel ka label hai, jisme likha hota hai ki ye kisne bheja, kya content hai, kitne time tak valid hai, aur browser isko kaise handle kare.

1. HTTP Response Headers Kaam Kyu Aate Hain?
Security → Headers bataate hain ki browser kis tarah response ko handle kare.

Caching → Response ko cache karna chahiye ya nahi?

Content-Type → Response ka data kis format me hai?

Redirection → Page kis nayi location pe shift ho gaya hai?
---------------------------------------------------------
2. HTTP Response Headers Ke Types
🛠 General Headers → Request aur response dono me kaam aate hain.
📄 Response Headers → Server response ka metadata dete hain.
🔒 Security Headers → Web security improve karne ke liye hote hain.
⚡ Caching Headers → Data ko cache karne ka control dete hain.
-------------------------------------------------------------------------
3. Common HTTP Response Headers
✅ 1. Content-Type (Response ka Format)
👉 Ye header bataata hai ki response ka data kis type ka hai.
Agar JSON data bhejna hai:


Content-Type: application/json
Agar HTML page bhejna hai:
---------------------------------------
Content-Type: text/html
🔹 Example:
Agar Amazon ka API JSON response bhejta hai, toh Content-Type: application/json hoga.
---------------------------------------------------------
✅ 2. Content-Length (Kitne Bytes ka Data hai)
Ye bataata hai ki response body kitni badi hai.

Content-Length: 2487
👉 Example:
Agar aap Netflix pe ek movie load kar rahe ho, toh browser Content-Length dekhkar pata karega ki kitna data aayega.
---------------------------------------------------------------------
✅ 3. Location (Redirects ke Liye)
Agar ek page move ho gaya hai, toh Location header bataata hai ki naya URL kya hai.

HTTP/1.1 301 Moved Permanently
Location: https://new-website.com
👉 Example:
Agar aap http://example.com pe jaate ho aur vo https://example.com pe shift ho jata hai, toh Location header redirect karega.
-------------------------------------------------------------------
✅ 4. Set-Cookie (Cookies Store Karne Ke Liye)
Agar server client ke browser me cookie save karna chahta hai, toh Set-Cookie header bhejega.

Set-Cookie: session_id=xyz123; HttpOnly; Secure
🔹 Example:
Jab aap Flipkart ya Amazon login karte ho, toh session maintain karne ke liye Set-Cookie use hota hai.
----------------------------------------------------------
✅ 5. Cache-Control (Response Ko Cache Karna Ya Nahi)
Ye bataata hai ki browser ya proxy server response ko cache kar sakta hai ya nahi.

Cache-Control: no-cache, no-store, must-revalidate
🔹 Example:
Agar aap bank ki website use kar rahe ho, toh Cache-Control: no-store ensure karega ki aapka sensitive data store na ho.
-------------------------------------
✅ 6. ETag (Caching Optimize Karne Ke Liye)
ETag ek unique identifier hai jo bataata hai ki kya response change hua hai ya nahi.

ETag: "5d8c72a5-3e4b"
🔹 Example:
Agar aap Facebook reload karte ho, toh ETag check hota hai ki kya naye updates aaye hain ya nahi.
-----------------------------------------------------------
✅ 7. Server (Server Software Ka Naam)
Ye header bataata hai ki server kaunsa software use kar raha hai.

Server: Apache/2.4.41 (Ubuntu)
🔹 Example:
Agar aap Google.com ka response check karein, toh Server: gws (Google Web Server) dikhega.
----------------------------------------------------
4. Security HTTP Headers
🔒 1. X-Content-Type-Options
Ye browser ko bataata hai ki Content-Type guess na kare (security improve karta hai).

X-Content-Type-Options: nosniff
🔹 Example:
Agar aap banking website ya payment gateway bana rahe ho, toh X-Content-Type-Options: nosniff security ke liye zaroori hai.
-----------------------------------------------
🔒 2. X-Frame-Options
Agar aap clickjacking attack se bachna chahte ho, toh X-Frame-Options use hota hai.

X-Frame-Options: DENY
🔹 Example:
Agar aap PayPal ya Google login page banate ho, toh X-Frame-Options: DENY ensure karega ki koi doosri site aapke page ko iframe me embed na kare.
----------------------------------------------------------------
🔒 3. Content-Security-Policy (CSP)
CSP XSS attacks se bachne ke liye use hota hai.

Content-Security-Policy: default-src 'self'
🔹 Example:
Agar aap e-commerce website pe user input allow kar rahe ho, toh Content-Security-Policy ensure karega ki malicious scripts execute na ho.
-----------------------------------------------------------------
5. Real-World Example
Agar aap ek successful response JSON format me bhej rahe ho, toh headers kuch aise honge:

HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 248
Cache-Control: no-cache
ETag: "xyz123"
Server: nginx
Set-Cookie: session_id=abcd123; HttpOnly; Secure
Aur agar server error (500) ho toh:

HTTP/1.1 500 Internal Server Error
Content-Type: text/plain
Content-Length: 56
Server: Apache
-------------------------------------------
6. Conclusion
1️⃣ HTTP Response Headers client-server communication ka ek important part hain.
2️⃣ Security headers attacks se bachne ke liye use hote hain.
3️⃣ Caching headers performance optimize karne ke liye use hote hain.
4️⃣ Agar API develop kar rahe ho, toh headers ka sahi use bohot zaroori hai.