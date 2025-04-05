using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 8_view component with view data
    {
        
    }
}
---------------------------------
View Component ke andar ViewData ka use kaise karte hain.
ViewData normally controller aur view ke beech hota hai, lekin ViewComponent ke case me thoda alag hota hai — iska apna ViewData hota hai.

💡 Scenario:
Soch ek "Top Products" View Component bana raha hai tu, jisme tu ViewData["Title"] = "Top Selling Products" bhejna chahta hai taaki wo view me use ho sake.
-----------------------------------------
✅ Step-by-Step Implementation
🔹 1. View Component Class
public class TopProductsViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public TopProductsViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var products = _context.Products
                               .OrderByDescending(p => p.Sales)
                               .Take(5)
                               .ToList();

        // Setting ViewData here
        ViewData["Title"] = "🔥 Top Selling Products";

        return View(products);
    }
}
--------------------------------------
🔹 2. View Component View
@model List<Product>
@{
    var title = ViewData["Title"] as string;
}

<div class="top-products-widget">
    <h4>@title</h4>

    <ul>
        @foreach (var p in Model)
        {
            <li>@p.Name - ₹@p.Price</li>
        }
    </ul>
</div>
--------------------------------------
🔹 3. Use It in Any Razor View
@await Component.InvokeAsync("TopProducts")
-------------------------------------
🔥 Important Points:
Feature	Description
ViewData["key"]	Works inside ViewComponent class, for passing data to its view
Scope	ViewData in ViewComponent is local (doesn’t mix with controller's ViewData)
Use Case	Set custom headings, flags, status text, or mode ("edit"/"view") dynamically
---------------------------------------
❗️Note:
Tu controller ke ViewData ko ViewComponent me directly access nahi kar sakta.

But tu ViewComponent ke ViewData me apni values set kar sakta hai jo uske view me accessible hoti hain.
----------------------------------
✅ Jab Use Karein?
Jab tu ViewComponent ke view me koi dynamic text, title, color, mode, message pass karna chahta hai.

Model ke alawa chhoti chhoti values bhejne ke liye.

