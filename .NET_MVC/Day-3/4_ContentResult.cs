using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 4_ContentResult
    {
        
    }
}
--------------------------------------------
ContentResult in ASP.NET Core 🚀
1️⃣ ContentResult Kya Hai?
🔹 ContentResult ek built-in result type hai jo plain text, HTML, ya JSON response return karne ke liye use hota hai.
🔹 Yeh IActionResult inherit karta hai aur HTTP response ke content-type ko specify karne ki facility deta hai.
🔹 ContentResult tab use hota hai jab hume raw string content return karna ho bina kisi serialization ke.
-------------------------------------------
2️⃣ ContentResult Ka Basic Syntax

public ContentResult GetMessage()
{
    return Content("Hello, this is a plain text response!");
}
📌 Request: GET /api/message
📌 Response: "Hello, this is a plain text response!"
-----------------------------------------
3️⃣ ContentResult Ka Use Kab Karein?
🔹 Jab simple text ya HTML return karni ho bina kisi JSON ya object serialization ke.
🔹 Jab hume content-type specify karna ho jaise ki "text/plain" ya "text/html".
🔹 Jab custom API responses return karni ho bina JsonResult ka use kiye.
-----------------------------------------------------------
4️⃣ ContentResult Ke Example
✅ 1. Plain Text Response
[HttpGet]
public ContentResult GetPlainText()
{
    return Content("This is a plain text response", "text/plain");
}
📌 Response Type: text/plain
📌 Output: "This is a plain text response"
------------------------------------------------
✅ 2. HTML Response
[HttpGet]
public ContentResult GetHtmlContent()
{
    return Content("<h1>Welcome to ASP.NET Core</h1>", "text/html");
}
📌 Response Type: text/html
-----------------------------------------------
📌 Output:
<h1>Welcome to ASP.NET Core</h1>
---------------------------------------
✅ 3. JSON Response (Manually)
[HttpGet]
public ContentResult GetJsonContent()
{
    string jsonData = "{ \"message\": \"This is a JSON response\" }";
    return Content(jsonData, "application/json");
}
📌 Response Type: application/json
--------------------------------------
📌 Output:
{
  "message": "This is a JSON response"
}
📌 Note: Zyada tar cases me JSON response ke liye JsonResult ya Ok(object) use hota hai.

----------------------------------------------------
6️⃣ Real-World Scenario Example
🔹 Web Application me Custom Message Return Karna

[HttpGet("status")]
public ContentResult GetServerStatus()
{
    return Content("Server is running smoothly!", "text/plain");
}
📌 Use Case: Monitoring API jo server ka status check kar sake.
📌 Response: "Server is running smoothly!"
-------------------------------------------------------
7️⃣ Conclusion
✔ ContentResult simple responses return karne ke liye use hota hai.
✔ Yeh text, HTML, ya JSON content serve kar sakta hai.
✔ Content-Type manually specify karna zaroori hai agar HTML ya JSON return karna ho.
✔ For structured API responses, JsonResult ya Ok() zyada preferable hota hai.