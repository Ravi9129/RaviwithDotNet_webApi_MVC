using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 4_Partial Views with ViewData
    {
        
    }
}
----------------------------------------
Partial Views with ViewData ke baare mein. Ye tab kaam aata hai jab tu Partial View ko data dena chahta hai bina strongly-typed model ke.

🔥 Real Life Scenario:
Soch le:

Tere layout me ek Partial View hai: _PromoBanner.cshtml

Isme tu ek message dikhana chahta hai — "Sale ends tonight!"

Ye message tu ViewData se bhej sakta hai — no need for full model

✅ Step-by-Step Example
--------------------------------------------------
🔹 Step 1: Create Partial View
<!-- Views/Shared/_PromoBanner.cshtml -->
<div class="alert alert-info">
    <strong>@ViewData["Message"]</strong>
</div>
---------------------------------------
🔹 Step 2: Call it in Main View and Pass ViewData
@{
    ViewData["Message"] = "Hurry! 50% Off till Midnight!";
}
@await Html.PartialAsync("_PromoBanner")
⚠️ Note: Agar tu async use kar raha hai toh ViewData usme by default pass hota hai.
------------------------------------------------------
🔹 Bonus: Partial View inside a Layout
<!-- _Layout.cshtml -->
@{
    ViewData["Message"] = "Welcome to the Offer Zone!";
}
@await Html.PartialAsync("_PromoBanner")
------------------------------------
⚡ When to Use ViewData in Partial View?
Scenario	Why Use ViewData?
Small data pass karni hai (string, number)	Lightweight, no need for full model
ViewModel pass karna overkill lag raha	Just pass what’s needed
Multiple fields pass karne hain alag-alag	Use ViewData keys
---------------------------------------------
🧠 Pro Tips:
ViewData is a Dictionary<string, object>

Type casting zaroori hoti hai agar tu complex types pass kare

ViewBag bhi same kaam karega (ViewBag.Message), but ViewData zyada control deta hai
---------------------
❌ What Not to Do
Avoid sending huge complex model parts in ViewData, uske liye strongly typed models ya ViewModels use kar.