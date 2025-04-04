using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 10_Shared Views
    {
        
    }
}
-------------------------------
Shared Views kya hota hai?
Shared Views wo Razor Views (.cshtml) hote hain jo har Controller ya View me use kiye ja sakte hain.
----------------------------------------------
Inhe reuse karne ke liye ek common folder hota hai:

/Views/Shared/
🧱 Example Samajh:
Tere paas ek common layout hai ya ek error page jo har controller use karta hai.

Structure:
/Views/
   ├── Home/
   │     └── Index.cshtml
   ├── Product/
   │     └── Details.cshtml
   └── Shared/
         ├── _Layout.cshtml
         ├── _ValidationScriptsPartial.cshtml
         ├── Error.cshtml
         └── _MyCustomBox.cshtml
         -----------------------------------------------
🧠 Real-Time Use Cases:
Use Case	Description
_Layout.cshtml	Common layout for all views
Error.cshtml	Global error page
_ValidationScriptsPartial	Form validation script reuse
_LoginPartial.cshtml	Header login bar (used in layout)
_CustomComponent.cshtml	Custom reusable partial view
----------------------------------
🔁 Reuse Kaise Karte Hain?
-------------------------------
✅ In Layout:
@await Html.PartialAsync("_LoginPartial")
----------------------------------------
✅ In Any View:

@await Html.PartialAsync("~/Views/Shared/_MyCustomBox.cshtml")
-------------------------------
✅ With Model:

@await Html.PartialAsync("_MyBox", Model.YourData)
---------------------------------------------
🧪 Real Example: _Error.cshtml
<!-- /Views/Shared/Error.cshtml -->
@model ErrorViewModel

<h2>Error Occurred</h2>
<p>@Model.Message</p>
------------------------------------------------
Call Automatically from Error Middleware:
app.UseExceptionHandler("/Home/Error");
🛠 Why Use Shared Views?
DRY principle — code repeat nahi karna padta

Central control — ek view change karo, sab jaga effect

Maintainability — component-based architecture

Partial Views + Shared = View Components ka base
-----------------------------------------------------
🧩 Real Scenario:
Suppose:
Tere paas ek cart icon hai jo har page ke top bar me chahiye.

Tu kya karega?

✅ Ek _CartSummary.cshtml banata /Views/Shared/ me
✅ Usko @await Html.PartialAsync("_CartSummary") se sab views me inject kar deta.
------------------------------------
✨ Bonus Tips:
Naming _CustomName.cshtml style rakho for clarity

Shared folder me layout bhi rehta hai usually

Partial views + Shared = Real reuse power 💪
------------------------
summary:

Shared Views = "Ek baar bana, sab jaga chala."