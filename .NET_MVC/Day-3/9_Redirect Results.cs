using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 9_Redirect Results
    {
        
    }
}
-----------------------------------------------------------
Redirect Results in ASP.NET Core 🚀
1️⃣ Redirect Results Kya Hai?
🔹 Redirect Results controllers me client ko kisi dusre endpoint ya URL par bhejne ke liye use hote hain.
🔹 Ye IActionResult interface ka part hain aur 301, 302, 307, 308 status codes ke sath response bhejte hain.

2️⃣ Kab Use Karein?
✅ Jab user ko kisi aur page ya action method pe redirect karna ho.
✅ Jab form submission ke baad dusre action pe bhejna ho (e.g., Login ke baad Dashboard).
✅ Jab SEO aur URL structure ko maintain karna ho (Permanent Redirect vs Temporary Redirect).
------------------------------------------------------------
3️⃣ Redirect Results Ke Types & Examples
✅ 1. Redirect() (302 Found - Temporary)
🔹 Client ko dusre URL par temporarily bhejta hai.

public IActionResult RedirectToGoogle()
{
    return Redirect("https://www.google.com");
}
📌 Response: 302 Found, user Google par redirect ho jayega.
------------------------------------------------------------------
✅ 2. RedirectPermanent() (301 Moved Permanently)
🔹 Client ko permanently dusre URL pe bhejta hai (SEO-friendly).

public IActionResult RedirectToNewSite()
{
    return RedirectPermanent("https://www.newsite.com");
}
📌 Response: 301 Moved Permanently, browser cache me store karega.
-------------------------------------------------------------
✅ 3. RedirectToAction() (302 Found - Temporary)
🔹 Controller ke kisi specific action method pe redirect karta hai.

public IActionResult GoToDashboard()
{
    return RedirectToAction("Dashboard");
}

public IActionResult Dashboard()
{
    return Content("Welcome to Dashboard!");
}
📌 Response: 302 Found, Dashboard() method call hoga.
------------------------------------------------------------------
✅ 4. RedirectToActionPermanent() (301 Moved Permanently)
🔹 Controller ke kisi specific action pe permanently redirect karta hai.

public IActionResult MoveToDashboard()
{
    return RedirectToActionPermanent("Dashboard");
}
📌 Response: 301 Moved Permanently, Dashboard() action hamesha access hoga.
----------------------------------------------------------
✅ 5. RedirectToRoute() (302 Found - Temporary)
🔹 Route ke basis par redirect karta hai (Route Name se).

public IActionResult RedirectToUserProfile()
{
    return RedirectToRoute("UserProfileRoute");
}
📌 Response: 302 Found, User Profile Route call hoga.
------------------------------------------------------------
✅ 6. RedirectToRoutePermanent() (301 Moved Permanently)
🔹 Route ke basis par permanently redirect karta hai.
public IActionResult MoveToProfile()
{
    return RedirectToRoutePermanent("UserProfileRoute");
}
📌 Response: 301 Moved Permanently, Route cache me store hoga.
---------------------------------------------------------
4️⃣ Real-World Scenario Example
🔹 Login ke baad user ko Dashboard pe redirect karna:

[HttpPost("login")]
public IActionResult Login(string username, string password)
{
    if (username == "admin" && password == "12345")
    {
        return RedirectToAction("Dashboard");
    }

    return RedirectToAction("LoginFailed");
}

public IActionResult Dashboard()
{
    return Content("Welcome Admin!");
}

public IActionResult LoginFailed()
{
    return Content("Invalid Credentials, Try Again.");
}
📌 Use Cases:
✔ Correct Login ⇒ 302 Found → Dashboard pe redirect.
✔ Wrong Login ⇒ 302 Found → Login Failed page pe redirect.
---------------------------------------------
5️⃣ Conclusion
✔ Redirect Results client ko dusre action, URL ya route pe bhejne ke liye use hote hain.
✔ SEO aur performance ke liye correct redirect type use karna zaroori hai (Permanent vs Temporary).
✔ RedirectToAction aur RedirectToRoute framework-friendly hain (Controller-Action routing me best).