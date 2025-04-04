using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 13_switch Statement
    {
        
    }
}
--------------------------------------------
🔹 switch Statement in ASP.NET Core (Razor & Controller)
📌 switch Statement Kya Hai?
🔹 switch ek conditional statement hai jo multiple cases ko handle karta hai.
🔹 Agar ek variable ki value kisi specific case se match hoti hai toh uska code execute hota hai.
🔹 Razor Views aur Controllers dono jagah switch ka use hota hai.
---------------------------------------------------
📌 1️⃣ Basic switch in Razor View
Agar aapko kisi ek variable ki value ke basis pe HTML content dikhana hai, toh switch ka use karein.

👨‍💻 Example: Simple switch in Razor
@{
    var userRole = "Admin";
}

@switch (userRole)
{
    case "Admin":
        <h2>Welcome, Admin!</h2>
        break;

    case "User":
        <h2>Welcome, User!</h2>
        break;

    default:
        <h2>Welcome, Guest!</h2>
        break;
}
✅ Agar userRole = "Admin" hoga toh "Welcome, Admin!" show karega.
----------------------------------------------------------------------------
📌 2️⃣ switch in Controller
Agar aapko Controller me koi decision lena hai toh switch ka use hota hai.

👨‍💻 Example: switch in Controller
public IActionResult Index()
{
    string userRole = "Admin";
    string message;

    switch (userRole)
    {
        case "Admin":
            message = "Welcome, Admin!";
            break;

        case "User":
            message = "Welcome, User!";
            break;

        default:
            message = "Welcome, Guest!";
            break;
    }

    ViewBag.Message = message;
    return View();
}
✅ Ye ViewBag.Message me ek specific message set karega jo View me display hoga.
----------------------------------------------------------------
📌 3️⃣ switch with Model Data
Agar aap Model ke data ke basis pe koi action lena chahte ho toh switch ka use kar sakte ho.

👨‍💻 Example: switch with Model Property in View
@model UserModel

@switch (Model.AccountType)
{
    case "Premium":
        <h2>Welcome, Premium Member!</h2>
        break;

    case "Basic":
        <h2>Enjoy Your Free Account!</h2>
        break;

    default:
        <h2>Upgrade to Premium!</h2>
        break;
}
✅ Agar AccountType = "Premium" hoga toh Premium message dikhayega.
-----------------------------------------------------------------------
📌 4️⃣ switch in API Controller
Agar aapko API Response different dena hai toh switch ka use kar sakte hain.

👨‍💻 Example: switch in API Controller
[HttpGet("{status}")]
public IActionResult GetStatusMessage(string status)
{
    string message;

    switch (status)
    {
        case "success":
            message = "Operation completed successfully!";
            return Ok(message);

        case "error":
            message = "An error occurred.";
            return BadRequest(message);

        case "unauthorized":
            message = "You are not authorized!";
            return Unauthorized(message);

        default:
            message = "Unknown status.";
            return NotFound(message);
    }
}
✅ Ye API status ke basis pe different HTTP responses return karega.
--------------------------------------------------
📌 Summary
✅ Basic switch - Ek variable ki value ke basis pe specific case execute karega.
✅ switch in Razor - Different HTML content show karne ke liye.
✅ switch in Controller - Backend logic handle karne ke liye.
✅ switch with Model - Model data ke basis pe decision lene ke liye.
✅ switch in API - API response customize karne ke liye.