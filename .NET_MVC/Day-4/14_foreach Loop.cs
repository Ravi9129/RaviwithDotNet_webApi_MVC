using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 14_foreach Loop
    {
        
    }
}
------------------------------------------------
🔹 foreach Loop in ASP.NET Core (Razor & Controller)
📌 foreach Kya Hai?
🔹 foreach ek loop hai jo collection ya list ke har element ko iterate (loop) karne ke liye use hota hai.
🔹 Ye ASP.NET Core ke Controllers aur Razor Views dono me use hota hai.
🔹 Ye array, list, dictionary, database records sab ke saath kaam karta hai.
----------------------------------------------------
📌 1️⃣ foreach in Razor View
Agar aapko ek list ke elements ko display karna hai, toh foreach ka use karein.

👨‍💻 Example: Simple foreach in Razor
@{
    var names = new List<string> { "Ravi", "Rahul", "Sara", "John" };
}

<ul>
    @foreach (var name in names)
    {
        <li>@name</li>
    }
</ul>
✅ Output:

Ravi

Rahul

Sara

John
---------------------------------------------------
📌 2️⃣ foreach in Controller
Agar aapko controller me ek list process karni hai, toh foreach ka use kar sakte hain.

👨‍💻 Example: foreach in Controller

public IActionResult Index()
{
    List<string> names = new List<string> { "Ali", "Rahul", "Sara", "John" };
    
    List<string> upperCaseNames = new List<string>();
    
    foreach (var name in names)
    {
        upperCaseNames.Add(name.ToUpper());
    }

    ViewBag.Names = upperCaseNames;
    return View();
}
✅ Ye controller names ko uppercase me convert karega aur ViewBag.Names me bhejega.
----------------------------------------------------
📌 3️⃣ foreach with Model in View
Agar aapko database ya API se aaye huye data ko display karna hai, toh foreach Model ke saath use hota hai.

👨‍💻 Example: foreach with Model in View
@model List<UserModel>

<table>
    <tr>
        <th>ID</th>
        <th>Name</th>
        <th>Email</th>
    </tr>
    @foreach (var user in Model)
    {
        <tr>
            <td>@user.Id</td>
            <td>@user.Name</td>
            <td>@user.Email</td>
        </tr>
    }
</table>
✅ Agar Model ek list of users hai, toh ye unka data ek table me dikhayega.
---------------------------------------------
📌 4️⃣ foreach in API Controller
Agar aapko API response modify karna hai, toh foreach ka use hota hai.

👨‍💻 Example: foreach in API Controller
[HttpGet("users")]
public IActionResult GetUsers()
{
    List<UserModel> users = new List<UserModel>
    {
        new UserModel { Id = 1, Name = "Ali", Email = "ali@example.com" },
        new UserModel { Id = 2, Name = "Rahul", Email = "rahul@example.com" }
    };

    List<string> userEmails = new List<string>();

    foreach (var user in users)
    {
        userEmails.Add(user.Email);
    }

    return Ok(userEmails);
}
✅ API sirf users ke emails return karegi.
---------------------------
📌 5️⃣ foreach with Dictionary
Agar aapke paas key-value pairs hai, toh foreach ka use aise hoga:

👨‍💻 Example: foreach with Dictionary in Razor View
@{
    var userRoles = new Dictionary<string, string>
    {
        { "Ali", "Admin" },
        { "Rahul", "User" },
        { "Sara", "Editor" }
    };
}

@foreach (var user in userRoles)
{
    <p>@user.Key is a @user.Value</p>
}
✅ Output:
Ali is a Admin
Rahul is a User
Sara is a Editor
------------------------------------------------------
📌 Summary
✅ Basic foreach - List ke elements ko loop karne ke liye.
✅ foreach in Controller - Data ko process karne ke liye.
✅ foreach with Model - Database se aaye data ko display karne ke liye.
✅ foreach in API - Data manipulate karne ke liye.
✅ foreach with Dictionary - Key-Value pairs ko iterate karne ke liye.