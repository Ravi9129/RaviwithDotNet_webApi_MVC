using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 9_MVC Architecture Pattern
    {
        
    }
}
---------------------------------
🔹 MVC Architecture Pattern in ASP.NET Core
📌 Kya Hai MVC?
MVC (Model-View-Controller) ek architectural pattern hai jo application ko 3 major parts me divide karta hai:

Model - Data aur business logic handle karta hai.

View - UI (User Interface) render karta hai.

Controller - Client se requests leta hai, process karta hai aur response bhejta hai.
---------------------------------------------------------------
✅ Use Kab Karte Hain?

Scalable aur Maintainable applications banane ke liye.

Separation of Concerns (SoC) maintain karne ke liye.

Testability improve karne ke liye.

🔹 MVC Flow (Kaise Kaam Karta Hai?)
1️⃣ User request bhejta hai (URL ya action call karta hai).
2️⃣ Controller request process karta hai aur Model se data fetch karta hai.
3️⃣ Model se data retrieve hota hai aur Controller ko milta hai.
4️⃣ Controller View ko data bhejta hai aur View render karta hai.
5️⃣ Response User ko dikhaya jata hai.
--------------------------------------------------------------
🛠 Example: MVC Application in ASP.NET Core
1️⃣ Model (Data aur Business Logic)
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
----------------------------------------------
2️⃣ Controller (Logic Handle Karna)
public class ProductController : Controller
{
    public IActionResult Index()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 50000 },
            new Product { Id = 2, Name = "Mobile", Price = 20000 }
        };

        return View(products);  // Data View ko pass karna
    }
}
---------------------------------------
3️⃣ View (UI Render Karna)
@model List<Product>

<h2>Product List</h2>
<ul>
    @foreach(var product in Model)
    {
        <li>@product.Name - ₹@product.Price</li>
    }
</ul>
--------------------------------------------
📌 Summary (MVC Architecture Ka Benefit)
✅ Scalability - Large projects ke liye suitable hai.
✅ Maintainability - Code alag-alag layers me organized hota hai.
✅ Reusability - Models aur Views easily reuse ho sakte hain.
✅ Testability - Unit Testing easy hoti hai.