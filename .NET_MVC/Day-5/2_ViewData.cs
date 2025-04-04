using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 2_ViewData
    {
        
    }
}
---------------------------------
🔹 ViewData in ASP.NET Core MVC
📌 ViewData Kya Hai?
🔹 ViewData ek dictionary-based object hai jo data ko Controller se View tak pass karne ke liye use hoti hai.
🔹 Yeh dynamically store karti hai data aur ViewData["key"] syntax ka use karke access ki jati hai.
🔹 ViewData ka scope sirf ek HTTP request tak limited hota hai (matlab ek hi View me available hoti hai, dusre request me nahi).
----------------------------------------------- 
📌 1️⃣ ViewData Ka Basic Example
Controller Code
public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Message"] = "Welcome to ASP.NET Core MVC!";
        ViewData["Year"] = 2025;
        return View();
    }
}
------------------------------
View (Index.cshtml)
<h2>@ViewData["Message"]</h2>
------------------------
<p>Current Year: @ViewData["Year"]</p>
✅ Output:
Welcome to ASP.NET Core MVC!
Current Year: 2025
-----------------------------------------------------
📌 2️⃣ ViewData with Complex Data (Objects)
Agar aap ViewData me object ya list store karna chahte ho, toh typecasting karni padegi.

Controller Code

public IActionResult Index()
{
    ViewData["Names"] = new List<string> { "Alice", "Bob", "Charlie" };
    return View();
}
------------------------------------------
View (Index.cshtml)

@{
    var names = ViewData["Names"] as List<string>;
}
<ul>
    @foreach (var name in names)
    {
        <li>@name</li>
    }
</ul>
✅ Output:
Alice

Bob

Charlie
-------------------------------------------------------------
📌 3️⃣ ViewData in Partial Views
Agar aap ViewData ko Partial View ke sath use karna chahte ho, toh bhi yeh work karega.
------------------
Parent View (Index.cshtml)

@{
    ViewData["Title"] = "Dashboard";
}
<partial name="_Header" />
------------------------------
Partial View (_Header.cshtml)

<h1>@ViewData["Title"]</h1>
------------------
✅ Output:

Dashboard

------------------------------------------
📌 5️⃣ ViewData Use Kab Karna Chahiye?
✅ Jab Controller se View me data pass karna ho.
✅ Jab dictionary-based dynamic storage chahiye.
✅ Jab ViewBag ka alternative chahiye (kyunki ViewBag internally ViewData hi use karta hai).
----------
❌ Use Mat Karo Agar:

Strongly Typed Model ka use kar rahe ho.

TempData required hai (redirect me bhi data chahiye).
-----------------------------------------------------------
📌 Conclusion
✅ ViewData ek dictionary object hai jo Controller se View me data bhejne ke liye use hota hai.
✅ ViewData["key"] ka use karke data set aur get hota hai.
✅ Typecasting zaroori hoti hai agar objects ya collections store karein.
✅ Ek HTTP request tak hi valid rehta hai.