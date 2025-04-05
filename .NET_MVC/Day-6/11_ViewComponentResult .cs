using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 11_ViewComponentResult 
    {
        
    }
}
--------------------------------
ViewComponentResult kya hota hai, aur kab use hota hai — chalo apni bhasha me deep samjhte hain 👇

🔷 ViewComponentResult Kya Hai?
Ye ek return type hai jo ViewComponent se view render karne ke liye use hota hai.

Jab tu ViewComponent banata hai, tu return View(...) karta hai na?
Woh ViewComponentResult return karta hai internally — jaise Controller me ViewResult hota hai, waise hi yeh hai ViewComponent ke liye.

🔧 Syntax:
public IViewComponentResult Invoke()
{
    // return view with model
    return View(model);
}
Ye View(...) method internally ViewComponentResult return karta hai.
----------------------------------------------
Agar tu chaahe toh direct bhi return kar sakta hai:

return new ViewViewComponentResult
{
    ViewName = "Default",
    ViewData = new ViewDataDictionary<List<Product>>(ViewData, productList)
};
Par zyadaatar log return View(model); hi use karte hain — clean & readable.
----------------------------------------------
🧠 Real Life Example:
public class CartSummaryViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartSummaryViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IViewComponentResult Invoke()
    {
        var cartItems = _cartService.GetCartItems();
        return View(cartItems);  // 👈 This returns ViewComponentResult
    }
}
View: Views/Shared/Components/CartSummary/Default.cshtml

✅ ViewComponentResult Return Karne ke Fayde:
💡 ViewComponent me view ko render karta hai

💥 Strongly typed model ke saath use kar sakta hai

🔐 Partial rendering ka clean tareeka deta hai

⚙️ Ajax ya server-side rendering dono me kaam karta hai

🧪 Tera Custom Result?
--------------------
Tu chaahe to dusra result bhi return kar sakta hai:
--------------------------------------
❗ ContentResult (e.g., plain string):
public IViewComponentResult Invoke()
{
    return Content("Hello from View Component!");
}
----------------------
❗ JsonResult:
public IViewComponentResult Invoke()
{
    var data = new { Name = "Test", Value = 123 };
    return new JsonViewComponentResult(data);
}
But note: ViewComponentResult is mainly for returning views — dusre use cases ke liye alag result classes hoti hain.
--------------------------------------

