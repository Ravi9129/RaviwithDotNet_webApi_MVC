using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 2_form tag helper
    {
        
    }
}
-------------------------------------------------
.NET Core ke form tag helper ki — isse form banane aur model se bind karne ka kaam bahut easy ho jata hai.
Ye Tag Helper Razor View me form ke HTML element ke saath use hota hai jisse asp-action, asp-controller, asp-route-* jaise attributes ka fayda uthaya ja sake.

🔍 Kya hota hai form tag helper?
Form Tag Helper allow karta hai ki tu Razor syntax me form likhte waqt controller ke action methods se bind kar sake — bina manually action URL likhe.
---------------------------------
✅ Syntax Example:

<form asp-action="Login" asp-controller="Account" method="post">
    <input asp-for="UserName" />
    <input asp-for="Password" type="password" />
    <button type="submit">Login</button>
</form>
-----------------------
🧠 Explanation:
asp-action="Login": ye form submit hone par Login action method ko call karega

asp-controller="Account": controller ka naam jahan Login method hai

method="post": form submit karega POST method ke through
-------------------------------------------------
🎯 Real Use Case:
Scenario: Tu ek login page bana raha hai jahan user username aur password enter kare aur backend me validate ho.
----------------------------------
Model:

public class LoginModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
----------------------------------------------
Controller:

public class AccountController : Controller
{
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // login logic
            return RedirectToAction("Dashboard");
        }

        return View(model);
    }
}
-----------------------------------------------
View:
@model LoginModel

<form asp-action="Login" method="post">
    <label asp-for="UserName"></label>
    <input asp-for="UserName" class="form-control" />
    <span asp-validation-for="UserName" class="text-danger"></span>

    <label asp-for="Password"></label>
    <input asp-for="Password" type="password" class="form-control" />
    <span asp-validation-for="Password" class="text-danger"></span>

    <button type="submit">Login</button>
</form>
-----------------------------------
💡 Features:
asp-route-*: kisi route param ke sath form bhejna

asp-antiforgery="true" by default hota hai (CSRF protection ke liye)

Clean aur strongly-typed model binding

Action URL automatically banata hai routing rules ke hisaab se
--------------------------------------------------
🧪 Example with Route Parameter:
<form asp-action="Update" asp-controller="Profile" asp-route-id="10" method="post">
Ye form submit karega /Profile/Update/10

-------------------------
 Important Points:
Feature	Purpose
asp-action	Action method ko target kare
asp-controller	Specific controller bataye
asp-route-*	Dynamic route values add kare
method="post/get"	HTTP method define kare
Anti-forgery token	Default hota hai hidden field ke form me