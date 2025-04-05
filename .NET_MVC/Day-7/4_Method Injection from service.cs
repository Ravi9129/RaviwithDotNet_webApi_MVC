using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 4_Method Injection from service
    {
        
    }
}
----------------------------------------------
Method Injection from service ki — ye bhi Dependency Injection ka ek tareeka hai, lekin thoda specific use-case ke liye hota hai.

🔍 Method Injection from Service Kya Hota Hai?
Jab tu directly controller ke method ke andar hi koi service inject karta hai, bina constructor use kiye, toh usse Method Injection kehte hain.
--------------------------------------------------
🧠 Iska use tab hota hai jab:

Service har method me required nahi hoti

Tu dynamic ya specific scenario me service chahiye
-------------------------------------------------------
✅ Syntax – FromServices Attribute ke saath:
public IActionResult MyAction([FromServices] IMyService service)
{
    var data = service.GetData();
    return View(data);
}
👆 Yaha IMyService constructor se nahi aaya. Sirf is action method me inject hua hai.
----------------------------------------
💻 Real Example:
Step 1: Service Interface & Implementation
public interface IProductService
{
    List<string> GetProducts();
}

public class ProductService : IProductService
{
    public List<string> GetProducts()
    {
        return new List<string> { "Apple", "Banana", "Orange" };
    }
}
-----------------------------------
Step 2: Register Service in Program.cs or Startup.cs

builder.Services.AddScoped<IProductService, ProductService>();
------------------------------------------
Step 3: Controller me Method Injection

public class ProductController : Controller
{
    public IActionResult Index([FromServices] IProductService productService)
    {
        var items = productService.GetProducts();
        return View(items);
    }
}
💥 No constructor needed! Service sirf yaha use ho rahi hai.

--------------------------------------
⚠️ Caution:
Method Injection frequent use se code messy ho sakta hai.

Constructor Injection is more maintainable for large apps.
------------------------------------------
🎯 Conclusion:
Method Injection = [FromServices] attribute ke through ek service ko sirf ek action method ke andar hi inject karna.

Ye ek flexible option hai, lekin agar service commonly use hoti ho, to constructor wala tareeka zyada sahi hota hai.