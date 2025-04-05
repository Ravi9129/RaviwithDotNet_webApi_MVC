using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 7_View Component
    {
        
    }
}
----------------------------------------
View Component ki — ye ASP.NET Core ka ek powerful feature hai jo Partial View se bhi zyada modular, reusable, testable hota hai.

💡 View Component kya hota hai?
View Component ek chhota component hota hai jo view ke andar chhota logic aur UI render karta hai. Ye controller se independent hota hai aur use kiya jata hai modular views banane ke liye.

🔥 Real Life Scenario:
Tu soch le ek eCommerce site hai. Har page me tu chhota "Cart Summary", ya "Top Categories" ka widget dikhata hai.

Ye data DB se aata hai

Har page pe chahiye

Lekin controller repeat nahi karna

Toh ViewComponent use karega
-------------------------------------------------------------
✅ Basic Structure of View Component
🔹 1. Create a View Component Class
// ViewComponents/TopCategoriesViewComponent.cs
public class TopCategoriesViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public TopCategoriesViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _context.Categories.Take(5).ToList();
        return View(categories);
    }
}
-----------------------------------------------------
🔹 2. Create View for it
<!-- Views/Shared/Components/TopCategories/Default.cshtml -->
@model List<Category>

<div class="category-widget">
    <h5>Top Categories</h5>
    <ul>
        @foreach (var cat in Model)
        {
            <li>@cat.Name</li>
        }
    </ul>
</div>
---------------------------------------------------
🔹 3. Use in Any View (Razor Page or MVC View)
@await Component.InvokeAsync("TopCategories")
🎯 Note: Class name TopCategoriesViewComponent → use "TopCategories"

--------------------------------------------
⚠️ Important Points:
Views folder path: Views/Shared/Components/{ComponentName}/Default.cshtml

Method can be Invoke() (sync) or InvokeAsync() (async)

Use for widgets, menus, summaries, etc.
---------------------------------------------------------
✅ Use Cases:
Shopping Cart Summary

User Profile Card

Notification Count

Trending Blog Posts

Footer/Sidebar Content
------------------------------------------------------
🔥 Bonus: With Parameters
public IViewComponentResult Invoke(int count)
{
    var topProducts = _context.Products.OrderByDescending(p => p.Sales).Take(count).ToList();
    return View(topProducts);
}
----------------------------------------
Use in Razor:

@await Component.InvokeAsync("TopProducts", new { count = 3 })