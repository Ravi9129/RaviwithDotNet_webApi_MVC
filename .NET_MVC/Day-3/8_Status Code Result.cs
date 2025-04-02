using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 8_Status Code Result
    {
        
    }
}
-----------------------------------
Status Code Result in ASP.NET Core 🚀
1️⃣ Status Code Result Kya Hai?
🔹 Status Code Result ek tarika hai jisse ASP.NET Core controller actions me HTTP status codes return kiye jate hain.
🔹 Ye IActionResult interface ka part hota hai aur standard HTTP response codes return karta hai, jaise:

200 OK

400 Bad Request

401 Unauthorized

403 Forbidden

404 Not Found

500 Internal Server Error

2️⃣ Kab Use Karein?
✅ Jab sirf HTTP status code return karna ho bina koi extra data ke.
✅ Jab client ko sirf response status batana ho (e.g., success, failure, unauthorized access).
✅ Jab API response ko structured way me return karna ho.
------------------------------------------------------
3️⃣ Status Code Result Ka Example
✅ 1. OkResult (200 OK)
public IActionResult Success()
{
    return Ok(); // Returns 200 OK without data
}
📌 Response: 200 OK
-----------------------------------------------
✅ 2. OkObjectResult (200 OK with Data)
public IActionResult GetUser()
{
    var user = new { Id = 1, Name = "Amit" };
    return Ok(user); // Returns 200 OK with JSON data
}
📌 Response: { "Id": 1, "Name": "Amit" } (JSON format)
-----------------------------------------------------------
✅ 3. BadRequestResult (400 Bad Request)
public IActionResult InvalidRequest()
{
    return BadRequest("Invalid input provided");
}
📌 Response: 400 Bad Request with "Invalid input provided"
-------------------------------------------------------------
✅ 4. UnauthorizedResult (401 Unauthorized)
public IActionResult AccessDenied()
{
    return Unauthorized();
}
📌 Response: 401 Unauthorized
------------------------------------------------------
✅ 5. ForbidResult (403 Forbidden)
public IActionResult RestrictedAccess()
{
    return Forbid(); // User is authenticated but does not have access
}
📌 Response: 403 Forbidden
-------------------------------------------------------
✅ 6. NotFoundResult (404 Not Found)
public IActionResult GetItem(int id)
{
    if (id < 1)
    {
        return NotFound("Item not found");
    }
    return Ok(new { Id = id, Name = "Item " + id });
}
📌 Response:
✔ Valid ID: 200 OK with JSON
✔ Invalid ID: 404 Not Found
-------------------------------------------------
✅ 7. ConflictResult (409 Conflict)
public IActionResult DuplicateRequest()
{
    return Conflict("This record already exists");
}
📌 Response: 409 Conflict with "This record already exists"
--------------------------------------------------------------------
✅ 8. StatusCodeResult (Custom HTTP Status Code)
public IActionResult CustomError()
{
    return StatusCode(418, "I'm a teapot!"); // 418 is a joke HTTP status code 😄
}
📌 Response: 418 I'm a teapot!
-------------------------------------------------------------
4️⃣ Real-World Scenario Example
🔹 User Login API jo different status codes return kare:

[HttpPost("login")]
public IActionResult Login(string username, string password)
{
    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    {
        return BadRequest("Username and password are required.");
    }

    if (username == "admin" && password == "12345")
    {
        return Ok(new { Message = "Login Successful", Token = "abc123xyz" });
    }

    return Unauthorized("Invalid credentials.");
}
📌 Use Cases:
✔ Missing Username/Password ⇒ 400 Bad Request
✔ Valid Login ⇒ 200 OK with token
✔ Invalid Login ⇒ 401 Unauthorized
--------------------------------------------------------
5️⃣ Conclusion
✔ Status Code Results API responses ko better control dene me madad karte hain.
✔ Har response scenario ke liye ek appropriate status code use karna best practice hai.
✔ Ye API clients ko clear indication dete hain ki request ka kya status hai.