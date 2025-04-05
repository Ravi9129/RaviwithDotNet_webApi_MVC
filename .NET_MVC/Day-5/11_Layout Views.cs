using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 11_Layout Views
    {
        
    }
}
---------------------------------
Layout Views pe — aur ye Razor Views ka asli "template engine" power hai 💥
Agar tu chhota project bhi bana raha ho ASP.NET Core MVC me, layout ke bina sab bikhar jaata hai.

🤔 Layout View kya hota hai?
Layout View ek master template hota hai jo common HTML structure hold karta hai — jaise header, footer, sidebar, scripts, etc.

Tere har ek page (view) us layout ko inherit karta hai — bas apna dynamic content inject karta hai.

📂 Folder Structure:
By default layout file hoti hai:

/Views/Shared/_Layout.cshtml
--------------------------
🧱 Example of Layout – _Layout.cshtml
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"] - MyApp</title>
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <header>
        @await Html.PartialAsync("_Header")
    </header>

    <main role="main" class="container">
        @RenderBody()
    </main>

    <footer>
        <p>&copy; 2025 - MyApp</p>
    </footer>

    @RenderSection("Scripts", required: false)
----------------------------------------------
📌 Important Parts Explained:
Razor Function	Explanation
@RenderBody()	Yahan child view ka content inject hota hai
@RenderSection("Scripts")	Optional: view se specific JS section push karne ke liye
@ViewData["Title"]	Dynamic title set karne ke liye har view se
🔗 View ko Layout se Link Kaise Karte Hain?
Uske liye ek aur file hoti hai:
----------------------
_ViewStart.cshtml
Ye file har view ko batata hai ki kaunsa layout use karna hai

@{
    Layout = "_Layout";
}
------------------------------------------
Ye file rakhte hain:

/Views/_ViewStart.cshtml
-------------------------
🧠 Real World Scenario:
Tu ek eCommerce app bana raha hai jisme:

Sab page me top bar, cart icon, footer common hai

Har page me bas content alag hai

Toh kya kare?
_Layout.cshtml me header/footer bana

@RenderBody() me alag-alag page ka view inject ho jaayega

📌 Dynamic Title Set Karna
-------------------------------------
In layout:
<title>@ViewData["Title"]</title>
--------------------------------------
In view:
@{
    ViewData["Title"] = "Home Page";
}
Bonus: JS & CSS Section Add Karna
-----------------------------------
In layout:
@RenderSection("Scripts", required: false)
-------------------------------------------
In view:
@section Scripts {
    <script src="~/js/custom.js"></script>
}
-------------------------------------
😎 Summary:
Layout = "Har page ke liye ek fixed frame"
View = "Us frame ke andar ka changing content"

Time bachata hai

Maintainability badhata hai

Design ek jaisa rehta hai har view me