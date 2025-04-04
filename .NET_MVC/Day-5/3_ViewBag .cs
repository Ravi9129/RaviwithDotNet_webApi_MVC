using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 3_ViewBag 
    {
        
    }
}
---------------------------------------------
ViewBag in ASP.NET Core MVC
📌 ViewBag Kya Hai?
🔹 ViewBag ek dynamic property hai jo data ko Controller se View tak pass karne ke liye use hoti hai.
🔹 Yeh internally ViewData ka wrapper hai, iska matlab yeh ViewData ki tarah hi kaam karta hai, lekin typecasting ki zaroorat nahi hoti.
🔹 ViewBag sirf current request tak hi valid hota hai.
---------------------------
📌 1️⃣ ViewBag Ka Basic Example
Controller Code
-----------
public class HomeController : Controller 
{
    public IActionResult Index()
    {
        ViewBag.Message = "Welcome to ASP.NET Core MVC!";
        ViewBag.Year = 2025;
        return View();
    }
}
-----------------------------------
View (Index.cshtml)
<h2>@ViewBag.Message</h2>
<p>Current Year: @ViewBag.Year</p>
----------------------------
✅ Output:
Welcome to ASP.NET Core MVC!
Current Year: 2025
-----------------------------------------------------------------
📌 2️⃣ ViewBag with Complex Data (Objects)
Agar aap ViewBag me objects ya lists store karte ho, toh direct access kar sakte ho, typecasting ki zaroorat nahi hoti.
---------------
Controller Code
---------------
public IActionResult Index()
{
    ViewBag.Names = new List<string> { "Alice", "Bob", "Charlie" };
    return View();
}
-------------------------------------------------
View (Index.cshtml)
<ul>
    @foreach (var name in ViewBag.Names)
    {
        <li>@name</li>
    }
</ul>
✅ Output:
Alice

Bob

Charlie
-----------------------------------------------
📌 3️⃣ ViewBag in Partial Views
Agar aap ViewBag ko Partial View ke sath use karna chahte ho, toh bhi yeh kaam karega.

Parent View (Index.cshtml)
@{
    ViewBag.Title = "Dashboard";
}
<partial name="_Header" />
------------------------------------
Partial View (_Header.cshtml)
<h1>@ViewBag.Title</h1>
✅ Output:
Dashboard

-----------------------------------------
📌 5️⃣ ViewBag Use Kab Karna Chahiye?
✅ Jab Controller se View me data pass karna ho.
✅ Jab dynamic properties ka use karna ho (matlab typecasting se bachna ho).
✅ Jab ViewData ka alternative chahiye (kyunki ViewBag internally ViewData hi use karta hai).
--------------------------------
❌ Use Mat Karo Agar:

Strongly Typed Model ka use kar rahe ho.

TempData required hai (redirect me bhi data chahiye).
----------------------------------------------------------
📌 Conclusion
✅ ViewBag ek dynamic property hai jo Controller se View me data bhejne ke liye use hoti hai.
✅ ViewBag.PropertyName ka use karke data set aur get hota hai.
✅ Typecasting ki zaroorat nahi hoti, unlike ViewData.
✅ Ek HTTP request tak hi valid rehta hai.