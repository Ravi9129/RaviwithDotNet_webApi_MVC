using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 7_IActionResult 
    {
        
    }
}
--------------------------------------
IActionResult in ASP.NET Core 🚀
1️⃣ IActionResult Kya Hai?
🔹 IActionResult ek interface hai jo ASP.NET Core controllers ke action methods ka return type define karta hai.
🔹 Iska use different types ke HTTP responses return karne ke liye hota hai, jaise:

JSON (JsonResult)

Views (ViewResult)

Redirects (RedirectResult)

Files (FileResult)

HTTP Status Codes (StatusCodeResult)

Example:
public IActionResult GetMessage()
{
    return Content("Hello, this is an IActionResult example!");
}
📌 Response: "Hello, this is an IActionResult example!"
--------------------------------------------
2️⃣ IActionResult Kab Use Karein?
🔹 Jab multiple response types return karne ho (JSON, View, Redirect, File, etc.).
🔹 Jab controller flexible aur reusable banani ho.
🔹 Jab response type runtime pe decide karna ho.

3️⃣ IActionResult Ke Examples
✅ 1. ViewResult (HTML View return karna)

public IActionResult ShowHomePage()
{
    return View("Home");
}
📌 Response: Home.cshtml view render hoga.
-----------------------------------------------------------
✅ 2. JsonResult (JSON Response dena)

public IActionResult GetUser()
{
    var user = new { Id = 1, Name = "Amit", Role = "Admin" };
    return Json(user);
}
📌 Response: { "Id": 1, "Name": "Amit", "Role": "Admin" } (JSON format me).
----------------------------------------------------------
✅ 3. ContentResult (Text return karna)
public IActionResult GetText()
{
    return Content("This is a simple text response");
}
📌 Response: "This is a simple text response"
-------------------------------------------
✅ 4. RedirectResult (Redirect to another action)

public IActionResult RedirectToHome()
{
    return RedirectToAction("Index", "Home");
}
📌 Response: HomeController ke Index action pe redirect karega.
---------------------------------------------------------
✅ 5. FileResult (File Download karna)

public IActionResult DownloadFile()
{
    byte[] fileBytes = System.IO.File.ReadAllBytes("wwwroot/files/sample.pdf");
    return File(fileBytes, "application/pdf", "downloaded_sample.pdf");
}
📌 Response: sample.pdf file download hogi.
-------------------------------------------------------------
✅ 6. StatusCodeResult (Custom HTTP Status Codes)
public IActionResult UnauthorizedAccess()
{
    return StatusCode(401, "Unauthorized Access");
}
📌 Response: 401 Unauthorized
----------------------------------------------------------
✅ 7. NotFoundResult (404 Not Found)

public IActionResult GetItem(int id)
{
    if (id < 1) 
    {
        return NotFound("Item not found");
    }
    return Ok(new { Id = id, Name = "Item " + id });
}
📌 Response: Agar id < 1 hua toh 404 Not Found.


-----------------------------------------------------------------
5️⃣ Real-World Scenario Example
🔹 User API jo different responses return kare:

[HttpGet("get-user/{id}")]
public IActionResult GetUser(int id)
{
    if (id <= 0)
    {
        return BadRequest("Invalid user ID");
    }

    var user = new { Id = id, Name = "Rohan", Email = "rohan@example.com" };
    
    if (user == null)
    {
        return NotFound();
    }

    return Ok(user);
}
📌 Use Cases:
✔ Invalid ID ⇒ 400 Bad Request
✔ User Not Found ⇒ 404 Not Found
✔ Success Response ⇒ 200 OK with JSON
---------------------------------------------
6️⃣ Conclusion
✔ IActionResult controller actions ka flexible return type hai jo multiple response types handle karta hai.
✔ Multiple response scenarios ko manage karne ke liye best practice hai.
✔ ActionResult<T> jab use karein jab sirf ek specific type ka response dena ho.