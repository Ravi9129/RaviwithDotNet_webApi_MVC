using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 8_Partial View + AJAX (Real-time Update) 
    {
        
    }
}
------------------------------------
Partial View + AJAX (Real-time Update) ek real project jaisa example leke, bina page reload ke data update hoga — full modern feel.

💡 Scenario:
Admin page pe Category select kare, toh products list real-time update ho jaye — bina page reload ke.
Matlab: Dropdown select karte hi partial view AJAX se reload ho.
--------------------------------
🧱 1️⃣ Models
🟨 Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
}
--------------------------
🟦 Category.cs
public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
}
--------------------------------------------------------
🧱 2️⃣ Controller: ProductController.cs

public class ProductController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Categories = GetCategories();
        return View();
    }

    public IActionResult GetProductsByCategory(int categoryId)
    {
        var products = GetProducts().Where(p => p.CategoryId == categoryId).ToList();
        return PartialView("_ProductListPartial", products);
    }

    private List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product{ Id = 1, Name = "Laptop", CategoryId = 1 },
            new Product{ Id = 2, Name = "Mobile", CategoryId = 1 },
            new Product{ Id = 3, Name = "Novel", CategoryId = 2 },
            new Product{ Id = 4, Name = "T-Shirt", CategoryId = 3 }
        };
    }

    private List<Category> GetCategories()
    {
        return new List<Category>
        {
            new Category{ Id = 1, Title = "Electronics" },
            new Category{ Id = 2, Title = "Books" },
            new Category{ Id = 3, Title = "Clothing" }
        };
    }
}
--------------------------------------------
🧱 3️⃣ Main View: Views/Product/Index.cshtml
@{
    ViewData["Title"] = "Product List";
}

<h2>Select Category</h2>

<select id="categorySelect" class="form-control">
    <option value="">-- Select Category --</option>
    @foreach (var cat in ViewBag.Categories)
    {
        <option value="@cat.Id">@cat.Title</option>
    }
</select>

<br/>

<div id="productList">
    @* Products will load here dynamically *@
</div>

@section Scripts {
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(function () {
            $('#categorySelect').change(function () {
                var catId = $(this).val();
                if (catId) {
                    $.get("/Product/GetProductsByCategory", { categoryId: catId }, function (data) {
                        $('#productList').html(data);
                    });
                } else {
                    $('#productList').html('');
                }
            });
        });
    </script>
}
------------------------------------------------------
🧱 4️⃣ Partial View: _ProductListPartial.cshtml
@model List<Product>

@if (Model != null && Model.Any())
{
    <ul>
        @foreach (var p in Model)
        {
            <li>@p.Name</li>
        }
    </ul>
}
else
{
    <p>No products found in this category.</p>
}
✅ Output:
Dropdown se category select karo.

Page reload nahi hoga.

Niche list automatic update hogi with AJAX.

Reusable _ProductListPartial future me kahi bhi use ho sakti hai.
---------------------------------------------------------
⚡ Real-World Use Cases:
Country -> State -> City dropdowns

User role change -> Show dynamic permissions

Cart update without page reload

Live form sections based on selection