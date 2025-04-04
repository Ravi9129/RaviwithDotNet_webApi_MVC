using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 10_Views
    {
        
    }
}
---------------------------------
🔹 Views in ASP.NET Core MVC
📌 Views Kya Hai?
🔹 Views ek UI (User Interface) component hai jo data ko HTML ke form me render karta hai.
🔹 Ye Model aur Controller ke data ko accept karta hai aur user ke liye display karta hai.
🔹 Views C# + Razor syntax ka use karti hain taaki dynamic content generate ho sake.

✅ Use Kab Karte Hain?

Jab HTML, CSS, Bootstrap, JavaScript ka use karke UI design karna ho.

Jab dynamic content (jaise database se fetched data) dikhana ho.

Jab Controller se aane wale data ko format karna ho.
---------------------------------------------------
📌 Types of Views
1️⃣ Normal Views (.cshtml) - Normal HTML pages jo Controller se data receive karti hain.
2️⃣ Partial Views - Reusable components jo kisi aur View me include kiye ja sakte hain.
3️⃣ Layout Views - Common UI structure ko maintain karne ke liye (header, footer, sidebar).
4️⃣ View Components - Dynamic UI elements jo controllers se independent hote hain.
5️⃣ Razor Views - Views jo Razor Syntax ka use karke dynamic content render karti hain.
--------------------------------------
🛠 Example: Basic View (.cshtml File)
@model List<Product>

<h2>Product List</h2>
<ul>
    @foreach(var product in Model)
    {
        <li>@product.Name - ₹@product.Price</li>
    }
</ul>
✅ Yeh View Controller se Product List accept karega aur dynamically data show karega.
-------------------------------------------------------------------
📌 Layout Views (_Layout.cshtml)
🔹 Layout views ek common UI template provide karti hain.
🔹 Jaise header, footer, navbar, jo har page me same rahenge.

👨‍💻 Example: _Layout.cshtml (Common Layout)
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"] - My Website</title>
</head>
<body>
    <header>
        <h1>My Website</h1>
    </header>
    <div>
        @RenderBody()  <!-- Yahan Child View Inject Hogi -->
    </div>
    <footer>
        <p>© 2025 - My Website</p>
    </footer>
</body>
</html>
✅ Sabhi Views _Layout.cshtml ka use karke common structure maintain kar sakti hain.
--------------------------------------------------
📌 Partial Views (_PartialView.cshtml)
🔹 Agar ek component multiple views me use karna ho toh Partial Views ka use hota hai.

👨‍💻 Example: _ProductCard.cshtml (Partial View)
@model Product

<div class="card">
    <h3>@Model.Name</h3>
    <p>Price: ₹@Model.Price</p>
</div>
-------------------------------------------------
👨‍💻 Use in Main View (Index.cshtml)
@foreach (var product in Model)
{
    @await Html.PartialAsync("_ProductCard", product)
}
✅ Yeh Partial View dynamically multiple products render karega!

📌 Strongly Typed Views
🔹 Views me Model pass karna optional hai, lekin agar Model define karte hain toh usko "Strongly Typed View" bolte hain.
-----------------------------------------------
👨‍💻 Example: Strongly Typed View
@model Product

<h2>@Model.Name</h2>
<p>Price: ₹@Model.Price</p>
✅ Isse compile-time validation milega aur errors kam honge!
--------------------------------------------------
📌 Summary
✅ Views HTML ke through dynamic data render karti hain.
✅ Normal Views, Partial Views, Layout Views aur View Components available hain.
✅ Razor Syntax ka use hota hai taaki C# code aur HTML combine ho sake.
✅ Strongly Typed Views Model se data accept karti hain.
✅ Layout Views se common structure maintain hota hai.