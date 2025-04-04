using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 15_for Loop
    {
        
    }
}
---------------------------------------------
🔹 for Loop in ASP.NET Core (Razor & Controller)
📌 for Kya Hai?
🔹 for loop ek iterative loop hai jo fixed iterations ke liye use hota hai.
🔹 Ye tab use hota hai jab aapko kisi specific range ya index-based collection par iterate karna ho.
🔹 ASP.NET Core me for loop Razor Views aur Controllers dono me use hota hai.
-----------------------------------------------
📌 1️⃣ for in Razor View
Agar aapko index-based loop execute karna hai, toh for ka use karein.

👨‍💻 Example: Simple for Loop in Razor
<ul>
    @for (int i = 1; i <= 5; i++)
    {
        <li>Item @i</li>
    }
</ul>
✅ Output:

Item 1

Item 2

Item 3

Item 4

Item 5
-------------------------------------------
📌 2️⃣ for Loop in Controller
Agar aapko ek list process karni hai aur usme kuch modification karna hai, toh for ka use karein.

👨‍💻 Example: for in Controller

public IActionResult Index()
{
    List<string> names = new List<string> { "Ali", "Rahul", "Sara", "John" };
    
    for (int i = 0; i < names.Count; i++)
    {
        names[i] = names[i].ToUpper();
    }

    ViewBag.Names = names;
    return View();
}
✅ Yahaan for loop list ke har naam ko uppercase me convert kar raha hai.
-------------------------------------------------------
📌 3️⃣ for with Model in View
Agar aapko database se aaye models ko iterate karna hai, toh for ka use hota hai.

👨‍💻 Example: for with Model in Razor View
@model List<UserModel>

<table>
    <tr>
        <th>#</th>
        <th>Name</th>
        <th>Email</th>
    </tr>
    @for (int i = 0; i < Model.Count; i++)
    {
        <tr>
            <td>@(i + 1)</td>
            <td>@Model[i].Name</td>
            <td>@Model[i].Email</td>
        </tr>
    }
</table>
✅ Model me jitne users honge, unka data ek table me dikhayega, aur index bhi show karega.
----------------------------------
📌 4️⃣ for in API Controller
Agar aapko API response me data manipulate karna hai, toh for ka use hota hai.

👨‍💻 Example: for Loop in API Controller
[HttpGet("users")]
public IActionResult GetUsers()
{
    List<UserModel> users = new List<UserModel>
    {
        new UserModel { Id = 1, Name = "Ali", Email = "ali@example.com" },
        new UserModel { Id = 2, Name = "Rahul", Email = "rahul@example.com" }
    };

    List<string> userEmails = new List<string>();

    for (int i = 0; i < users.Count; i++)
    {
        userEmails.Add(users[i].Email);
    }

    return Ok(userEmails);
}
✅ API sirf users ke emails return karegi.
--------------------------------

📌 Summary
✅ Basic for - Fixed range iteration ke liye.
✅ for in Controller - List modify karne ke liye.
✅ for with Model - Database se aaye data ko index-based iterate karne ke liye.
✅ for in API - Data manipulate karne ke liye.