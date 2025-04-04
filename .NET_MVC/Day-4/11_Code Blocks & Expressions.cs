using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 11_Code Blocks & Expressions
    {
        
    }
}
-----------------------------------------
🔹 Code Blocks & Expressions in ASP.NET Core Views (Razor Syntax)
📌 Razor Syntax Kya Hai?
ASP.NET Core Views me Razor Syntax ka use hota hai jo C# code aur HTML ko combine karta hai.
🔹 Code Blocks (@{}) - C# logic likhne ke liye.
🔹 Inline Expressions (@Expression) - Directly value display karne ke liye.
🔹 Control Statements (if, for, foreach) - HTML me loops aur conditions lagane ke liye.
---------------------------------------------------
📌 1️⃣ Code Blocks (@{})
🔹 Razor me agar aapko multiple lines ka C# code likhna hai, toh aap @{} ka use kar sakte ho.
--------------------------
👨‍💻 Example: Code Block in View
@{
    var message = "Welcome to Razor Syntax!";
    var currentTime = DateTime.Now;
}

<h2>@message</h2>
<p>Current Time: @currentTime</p>
✅ Yeh Razor block ek message aur current time render karega.
------------------------------------------------------------
📌 2️⃣ Inline Expressions (@Expression)
🔹 Agar aap single C# statement ka output show karna chahte ho, toh @Expression ka use hota hai.

👨‍💻 Example: Inline Expression
<h2>Current Year: @DateTime.Now.Year</h2>
✅ Isme DateTime.Now.Year execute hoke current year display hoga.
------------------------------------------
📌 3️⃣ Control Statements in Razor (If, For, Foreach)
👨‍💻 If-Else Statement
@{
    var isLoggedIn = true;
}

@if (isLoggedIn)
{
    <p>Welcome, User!</p>
}
else
{
    <p>Please login first.</p>
}
✅ Yeh check karega ki user logged in hai ya nahi.
--------------------------------
👨‍💻 Loop (for, foreach)
@for (int i = 1; i <= 5; i++)
{
    <p>Item @i</p>
}
✅ Yeh 5 baar "Item" print karega.
------------------------------------
👨‍💻 Foreach Loop with Model Data
@model List<string>

<ul>
    @foreach (var item in Model)
    {
        <li>@item</li>
    }
</ul>
✅ Yeh Model se data fetch karke HTML list render karega.
------------------------------------------------------
📌 4️⃣ Using HTML inside Razor Code
🔹 Agar aapko if ya foreach ke andar pure HTML tags likhne hain toh Razor syntax automatically detect kar lega.

👨‍💻 Example: HTML in Razor Code
@{
    var isAdmin = true;
}

@if (isAdmin)
{
    <h2>Welcome Admin!</h2>
    <button>Manage Users</button>
}
✅ Yeh admin ke liye welcome message aur button render karega.
----------------------------------------------------
📌 5️⃣ Escaping Razor Code (@@)
🔹 Agar aapko Razor ke @ symbol ko HTML me as a normal text dikhana hai, toh @@ ka use hota hai.
----------------------------------------
👨‍💻 Example: Escape @ Symbol
<p>Email us at: support@@example.com</p>
✅ Yeh output me support@example.com dikhayega, Razor confuse nahi hoga.
---------------------------------------------------------
📌 Summary
✅ Code Blocks (@{}) - Multiple C# lines likhne ke liye.
✅ Inline Expressions (@Expression) - Single line values display karne ke liye.
✅ If-Else aur Loops (for, foreach) - Dynamic content generate karne ke liye.
✅ Escaping Razor (@@) - @ symbol ko as text use karne ke liye.