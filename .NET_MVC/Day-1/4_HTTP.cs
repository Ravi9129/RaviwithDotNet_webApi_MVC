using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 4_HTTP
    {
        
    }
}
----------------------------------------------------------
HTTP Kya Hai?
HTTP (Hypertext Transfer Protocol) ek communication protocol hai jo client (browser ya app) aur server ke beech data transfer ke liye use hota hai. Jab bhi aap website open karte ho ya API request bhejte ho, toh HTTP ka use hota hai.

Real-World Example:
Maan lo aap ek E-commerce website use kar rahe ho. Jab aap ek product dekhne ke liye "View Details" button dabate ho, toh ek HTTP request jaati hai server pe, aur response me product ka data milta hai.

HTTP Request & Response Kaise Kaam Karta Hai?
1️⃣ Client (Browser ya App) request bhejta hai
2️⃣ Server request ko process karta hai
3️⃣ Server response bhejta hai
4️⃣ Client received data ko show karta hai
 
Example:
Jab aap Google pe kuch search karte ho, toh aapka browser Google ke server ko request bhejta hai. Google ka server aapko search results response me bhejta hai, jo aapke browser me show hota hai.

HTTP Methods
HTTP me multiple methods hote hain jo bataate hain ki request kis type ki hai.
--------------------------------------------------
1. GET Method
Jab aap kisi page ka data dekhte ho ya koi record fetch karte ho, tab GET method use hoti hai.

Example:
Maan lo aap ek Blog Website par ho aur aapko ek article dekhna hai.

Request:

GET /articles/10 HTTP/1.1
Host: example.com
Response:

{
    "id": 10,
    "title": "ASP.NET Core Kya Hai?",
    "content": "ASP.NET Core ek open-source framework hai..."
}
➡ Kab use karein?
Jab sirf data dekhna ho, bina changes kare (jaise kisi product ka detail page).
-------------------------------------------------------
2. POST Method
Jab aap koi naya data create karte ho, tab POST method use hoti hai.

Example:
Maan lo aap ek Sign-Up form bhar rahe ho. Jab aap "Submit" button dabate ho, toh ek POST request jaati hai.

Request:

POST /users/register HTTP/1.1
Host: example.com
Content-Type: application/json

{
    "name": "Amit",
    "email": "amit@example.com",
    "password": "123456"
}
Response:

{
    "message": "User registered successfully",
    "userId": 101
}
➡ Kab use karein?
Jab naya record database me add karna ho (jaise user registration ya naya blog post create karna).
-------------------------------------------------------------
3. PUT Method
Jab aap kisi existing record ko update karte ho, tab PUT method use hoti hai.

Example:
Agar aap apni profile update kar rahe ho, toh ek PUT request bheji jaayegi.

Request:

PUT /users/101 HTTP/1.1
Host: example.com
Content-Type: application/json

{
    "name": "Amit Kumar",
    "email": "amit.k@example.com"
}
Response:

{
    "message": "Profile updated successfully"
}
➡ Kab use karein?
Jab poora record update karna ho (jaise profile update karna ya kisi product ka naam badalna).
------------------------------------------------------------------------
4. DELETE Method
Jab aap kisi record ko delete karte ho, tab DELETE method use hoti hai.

Example:
Agar aap apna account delete karna chahte ho, toh ek DELETE request bheji jaayegi.

Request:

DELETE /users/101 HTTP/1.1
Host: example.com
-------------------------
Response:

{
    "message": "User deleted successfully"
}
➡ Kab use karein?
Jab koi record database se hataana ho (jaise kisi user ka account delete karna).
------------------------------------------------------------
HTTP Status Codes
Jab server koi request receive karta hai, toh ek response code bhejta hai jo bataata hai ki request successful thi ya nahi.

1. 200 - OK
✅ Jab request sahi se complete ho jaaye, tab ye status aata hai.
Example: Jab aap kisi page ka data dekhte ho.

2. 201 - Created
✅ Jab koi naya record successfully create ho jaaye, tab ye status aata hai.
Example: Jab ek naya user register hota hai.

3. 400 - Bad Request
❌ Jab kuch galat data bheja jaaye, tab ye error aata hai.
Example: Agar aap registration form me invalid email likhein.

4. 401 - Unauthorized
❌ Jab user ko access nahi hai, tab ye error aata hai.
Example: Agar aap bina login kiye admin panel access karne ka try karein.

5. 404 - Not Found
❌ Jab requested resource nahi mile, tab ye error aata hai.
Example: Agar aap ek wrong URL open karein.

6. 500 - Internal Server Error
❌ Jab server me koi technical issue ho, tab ye error aata hai.
Example: Agar database crash ho jaye ya code me koi bug ho.
---------------------------------------------
Conclusion
1️⃣ HTTP ek protocol hai jo client aur server ke beech data transfer karta hai.
2️⃣ GET, POST, PUT, DELETE request types hoti hain jo different tasks ke liye use hoti hain.
3️⃣ Har request ka ek response code hota hai jo request ka status batata hai.
4️⃣ Agar aap web development ya API development kar rahe hain toh HTTP requests aur status codes samajhna important hai.