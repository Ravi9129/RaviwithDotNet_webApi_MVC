using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 2_Nested Layouts 
    {
        
    }
}
---------------------------------------------
Nested Layouts ke baare mein. Ye topic important hai jab tu multi-level layout structure banana chahta hai. Jaise:

Ek Master Layout sabke liye (overall structure)

Fir uske andar chhoti-chhoti Child Layouts jo different modules ke liye customized hoti hain.
-------------------------------------------------------------
✅ Real Scenario
Tera project me:

MasterLayout → Header, footer, global CSS, JS

AdminLayout → Admin ke left menu, dashboard sidebar

UserLayout → Normal user ke liye simplified layout

Ab dono AdminLayout aur UserLayout, master layout ko inherit karenge — yehi hota hai nested layout.
----------------------------------------------------------------
🔹 Step-by-Step Setup
✅ Step 1: Master Layout (_MasterLayout.cshtml)
<!-- Views/Shared/_MasterLayout.cshtml -->
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"]</title>
</head>
<body>
    <header>My Site Header</header>

    @RenderBody() <!-- Injects Child Layout -->

    <footer>My Footer</footer>
</body>
</html>
----------------------------------------------
✅ Step 2: Child Layout (_AdminLayout.cshtml) jo MasterLayout inherit kare
<!-- Views/Shared/_AdminLayout.cshtml -->
@{
    Layout = "~/Views/Shared/_MasterLayout.cshtml"; // 👈 Nested Layout
}

<div class="admin-sidebar">Admin Menu</div>

<div class="admin-content">
    @RenderBody() <!-- Injects Actual View -->
</div>
-------------------------------------------------
✅ Step 3: View (Dashboard.cshtml) jo Child Layout use kare
<!-- Views/Admin/Dashboard.cshtml -->
@{
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
    ViewData["Title"] = "Admin Dashboard";
}

<h2>Welcome to Admin Panel</h2>
--------------------------------------------------
🔁 Flow of Rendering:
Dashboard.cshtml ka layout hai _AdminLayout.cshtml

_AdminLayout.cshtml ka layout hai _MasterLayout.cshtml
----------------------------------------
Final output:

_MasterLayout → wrap karta hai _AdminLayout

_AdminLayout → wrap karta hai actual Dashboard.cshtml
------------------------------------------------------
🧠 Why Use Nested Layouts?
Code reusability — master layout sabke liye common

Separation of concerns — har section ka apna look & feel

Cleaner structure — easy to maintain module-based views
--------------------------------------------------
⚠️ Important:
Har level pe @RenderBody() hona zaroori hai jahan tu content inject kar raha hai.

Layout file ka path always sahi dena (~/Views/Shared/...)

Tu chaahe toh @RenderSection() bhi use kar sakta nested layouts me.
--------------------------------------------------
🔥 Bonus Example: 3 Levels Deep
Dashboard.cshtml
 ⮑ _AdminLayout.cshtml
     ⮑ _MasterLayout.cshtml