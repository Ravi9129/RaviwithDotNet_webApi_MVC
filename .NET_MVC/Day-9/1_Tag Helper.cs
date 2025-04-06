using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 1_Tag Helper
    {
        
    }
}
--------------------------------------------------------
Tag Helpers ki — ye ASP.NET Core ka ek zabardast feature hai jo HTML markup ke andar C# logic use karne ka simple aur readable tareeqa deta hai.

🔍 Kya Hota Hai Tag Helper?
Tag Helpers allow karte hain HTML ke andar Razor syntax ke zariye C# code likhne ko, lekin HTML jaise dikhte hain — jisse readability aur maintainability dono improve hoti hai.
----------------------------------------------
Jaise ki:

<input asp-for="UserName" class="form-control" />
Ye Razor syntax hai lekin dikhta hai jaise normal HTML tag ho. Ismein asp-for ek tag helper hai jo model ke UserName property se bind ho raha hai.
---------------------------------------
✅ Fayda Kya Hai?
Feature	Fayda
HTML-friendly	Frontend developers ke liye readable
Intellisense Support	Visual Studio me proper suggestions milte hain
Model Binding Easy	asp-for, asp-action, asp-controller etc. se strong binding
Less Bugs	Hardcoded route aur form action se bachav
-----------------------------------------
🎯 Real Life Example
Agar tu ek form banata hai login ka:

<form asp-action="Login" method="post">
    <label asp-for="UserName"></label>
    <input asp-for="UserName" class="form-control" />

    <label asp-for="Password"></label>
    <input asp-for="Password" type="password" class="form-control" />

    <button type="submit">Login</button>
</form>
asp-for="UserName" — ye tag helper Model.UserName se bind hota hai
asp-action="Login" — ye form ka action Login method pe route karega.
-------------------------------------
🔧 Common Tag Helpers
----------------------------------------------
📌 1. asp-for
Model ki property se bind karta hai

<input asp-for="Email" />
---------------------------------------------
📌 2. asp-action, asp-controller
Action method ya controller route define karne ke liye

<a asp-action="Edit" asp-controller="Users" asp-route-id="10">Edit</a>
------------------------------------------
📌 3. asp-route-*
Dynamic route parameters bhejne ke liye

<a asp-action="Details" asp-route-id="5">Details</a>
--------------------------------------------
📌 4. asp-validation-for
Validation message dikhane ke liye

<span asp-validation-for="UserName" class="text-danger"></span>
---------------------------------------
📌 5. environment tag helper
Environment based HTML dikhane ke liye (dev/prod)

<environment include="Development">
    <script src="site.js"></script>
</environment>
-------------------------------------
💡 Custom Tag Helper Banana Hai?
Agar tu khud ka tag helper banana chahta hai:

[HtmlTargetElement("uppercase")]
public class UpperCaseTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var content = output.Content.GetContent();
        output.Content.SetContent(content.ToUpper());
    }
}
------------------------------
Use in view:
<uppercase>This will be capital</uppercase>
----------------------------------------
⚠️ Kab Use Kare?
✅ Form fields generate karne ke liye (especially with validation)

✅ URLs banane ke liye based on routing

✅ Strongly typed model binding ke liye

✅ Cleaner aur maintainable code ke liye

