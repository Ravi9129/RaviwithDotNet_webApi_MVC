using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 7_ViewModel + Partial Views
    {
        
    }
}
-----------------------------------------
ViewModel + Partial Views" ka combo real-life enterprise projects me modular, 
reusable form pages banane ke liye hota hai — jaise admin panel, user form, product pages, etc.

Chal, step-by-step samjhta hoon ek real-world example ke through.

💡 Scenario:
Admin ek Product create kar raha hai jisme:

🔸 Product Info (Name, Price)

🔸 Category Info (dropdown)

🔸 Image Upload (optional part)

Isko hum tod denge:

✅ ViewModel me combine karenge

✅ Partial Views use karenge for sub-form reuse
--------------------------------------
🧱 1️⃣ Models
🔸 Product.cs
public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(1, 100000)]
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
}
-------------------
🔸 Category.cs
public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
}
----------------------------------------------
🧱 2️⃣ ViewModel: ProductViewModel.cs

public class ProductViewModel
{
    public Product Product { get; set; }

    public List<Category> Categories { get; set; }
}
------------------------------------
🧱 3️⃣ Controller: ProductController.cs

public class ProductController : Controller
{
    public IActionResult Create()
    {
        var vm = new ProductViewModel
        {
            Product = new Product(),
            Categories = GetCategories()
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult Create(ProductViewModel vm)
    {
        if (ModelState.IsValid)
        {
            // Yaha product save hoga
            return RedirectToAction("Success");
        }

        vm.Categories = GetCategories(); // error case me dubara set
        return View(vm);
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
---------------------------------------------
🧱 4️⃣ Main View: Views/Product/Create.cshtml
@model ProductViewModel

<h2>Create Product</h2>

<form asp-action="Create" method="post">
    @* Partial for Product Info *@
    @Html.Partial("_ProductForm", Model)

    <button type="submit">Save</button>
</form>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
🧱
----------------------------------------------

 5️⃣ Partial View: _ProductForm.cshtml

@model ProductViewModel

<div>
    <label>Name</label>
    <input asp-for="Product.Name" />
    <span asp-validation-for="Product.Name" class="text-danger"></span>
</div>

<div>
    <label>Price</label>
    <input asp-for="Product.Price" />
    <span asp-validation-for="Product.Price" class="text-danger"></span>
</div>

<div>
    <label>Category</label>
    <select asp-for="Product.CategoryId" asp-items="@(new SelectList(Model.Categories, "Id", "Title"))">
        <option value="">-- Select Category --</option>
    </select>
    <span asp-validation-for="Product.CategoryId" class="text-danger"></span>
</div>
-------------------------------
✅ Real Benefits:
✅ Advantage	🚀 Explanation
Reusability	_ProductForm.cshtml ko Edit view me bhi use kar sakte ho
Clean Code	View chhota aur readable hota hai
Maintainability	Alag views me change karna easy hota hai
Testability	Har form part modular banta hai
------------------------------------------
📦 Where to Use:
Admin Forms (Product, Employee, Article)

Multi-step Forms

Repeatable components (Address, Social Links, Profile Info)
