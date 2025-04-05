using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 10_AddScoped()
    {
        
    }
}
-------------------------------------
AddScoped() ke baare mein — yeh bhi Dependency Injection ka ek important part hai ASP.NET Core mein.

🔍 AddScoped() kya karta hai?
AddScoped() ek service ka ek hi instance banata hai per HTTP request.
Agar controller, service ya component ke andar multiple jagah inject karo — same request ke andar to same instance milega.
Lekin next request me naya instance banega.
---------------------------------------
✅ Kab use karte hain?
Jab service request-level data ke saath kaam kar rahi ho

Jab DBContext ya Unit of Work pattern use ho raha ho

Jab state ek request ke liye shared hona chahiye, but globally nahi

🧠 Ek Line Me:
Ek HTTP request ke liye ek instance. Har nayi request pe naya instance.

💻 Example: Scoped Service
Step 1: Interface & Implementation
public interface IGuidService
{
    string GetGuid();
}

public class ScopedGuidService : IGuidService
{
    private string _guid = Guid.NewGuid().ToString();

    public string GetGuid()
    {
        return _guid;
    }
}
----------------------------------
Step 2: Register in Program.cs

builder.Services.AddScoped<IGuidService, ScopedGuidService>();
--------------------------------------
Step 3: Use in Controller
public class HomeController : Controller
{
    private readonly IGuidService _service1;
    private readonly IGuidService _service2;

    public HomeController(IGuidService service1, IGuidService service2)
    {
        _service1 = service1;
        _service2 = service2;
    }

    public IActionResult Index()
    {
        ViewBag.Guid1 = _service1.GetGuid();
        ViewBag.Guid2 = _service2.GetGuid();
        return View();
    }
}
--------------------------
🔍 Output (Same Request):
Guid1: xyz-123
Guid2: xyz-123 ✅ (same request = same instance)
-------------------------------
🔁 Next Request:
Guid1: abc-789
Guid2: abc-789 ✅ (naya request = naya instance)
⚠️ Kab na use karein?
Jab state globally share karni ho (tab Singleton)

Jab service lightweight aur stateless ho (tab Transient)
------------------------------------
🛠 Real Use Cases
EF Core DbContext (best example!)

Request-scoped services: validation tracker, per-user logger, etc.

Services jo request ke lifecycle tak zinda rehni chahiye
-------------------------------------
🔥 Summary:
🔸 Lifetime	🔹 Scoped
Lifetime	One per HTTP request
Same request	✅ Same instance
New request	✅ New instance
Best for	DBContext, Unit of Work