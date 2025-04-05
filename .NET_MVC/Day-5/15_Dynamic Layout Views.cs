using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 15_Dynamic Layout Views
    {
        
    }
}
--------------------------------------------------
Dynamic Layout Views ki — jiska matlab hai: alag-alag layout files use karna based on condition, jaise:

Admin ke liye alag layout

Normal user ke liye alag

Mobile/Desktop ke liye different layout

Route ya controller/action ke according layout change karna

✅ Real-life scenario
Tu ek web app bana raha hai jisme:

/admin/dashboard → admin layout chahiye

/home/index → normal layout chahiye
----------------------------------------------------------
🔹 Step-by-step Setup: Dynamic Layouts
✅ Step 1: Tere paas 2 layout files honi chahiye:
Views/Shared/_UserLayout.cshtml  
Views/Shared/_AdminLayout.cshtml
-----------------------------------
✅ Step 2: _ViewStart.cshtml me logic likhna
@{
    var path = Context.Request.Path.ToString().ToLower();

    if (path.StartsWith("/admin"))
    {
        Layout = "~/Views/Shared/_AdminLayout.cshtml";
    }
    else
    {
        Layout = "~/Views/Shared/_UserLayout.cshtml";
    }
}
Ye Request.Path check karta hai current route ka path aur uske hisaab se layout assign karta hai.
------------------------------------------
🔁 Alternate Example: Role ke hisaab se
Agar tu user role ke basis pe dynamic layout chahata hai:

@{
    var user = Context.User;

    if (user.Identity.IsAuthenticated && user.IsInRole("Admin"))
    {
        Layout = "~/Views/Shared/_AdminLayout.cshtml";
    }
    else
    {
        Layout = "~/Views/Shared/_UserLayout.cshtml";
    }
}
------------------------------------------
⚠ Important Notes:
_ViewStart.cshtml me Context sirf ASP.NET Core me milega (Razor Pages aur MVC dono me).

Agar tu kisi specific view me Layout = null karega, toh layout use hi nahi hoga.

Tujhe layout path accurate dena hoga, warna error aayega.
----------------------------------------------
💡 Pro Tip:
Agar tu multiple layouts use karta hai, toh folders use karke clean architecture bana:
Views/
  Shared/
    _UserLayout.cshtml
    _AdminLayout.cshtml
  Admin/
    Dashboard.cshtml
  Home/
    Index.cshtml
    ------------------------------------------
🧠 Summary:
Situation	Layout
/admin/* path	_AdminLayout.cshtml
/home/* or others	_UserLayout.cshtml
Role-based layout	if user.IsInRole("Admin")
View-specific override	Layout = "..." inside that view
