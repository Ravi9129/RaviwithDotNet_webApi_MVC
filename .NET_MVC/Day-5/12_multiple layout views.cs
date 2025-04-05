using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 12_multiple layout views
    {
        
    }
}
-----------------------------------
multiple layout views ki — matlab ek project me alag-alag layout files use karna, based on section, area, ya user type (admin/user/public).

🤔 Multiple Layouts kyun chahiye hote hain?
Real world me alag-alag user roles ya modules ke liye alag designs chahiye:

User area ke liye simple UI

Admin panel ke liye dashboard-type UI

Public pages ke liye lightweight layout

✅ Kaise banate hain multiple layouts?
Tu har ek layout ko Views/Shared/ me bana sakta hai:
---------------------------------------------
Example Structure:
/Views/Shared/
  ├── _Layout.cshtml                (Default for public views)
  ├── _AdminLayout.cshtml          (For Admin panel)
  └── _UserLayout.cshtml           (For logged-in user section)
🔧 Step by Step
-----------------------------------------------
🔹 Step 1: Default _Layout.cshtml
<!-- Views/Shared/_Layout.cshtml -->
<body>
  <header>Public Header</header>
  <main>@RenderBody()</main>
  <footer>Public Footer</footer>
</body>
--------------------------------------------
🔹 Step 2: Admin Layout
<!-- Views/Shared/_AdminLayout.cshtml -->
<body>
  <nav>Admin Sidebar</nav>
  <section class="admin-body">
    @RenderBody()
  </section>
</body>
---------------------------
🔹 Step 3: User Layout
<!-- Views/Shared/_UserLayout.cshtml -->
<body>
  <header>User Nav</header>
  <main class="user-body">
    @RenderBody()
  </main>
</body>
----------------------------
🧠 Layout Select Karna per View Basis
Method 1: View me manually layout set karna
@{
    Layout = "_AdminLayout";
}
Is line ko view ke top me daal de — ye specific layout use karega, _ViewStart.cshtml ko override kar dega.
-------------------------------------------
Method 2: Role-based layout selection in _ViewStart.cshtml
@{
    var user = Context.User;

    if (user.IsInRole("Admin"))
    {
        Layout = "_AdminLayout";
    }
    else if (user.IsInRole("User"))
    {
        Layout = "_UserLayout";
    }
    else
    {
        Layout = "_Layout";
    }
}
Tera layout dynamic ho gaya based on login role ✅
-------------------------------------------
📦 View Folder Based Layout
Tu chahe toh har view folder me apna _ViewStart.cshtml daal sakta hai:
---------------------
Example:
/Views/Admin/_ViewStart.cshtml      ➝ Layout = "_AdminLayout"
/Views/User/_ViewStart.cshtml       ➝ Layout = "_UserLayout"
/Views/_ViewStart.cshtml            ➝ Default for everything else
Ye approach clean hai — har section apna layout handle kare
-----------------------------------------------------------------
💥 Real Scenario:
Tera project hai ek Learning Platform

Admin: manage karega content – use _AdminLayout

User: course dashboard – use _UserLayout

Home/About: public info – use _Layout

Tera har controller alag folder me jaayega, aur unke layout @{ Layout = "_XYZLayout"; } se manage honge. Ya folder-level _ViewStart.cshtml se.
-----------------------------------------------------
✨ Summary:
Multiple Layouts = Better UI per module/role

@{ Layout = "_LayoutName"; } se manually set karo

_ViewStart.cshtml me dynamic logic daal sakte ho

Har folder me apna _ViewStart.cshtml daalke clean structure bana sakte ho

