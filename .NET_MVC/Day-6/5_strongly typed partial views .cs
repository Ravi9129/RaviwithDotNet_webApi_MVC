using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 5_strongly typed partial views 
    {
        
    }
}
---------------------------------------------------
strongly typed partial views ka scene — aur ye toh developer ka asli weapon hota hai. Jab tu Partial View me specific model ka structure use karta hai, validation chahiye hoti hai, ya form bharwana hota hai — tab ye kaam aata hai.

🔥 Real Life Scenario:
Soch le tu ek product detail page bana raha hai:

Usme ek Product Info Card dikhana hai (Name, Price, In Stock)

Ye card multiple views me reuse hoga (Home, ProductList, ProductDetail)

Tu chahta hai model ke through data aaye — bina ViewData, ViewBag ke jhanjhat ke
---------------------------------------------------------
✅ Step-by-Step Example
🔹 Step 1: Model Class
// Models/Product.cs
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}
------------------------------------------
🔹 Step 2: Create Strongly-Typed Partial View
<!-- Views/Shared/_ProductCard.cshtml -->
@model MyApp.Models.Product

<div class="card">
    <h4>@Model.Name</h4>
    <p>Price: ₹@Model.Price</p>
    <p>Status: @(Model.InStock ? "Available" : "Out of Stock")</p>
</div>
--------------------------------------
🔹 Step 3: Use it in Parent View
@model MyApp.Models.Product

<h2>Product Page</h2>

@await Html.PartialAsync("_ProductCard", Model)
----------------------------------------------------
Agar tu model list pass kar raha ho:

@model List<MyApp.Models.Product>

@foreach (var item in Model)
{
    @await Html.PartialAsync("_ProductCard", item)
}
----------------------------------------
⚡ Why Strongly Typed Partial View is Best?
Benefits	Explanation
✅ Compile-time checking	IntelliSense aur error detection
✅ Cleaner code	No ViewBag/ViewData clutter
✅ Reusable blocks	Just pass model and reuse
✅ Form binding possible	Full model validation with forms
---------------------------------------------
💡 Tips:
Partial Views ko usually Views/Shared folder me rakh

Model mismatch karega toh runtime error aayega — always pass correct type

Tu @model define kare bina Model.Property use nahi kar sakta
----------------------------------------
❌ Common Mistakes:
Model type pass nahi kara aur partial me access kiya → runtime crash

List ka item pass nahi kara, list poori bhej di ek item view me