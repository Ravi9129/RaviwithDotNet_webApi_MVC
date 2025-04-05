using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 3_Partial Views
    {
        
    }
}
----------------------------------------------------
Partial Views ke baare mein. Ye ek aisa concept hai jo tere code ko modular, reusable aur maintainable banata hai. 
Jaise tu chhoti-chhoti UI blocks banata hai jinko multiple pages me reuse kar sake — wahi hota hai partial view ka kaam.
----------------------------------------
💡 Real Life Example:
Soch tu ek blog site bana raha hai:

Har page pe ek common "Recent Posts" list hai

Har page me ek "User Info Card" bhi dikhta hai

Tu chahe toh ye har page me copy-paste kare… ya ek baar ek partial view bana ke sab jagah use kar le.
----------------------------------------
🔧 Definition:
Partial View ek chhota Razor file hota hai jo kisi aur view me embed hota hai.

Ye full page render nahi karta

Sirf ek partial part of UI hota hai (jaise card, table, sidebar)
-----------------------------------------
✅ Use Cases:
User cards, product boxes, sidebar, navbar

Modal windows, search forms, contact blocks

Table rows (repeat hone wale structure)
----------------------------------------------------
🔥 Step-by-Step Example:
1. Partial View Create Karo

<!-- Views/Shared/_UserCard.cshtml -->
@model MyApp.Models.User

<div class="user-card">
    <h3>@Model.Name</h3>
    <p>Email: @Model.Email</p>
</div>
------------------------------
2. Call Partial View from Any View

<!-- Views/Home/Index.cshtml -->
@model MyApp.Models.User

<h2>Welcome to Home</h2>

@await Html.PartialAsync("_UserCard", Model)
--------------------------------------------
Ya agar async call nahi chahiye:

@Html.Partial("_UserCard", Model)
----------------------------------------------------
🔁 Partial View Without Model?
<!-- _LoginBox.cshtml -->
<form>
    <input type="text" placeholder="Username" />
</form>
-----------------------------------------------
Use anywhere:

@await Html.PartialAsync("_LoginBox")
--------------------------------------------------------
📦 Partial View Location Tips:
Usually Views/Shared/ me rakhte hain — so that globally available ho

Lekin tu module specific bhi bana sakta hai:

Views/User/_UserDetails.cshtml

Views/Product/_ProductCard.cshtml

---------------------------------------------------------------
🎯 When to Use:
Jab tu repeated UI banana chahe

Jab tu chhoti UI blocks ko reuse kare

Jab tu form ko split karna chahe

Jab tu AJAX update kare sirf ek part of page ka