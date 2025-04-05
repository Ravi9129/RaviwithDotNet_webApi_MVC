using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 13_view data in layout views
    {
        
    }
}
---------------------------------------
Layout View me ViewData kaise use hota hai. Real-world scenario ke saath simple example dunga jo tujhe turant samajh aa jaaye.

🔥 Q: ViewData ko Layout View me kyun use karte hain?
Kabhi-kabhi tujhe kisi common layout me data chahiye hota hai jo har page ke according change hota hai:

Page title

Logged-in user ka naam

Custom heading

Cart count

etc.

Yahi kaam ViewData ya ViewBag se hota hai — kyunki ye values controller se views tak jaati hain (including layout).

✅ Real Scenario
Tere paas ek layout hai jisme tu chahta hai har page ka custom title aaye <title> tag me.
---------------------------------------------------
🔹 Step 1: Layout file me ViewData use karna
<!-- Views/Shared/_Layout.cshtml -->
<head>
    <title>@ViewData["Title"] - MyApp</title>
</head>
<body>
    <header>
        <h1>@ViewData["Header"]</h1>
    </header>

    <main>
        @RenderBody()
    </main>
</body>
---------------------------------------------
🔹 Step 2: Controller me ViewData set karna
public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home Page";
        ViewData["Header"] = "Welcome to Home";

        return View();
    }

    public IActionResult About()
    {
        ViewData["Title"] = "About Page";
        ViewData["Header"] = "Know About Us";

        return View();
    }
}
Ab jab tu /Home/Index open karega, layout ke <title> me "Home Page - MyApp" dikhai dega, aur header me "Welcome to Home" 👍
--------------------------------------------------------------
🔄 Alternative: ViewBag
----------------
Same kaam tu ViewBag se bhi kar sakta hai:
ViewBag.Title = "Home Page";
-----------------------------------------------------
Aur layout me:
<title>@ViewBag.Title - MyApp</title>
Dono ka kaam same hai, par ViewData dictionary based hai aur ViewBag dynamic.
----------------------------------------------
💡 Bonus Tip: Default Title via _ViewStart.cshtml
Agar tu har page pe default title chahata hai:

@{
    ViewData["Title"] = ViewData["Title"] ?? "Default Page";
}
-----------------------------------
🧠 Summary:
Purpose	Use
Layout title, header, etc.	@ViewData["Title"] in layout
Controller set	ViewData["Title"] = "xyz"
Optional alternative	ViewBag.Title = "xyz"
Reusable default	Set in _ViewStart.cshtml
