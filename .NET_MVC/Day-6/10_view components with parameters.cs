using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 10_view components with parameters
    {
        
    }
}
----------------------------------------
View Components me Parameters pass karne ki!
Jaise controller actions me parameter dete hain, waise hi tu ViewComponent ke Invoke method me bhi parameter de sakta hai.

🔥 Scenario:
Tu ek "CategoryWiseProducts" View Component bana raha hai
aur tu chah raha hai ki category name parameter ke through bheja jaye.
----------------------------------------------
✅ Step-by-Step Implementation
🔹 1. View Component Class with Parameter
public class CategoryWiseProductsViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public CategoryWiseProductsViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke(string category)
    {
        var products = _context.Products
                               .Where(p => p.Category == category)
                               .ToList();

        return View(products);
    }
}
⚠️ Method ka naam Invoke hi rehna chahiye (ya InvokeAsync if async)
----------------------------------------------
🔹 2. View (Strongly Typed)
📁 Views/Shared/Components/CategoryWiseProducts/Default.cshtml

@model List<Product>

<h4>📦 Products in this Category</h4>
<ul>
@foreach (var p in Model)
{
    <li>@p.Name - ₹@p.Price</li>
}
</ul>
------------------------------
🔹 3. Razor Page/View me Use Karna

@await Component.InvokeAsync("CategoryWiseProducts", new { category = "Electronics" })
-----------------------------------
🔍 Important Notes:
Feature	Details
Invoke() / InvokeAsync()	Accepts parameters directly
Call from View	Use Component.InvokeAsync("ComponentName", new { param = value })
Parameter Matching	Matches by name (case-insensitive)
Multiple Parameters	Tu new { param1 = val1, param2 = val2 } use kar sakta hai
--------------------------------------------------------
🧠 Real Life Use Cases:
🔍 Filter by category, tag, author, or product type

📆 Load dynamic widget: calendar, reminders, upcoming events

🛒 Cart/notifications specific to user (e.g., pass userId)

🎯 Show items by location or language
-------------------------------------------------
💡 Example with Multiple Parameters?
public IViewComponentResult Invoke(string category, int max)
{
    var result = _context.Products
                         .Where(p => p.Category == category)
                         .Take(max)
                         .ToList();

    return View(result);
}
--------------------------
Usage:

@await Component.InvokeAsync("CategoryWiseProducts", new { category = "Books", max = 3 })
