using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 12_if Statement
    {
        
    }
}
-------------------------------
🔹 if Statement in ASP.NET Core (Razor Syntax)
📌 if Statement Kya Hai?
🔹 if statement ek conditional statement hai jo Razor Views aur Controllers dono jagah use hoti hai.
🔹 Ye condition check karta hai aur agar condition true ho toh specific HTML ya C# code execute hota hai.
🔹 Razor Views me @if ka use hota hai, aur Controllers me normal C# if statement hota hai.
--------------------------------------------- 
📌 1️⃣ Basic if Statement in Razor View
Agar aapko koi HTML conditionally dikhani hai, toh @if ka use karein.

👨‍💻 Example: Simple if
@{
    var isLoggedIn = true;
}

@if (isLoggedIn)
{
    <h2>Welcome, User!</h2>
}
✅ Agar isLoggedIn true hoga toh "Welcome, User!" dikhayega.
-------------------------------------------
📌 2️⃣ if-else Statement
Agar condition false ho toh doosra content dikhane ke liye else use hota hai.

👨‍💻 Example: if-else in Razor
@{
    var isLoggedIn = false;
}

@if (isLoggedIn)
{
    <h2>Welcome, User!</h2>
}
else
{
    <p>Please login first.</p>
}
✅ Agar isLoggedIn = false hai, toh "Please login first" dikhayega.
------------------------------------------------------
📌 3️⃣ if-else if-else Statement
Agar multiple conditions check karni ho toh else if ka use hota hai.

👨‍💻 Example: Multiple Conditions

@{
    var userRole = "Admin";
}

@if (userRole == "Admin")
{
    <h2>Welcome, Admin!</h2>
}
else if (userRole == "User")
{
    <h2>Welcome, User!</h2>
}
else
{
    <h2>Welcome, Guest!</h2>
}
✅ Yeh user ke role ke according message dikhayega.
--------------------------------------------------------------
📌 4️⃣ if Statement with Loops
🔹 if statement ko foreach loop ke andar bhi use kar sakte hain.

👨‍💻 Example: if inside foreach
@{
    var products = new List<string> { "Laptop", "Mobile", "Tablet" };
}

<ul>
    @foreach (var product in products)
    {
        <li>
            @product
            @if (product == "Laptop")
            {
                <strong> - Best Seller</strong>
            }
        </li>
    }
</ul>
✅ Agar product "Laptop" hoga toh uske aage "Best Seller" likh dega.
------------------------------------------------------
📌 5️⃣ if Statement in Controller
Agar aapko Controller me koi condition check karni hai, toh normal C# if statement ka use hota hai.

👨‍💻 Example: if in Controller
public IActionResult Index()
{
    bool isAdmin = true;

    if (isAdmin)
    {
        ViewBag.Message = "Welcome, Admin!";
    }
    else
    {
        ViewBag.Message = "Welcome, User!";
    }

    return View();
}
✅ ViewBag me message set ho raha hai jo View me display hoga.
------------------------------------------------------------
📌 6️⃣ if with Model in View
Agar aap Model ke data ke basis pe koi content dikhana chahte ho toh @if ka use kar sakte ho.

👨‍💻 Example: if with Model Property
@model UserModel

@if (Model.IsPremiumUser)
{
    <h2>Welcome, Premium Member!</h2>
}
else
{
    <h2>Upgrade to Premium!</h2>
}
✅ Agar user premium hai toh special message show karega.
--------------------------------------------
📌 Summary
✅ Basic if - Condition true hone par content show karega.
✅ if-else - Agar condition false ho toh alternate content show karega.
✅ if-else if-else - Multiple conditions handle karega.
✅ if with foreach - Lists ya collections ke sath use hota hai.
✅ if in Controller - Backend logic ke liye use hota hai.
✅ if with Model - Dynamic data ke sath use hota hai.