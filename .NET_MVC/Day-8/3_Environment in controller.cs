using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 3_Environment in controller
    {
        
    }
}
---------------------------
 Environment ko Controller ke andar kaise access karte hain aur real-world mein kab, kyu, aur kaise use hota hai.

🧠 Use-case:
Tu chah raha hai ki controller ke andar environment ke base pe kuch logic change ho.

Jaise: Development mein dummy data bhejna aur Production mein real database data dena.

Ya fir logs/debug messages sirf dev mode mein dikhana.
------------------------------------------
✅ Step 1: IHostEnvironment ko inject karna

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

public class HomeController : Controller
{
    private readonly IHostEnvironment _env;

    public HomeController(IHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult Index()
    {
        if (_env.IsDevelopment())
        {
            return Content("App is running in Development Environment");
        }
        else if (_env.IsProduction())
        {
            return Content("App is running in Production Environment");
        }
        else if (_env.IsStaging())
        {
            return Content("App is running in Staging Environment");
        }

        return Content("Unknown Environment");
    }
}
-------------------------------------------------------
✅ Alternate Interface: IWebHostEnvironment
Dono (IHostEnvironment or IWebHostEnvironment) kaam karte hain, but IWebHostEnvironment web-specific properties jaise WebRootPath bhi deta hai.

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _env;

    public HomeController(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult EnvDetails()
    {
        string rootPath = _env.WebRootPath;
        string contentRoot = _env.ContentRootPath;
        string currentEnv = _env.EnvironmentName;

        return Content($"Environment: {currentEnv}\nWebRoot: {rootPath}\nContentRoot: {contentRoot}");
    }
}
-------------------------------------------------------
🔍 Real-World Example:
❓ Problem:
Tu ek API bana raha hai jisme Development mein mock data bhejna hai, aur Production mein database se data lena hai.

✅ Solution:
public IActionResult GetProducts()
{
    if (_env.IsDevelopment())
    {
        // Dummy product list
        var products = new[] {
            new { Id = 1, Name = "Dev Product 1" },
            new { Id = 2, Name = "Dev Product 2" }
        };
        return Json(products);
    }

    // Real data from DB (assume _db injected)
    var realProducts = _db.Products.ToList();
    return Json(realProducts);
}

---------------------------------------
🧾 Summary:
Tu IHostEnvironment ya IWebHostEnvironment ko controller ke constructor mein inject kare.

.IsDevelopment(), .IsProduction(), .EnvironmentName se environment detect kare.

Ye pattern har controller/action mein reusable hai.