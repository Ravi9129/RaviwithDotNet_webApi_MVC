using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 2_[Bind] vs [BindNever]
    {
        
    }
}
----------------------------------------------
🔹 [Bind] vs [BindNever] in ASP.NET Core Model Binding
ASP.NET Core me Model Binding automatically request data (Form, Query, Route, Body) ko model properties me map karta hai. Lekin kabhi kabhi hume kuch properties ko bind karna ya restrict karna hota hai.

Iske liye [Bind] aur [BindNever] attributes ka use hota hai.

📌 1. [Bind] Attribute
✅ Sirf specified properties ko bind karta hai, baki sab ignore ho jati hain.
✅ Agar model me kuch hi properties ko accept karna hai toh ye useful hai.
----------------------------------------------------------
👨‍💻 Example: Sirf Name aur Email bind ho, Role bind na ho

public class UserModel
{
    [Bind("Name,Email")]
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } // Ye bind nahi hoga
}
----------------------------------------------------
🚀 API Controller
[HttpPost]
public IActionResult CreateUser([FromForm] UserModel user)
{
    return Ok(user);
}
--------------------------------------------
🎯 Result:
Agar request me Role ka value pass karein toh bhi ignore hoga.

{
    "name": "John Doe",
    "email": "john@example.com",
    "role": "Admin"
}
----------------------------------------------
📌 Response:

{
    "name": "John Doe",
    "email": "john@example.com",
    "role": null  // Role ignore ho gaya
}
-------------------------------------------
📌 2. [BindNever] Attribute
✅ Ye kisi property ko model binding se completely exclude karta hai.
✅ Sensitive properties (jaise IsAdmin, PasswordHash, UserId) ko bind hone se rokta hai.
✅ Security ke liye useful hai, taaki users sensitive properties manipulate na kar sakein.
----------------------------------------------------------------------
👨‍💻 Example: IsAdmin bind nahi hoga
public class UserModel
{
    public string Name { get; set; }
    public string Email { get; set; }

    [BindNever]
    public bool IsAdmin { get; set; } // Ye bind nahi hoga
}
------------------------------------------
🚀 API Controller
[HttpPost]
public IActionResult CreateUser([FromForm] UserModel user)
{
    return Ok(user);
}
----------------------------------------------
🎯 Result:
Agar request me IsAdmin = true bheja gaya, toh bhi wo ignore ho jayega.
{
    "name": "Alice",
    "email": "alice@example.com",
    "isAdmin": true
}
--------------------------------------
📌 Response:

{
    "name": "Alice",
    "email": "alice@example.com",
    "isAdmin": false  // Default value hi rahegi
}
---------------------------------------------------------
🎯 Final Thoughts
✅ [Bind] tab use karo jab sirf kuch properties ko allow karna ho.
✅ [BindNever] tab use karo jab kisi property ko request data se completely exclude karna ho (Security reasons).