using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 9__ViewImports
    {
        
    }
}
----------------------------
🧾 _ViewImports.cshtml – Kya hota hai?
Ye ek special Razor file hoti hai jo har View me shared common directives ko import karne ka kaam karti hai — jaise:

Namespaces

TagHelpers

Layout

Custom directives
--------------------------
Ye file usually hoti hai:

/Views/_ViewImports.cshtml
----------------------------------------------
🧱 Real Example – _ViewImports.cshtml
@using YourProjectName.Models
@using YourProjectName.ViewModels
@using Microsoft.AspNetCore.Identity
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@inherits RazorPage<dynamic>
🧠 Breakdown – Line by Line
---------------------------------------
✅ @using
Jaise C# me using karte ho, waise hi:

@using YourProjectName.Models
Har view me Model, ViewModel, ya koi bhi class use karne ke liye baar baar namespace likhne ki zarurat nahi.
--------------------------
✅ @addTagHelper
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
-------------------------------------------
Ye TagHelpers ko enable karta hai — jaise:

<form asp-controller="Home" asp-action="Submit">
Iske bina asp-* wale attributes kaam nahi karenge.
-----------------------------------
✅ @inherits
@inherits RazorPage<dynamic>`
Optional hai, mostly advanced cases me use hota hai jab tu Razor engine ko customize karta hai.
---------------------------------------------
🔥 Real-World Kaam:
🧩 Imagine:
Tere paas 50 views hain aur sab me tu @using YourProjectName.Models likh raha hai — headache.

Bas ek _ViewImports.cshtml bana le, sab Views automatically woh namespaces, taghelpers inherit kar lenge.

---------------------------------
⚠️ Tips:
Ye file optional hai, par hona chahiye project me.

Tu multiple _ViewImports.cshtml bana sakta hai for Area-wise configuration.
----------------------------------------------
Bonus:
Agar tu Razor Pages use karta hai (MVC nahi), tab bhi _ViewImports.cshtml kaam karta hai:

/Pages/_ViewImports.cshtml
Bhai short me bole toh —
_ViewImports.cshtml = "Razor Views ke liye Ek baar likho, har jagah use karo."