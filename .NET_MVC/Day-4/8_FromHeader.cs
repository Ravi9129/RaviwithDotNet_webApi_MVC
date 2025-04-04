using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 8_FromHeader
    {
        
    }
}
-------------------------------------------------------
🔹 [FromHeader] in ASP.NET Core
📌 Kya Hai?
[FromHeader] ek model binding attribute hai jo HTTP request headers se data extract karta hai aur usko controller method ke parameters me bind karta hai.

✅ Use Kab Karte Hain?

Jab client request headers me kuch important information bhej raha ho.

Authentication, API keys, Custom headers ya Metadata retrieve karne ke liye.

Security, logging, ya request tracking ke liye.
-------------------------------------------------
🛠 Example 1: Simple Header Binding
Agar ek client request "User-Agent" header bhej raha hai, toh hum isko [FromHeader] se extract kar sakte hain.

👨‍💻 Controller Method

[HttpGet("user-agent")]
public IActionResult GetUserAgent([FromHeader] string UserAgent)
{
    return Ok($"User-Agent: {UserAgent}");
}
---------------------------------------
📤 Client Request:

GET /user-agent
Headers:
User-Agent: Mozilla/5.0
--------------------------------------------
📥 Response:
User-Agent: Mozilla/5.0
✅ Header value bind ho gaya!
-------------------------------------------------------------
🛠 Example 2: Custom Header Binding
Agar tum custom headers bhejna chahte ho jaise "X-API-Key", toh [FromHeader] usko bhi handle karega.
---------------------------
👨‍💻 Controller Method
[HttpGet("check-api-key")]
public IActionResult CheckApiKey([FromHeader] string XApiKey)
{
    if (XApiKey == "12345")
        return Ok("API Key Valid!");
    
    return Unauthorized("Invalid API Key!");
}
-----------------------------------
📤 Client Request:
GET /check-api-key
Headers:
X-API-Key: 12345
--------------------------------------
📥 Response:
API Key Valid!
✅ Custom Header value bind ho gaya!
----------------------------------------------------
🛠 Example 3: Multiple Headers Bind Karna
Agar multiple headers bind karne hain, toh ek model class use kar sakte ho.
--------------------------------------------
👨‍💻 Model Class
public class RequestHeaders
{
    [FromHeader(Name = "X-Request-ID")]
    public string RequestId { get; set; }

    [FromHeader(Name = "User-Agent")]
    public string UserAgent { get; set; }
}
-------------------------------------------
👨‍💻 Controller Method
[HttpGet("headers-info")]
public IActionResult GetHeaders([FromHeader] RequestHeaders headers)
{
    return Ok($"Request ID: {headers.RequestId}, User-Agent: {headers.UserAgent}");
}
--------------------------------------
📤 Client Request:

GET /headers-info
Headers:
X-Request-ID: abc123
User-Agent: PostmanRuntime/7.26.8
--------------------------------------
📥 Response:

Request ID: abc123, User-Agent: PostmanRuntime/7.26.8
✅ Multiple headers bind ho gaye!
-----------------------------------------------
📌 Summary
✅ [FromHeader] request headers se values extract karta hai.
✅ Authentication tokens, API keys, metadata retrieve karne ke liye useful hai.
✅ Custom headers ko handle kar sakta hai.
✅ Multiple headers ko ek model class me bind kar sakte hain.