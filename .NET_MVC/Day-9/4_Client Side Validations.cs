using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 4_Client Side Validations
    {
        
    }
}
---------------------------------------
Client Side Validations ki — jo user ka form browser me hi validate karta hai, server tak request jaane se pehle. Fast hota hai, user-friendly hota hai, aur unnecessary server hits se bachaata hai.

🔍 Client Side Validation kya hoti hai?
Jab tu form bharte waqt browser hi turant batata hai:

“ye field required hai”

“email galat hai”

“minimum characters 5 hone chahiye”

Ye sab client side validations hote hain — bina server ke response ka wait kiye.

🧠 ASP.NET Core me ye kaise kaam karta hai?
ASP.NET Core Razor Views me agar tu Data Annotations lagata hai model pe aur proper scripts use karta hai, to ye validation auto enable ho jaati hai.
-----------------------------------------
✅ Step by Step Example:
1. Model:

public class RegisterModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 3)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Range(18, 60)]
    public int Age { get; set; }
}
----------------------------------
2. View:

@model RegisterModel

<form asp-action="Register" method="post">
    <input asp-for="UserName" />
    <span asp-validation-for="UserName"></span>

    <input asp-for="Email" />
    <span asp-validation-for="Email"></span>

    <input asp-for="Age" type="number" />
    <span asp-validation-for="Age"></span>

    <button type="submit">Register</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
-----------------------
3. _ValidationScriptsPartial:
Is file me yahi scripts hoti hain:

<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
-----------------------------------
⚙️ Kaise kaam karta hai?
asp-validation-for span automatically bind hota hai ModelState se

JavaScript (jQuery Validation + Unobtrusive Validation) check karta hai form submit ke time pe

Agar galti hoti hai, turant error dikhata hai bina page reload ke
--------------------------------------------------------
🧪 Real-Life Use Case:
Imagine kar tu ek registration page bana raha hai — user email galat bhar de, ya age chhoti de de. Tu chaahega ki wo turant error dekh le, submit na ho jab tak sab sahi na ho.
-----------------------
✅ Benefits:
⚡ Super fast (no server hit)

🙅‍♂️ Prevents invalid form submits

😎 Better user experience
-----------------------------------------
🧠 Note:
Server-side validation must bhi ho — kyunki client side ko bypass kiya ja sakta hai (via DevTools ya Postman)

Client validation is UX, not Security
---------------------------------------------
🔒 Combine karein dono:
✔ Client side: for fast feedback
✔ Server side: for security & validation guarantee

