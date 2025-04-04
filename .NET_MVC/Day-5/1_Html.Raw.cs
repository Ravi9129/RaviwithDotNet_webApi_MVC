using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 1_Html.Raw
    {
        
    }
}
-------------------------------------------
🔹 Html.Raw in ASP.NET Core MVC
📌 Html.Raw Kya Hai?
🔹 Html.Raw ASP.NET Core MVC me ek helper method hai jo HTML encoding ko bypass karne ke liye use hoti hai.
🔹 Iska main kaam HTML content ko as-it-is render karna hai bina usko escape kiye.
🔹 Agar aap @Html.Raw("<b>Hello</b>") likho to yeh <b>Hello</b> ko bold text me dikhayega na ki as a string.
-------------------------------------------------------
📌 1️⃣ Without Html.Raw (Auto Encoding)
@{
    var message = "<b>Hello, World!</b>";
}
<p>@message</p>
🛑 Output:
<b>Hello, World!</b>
✅ Yeh <b> tag as a text show karega, bold nahi karega, kyunki ASP.NET Core MVC automatically encoding karta hai.
--------------------------------------------------------------------
📌 2️⃣ Using Html.Raw (Bypass Encoding)
@{
    var message = "<b>Hello, World!</b>";
}
<p>@Html.Raw(message)</p>
✅ Output:
👉 Hello, World! (Bold me dikhai dega)
✅ Ab <b> tag properly execute hoga aur text bold me show hoga.
----------------------------------------------------
📌 3️⃣ Html.Raw with Dynamic Content
Agar aap database se HTML formatted content fetch kar rahe ho, toh Html.Raw use kar sakte ho.

@{
    string contentFromDB = "<p style='color:red;'>Warning: Unauthorized Access!</p>";
}
@Html.Raw(contentFromDB)
✅ Output:
🔴 Warning: Unauthorized Access! (Red color me dikhai dega)
---------------------------------------------------------------
📌 4️⃣ Html.Raw in Loop (Multiple Values Render Karna)
Agar multiple items render karni ho, toh bhi Html.Raw ka use ho sakta hai.

@{
    List<string> messages = new List<string>
    {
        "<b>First Message</b>",
        "<i>Second Message</i>",
        "<u>Third Message</u>"
    };
}

@foreach (var msg in messages)
{
    <p>@Html.Raw(msg)</p>
}
✅ Output:
👉 First Message (Bold)
👉 Second Message (Italic)
👉 Third Message (Underlined)
--------------------------------------------
📌 5️⃣ Html.Raw in Razor View Components
Agar aap ViewComponent ya Partial View me Html.Raw use karna chahte ho, toh aise likh sakte ho:

@model string

@Html.Raw(Model)
✅ Yahan Model ek HTML formatted string hai jo as-it-is render hogi.
-------------------------------------------------------------------
📌 7️⃣ Html.Raw with Security Concerns (XSS Attack)
🚨 ⚠️ Warning:
Agar aap User Input ya Database se aayi hui values bina validate kiye Html.Raw me daaloge, toh XSS (Cross-Site Scripting) Attack ka risk badh jata hai!

🛑 XSS Attack Example
Agar koi user <script>alert('Hacked!');</script> likh de aur Html.Raw use ho raha ho, toh browser isko execute kar dega!

✅ Secure Way:
----------
@{
    string userInput = "<script>alert('Hacked!');</script>";
}
@Html.Encode(userInput)  // Safe way
🔒 Html.Encode user input ko encode kar dega, jisse script run nahi hoga.
----------------------------------------------
📌 Summary
✅ Html.Raw HTML Encoding ko bypass karta hai aur HTML content ko as-it-is render karta hai.
✅ Jab HTML formatted content (Database ya CMS se) dikhana ho, tab useful hota hai.
✅ Loop aur Dynamic Content me bhi helpful hai.
✅ Security Issue: Html.Raw XSS Attack ke liye vulnerable ho sakta hai, toh user input validate karna zaroori hai.