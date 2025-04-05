using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 6_PartialViewResult
    {
        
    }
}
---------------------------------------
PartialViewResult ke baare mein — ye ek special type ka result hota hai jo ASP.NET Core me sirf ek partial view render karta hai, bina full layout ke.

🔥 Real Life Scenario:
Soch tu ek Product List page pe ho, jahan tu filter apply karta hai (category, price, rating) — aur result poora page reload na ho, sirf ek part (product list) update ho.

Yahan pe controller AJAX request ka response me PartialViewResult return karega — taaki sirf product list hi re-render ho.
----------------------------------------------------------
✅ Syntax:
🔹 Basic Return in Controller:
public IActionResult ProductCard(int id)
{
    var product = _context.Products.Find(id);
    return PartialView("_ProductCard", product);
}
💡 Use Case: AJAX + PartialViewResult
--------------------------------------------------
🔹 View (Razor) Code:
<div id="productList">
    @await Html.PartialAsync("_ProductList", Model)
</div>

<script>
    function filterProducts(categoryId) {
        $.get("/Product/FilterByCategory/" + categoryId, function(result) {
            $("#productList").html(result);
        });
    }
</script>
----------------------------------------------
🔹 Controller Code:
public IActionResult FilterByCategory(int categoryId)
{
    var filteredProducts = _context.Products
                                   .Where(p => p.CategoryId == categoryId)
                                   .ToList();

    return PartialView("_ProductList", filteredProducts);
}
--------------------------------------------------
🤔 Why Use PartialViewResult?
Situation     	Why Use It
AJAX requests    	Only return partial HTML
Dynamic UI update    	No full page refresh
Nested rendering	   Reuse small components
Fast loading	    Reduce payload & layout processing
--------------------------------------------
⚠️ Important Notes:
return PartialView() internally returns PartialViewResult

You can explicitly write return new PartialViewResult() too, but usually not needed

Layout automatically excluded unless manually added
--------------------------------------------------
🔹 Explicit Return (not often needed):
return new PartialViewResult
{
    ViewName = "_ProductCard",
    ViewData = new ViewDataDictionary<Product>(ViewData, product)
};
------------------------------------------------------
✅ Summary
PartialViewResult returns only a Razor partial

Mostly used in AJAX scenarios, or when rendering components dynamically

Better for reusability, performance, and separation of logic