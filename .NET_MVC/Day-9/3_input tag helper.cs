using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 3_input tag helper
    {
        
    }
}
--------------------------------------
input tag helper ki — ye Razor view me HTML <input> element ko model ke sath directly bind karta hai.

🔍 Kya hota hai input tag helper?
<input asp-for="PropertyName" />
Iska matlab hai — model ke kisi ek property ke liye ek input generate karna jisme:

name & id automatically set hota hai

model binding work karta hai

validation message bhi aata hai agar use kiya ho
-------------------------------------
✅ Simple Example:

public class RegisterModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
}
--------------------------------
View:

@model RegisterModel

<form asp-action="Register" method="post">
    <label asp-for="UserName"></label>
    <input asp-for="UserName" class="form-control" />

    <label asp-for="Email"></label>
    <input asp-for="Email" class="form-control" />

    <button type="submit">Register</button>
</form>
----------------------------------
🧠 asp-for Kya Karta Hai?
<input asp-for="UserName" /> =>

name="UserName"

id="UserName"

Value pre-fill karta hai agar model me data ho

Server-side validation ke liye support karta hai
---------------------------------------------
🧪 Real-life Scenario:
Suppose tu ek Profile Edit Page bana raha hai — user ke details pre-filled hote hain aur update karne hote hain.
---------------------
Model:
public class ProfileModel
{
    [Required]
    public string FullName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
-------------------------------------
View:

@model ProfileModel

<form asp-action="UpdateProfile" method="post">
    <div>
        <label asp-for="FullName"></label>
        <input asp-for="FullName" />
        <span asp-validation-for="FullName"></span>
    </div>

    <div>
        <label asp-for="Email"></label>
        <input asp-for="Email" type="email" />
        <span asp-validation-for="Email"></span>
    </div>

    <button type="submit">Update</button>
</form>
-------------------------------------------------
🧾 Features:
Feature	Benefit
asp-for	Strongly typed binding
Automatic name, id, value	Fewer bugs
Support client + server validation	Full support
Works with ViewModel	Easy integration
---------------------------------------------------
🧠 Advance:
Agar tu date ya password lena chahta hai:
-------
<input asp-for="BirthDate" type="date" />
<input asp-for="Password" type="password" />
-------------------------------------
⚠️ Important Points:
Tu jab input asp-for="Prop" use karta hai to Razor View automatically ModelState aur Model.Prop ka value bind karta hai

input tag helper form ke andar hi kaam karega