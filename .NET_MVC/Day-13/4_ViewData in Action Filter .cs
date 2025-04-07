using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 4_ViewData in Action Filter 
    {
        
    }
}
----------------------------------------
ViewData in Action Filter ka use — ye useful hota hai jab tu layout, partial view, ya shared data ko har view me dynamically pass karna chahta hai without repeating logic in every controller action.

🔥 Scenario: Real-World Use Case
Real-World Example:
Tere app ke sabhi views ke header me ek dynamic message ya user info show karna hai.
Ab tu har controller action me likhne se acha, ek Action Filter bana le jo sabhi views me ViewData set kare.
------------------------------------------------------------
🛠 Step-by-Step: ViewData in Action Filter
✅ Step 1: Create Custom Action Filter

public class AddViewDataAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Access Controller & ViewData
        if (context.Controller is Controller controller)
        {
            controller.ViewData["AppName"] = "My Awesome App 🚀";
            controller.ViewData["CurrentTime"] = DateTime.Now.ToString("hh:mm tt");
        }

        base.OnActionExecuting(context);
    }
}
----------------------------------------------
✅ Step 2: Use it on Controller or Action

[AddViewData]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
}
Ab ViewData["AppName"] and ViewData["CurrentTime"] har view me available hai.
---------------------------------------
✅ Step 3: Access in Razor View

<h1>@ViewData["AppName"]</h1>
<p>Loaded at: @ViewData["CurrentTime"]</p>
🌐 Use Globally for All Controllers
----------------------------------------------
Tu is filter ko globally bhi register kar sakta hai:

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AddViewDataAttribute>();
});
-----------------------------------------
🔄 Dynamic Use Case Example
Use Case: User ke Role ke hisaab se UI me kuch hide/show

public override void OnActionExecuting(ActionExecutingContext context)
{
    if (context.Controller is Controller controller)
    {
        var user = context.HttpContext.User;

        controller.ViewData["IsAdmin"] = user.IsInRole("Admin");
    }
}
-----------------------------------------
View me:

@if ((bool)ViewData["IsAdmin"])
{
    <button>Delete User</button>
}
-------------------------------------------
🧠 Notes
ViewData works only with Views, not with JsonResult or API.

Action filters can access controller and manipulate both ViewData and ViewBag easily.

ViewData is dictionary-based, so null checks are important if unsure.
--------------------------------------------------------
✅ Summary
✔ Use ViewData in Action Filter to:

Set common view values (title, app name, version, etc.)

Avoid repetition across controller actions

Dynamically customize layouts/partials

