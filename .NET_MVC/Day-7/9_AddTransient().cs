using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 9_AddTransient()
    {
        
    }
}
-------------------------------------
AddTransient() ke baare mein — yeh ASP.NET Core Dependency Injection ka ek important method hai jo transient lifetime set karta hai.

🔍 AddTransient() kya karta hai?
Jab bhi koi service inject hoti hai, AddTransient() har baar naya object banata hai —
Chahe woh same request ke andar ho ya alag-alag.

✅ Kab use karte hain?
Jab service ka koi state maintain karna zaroori nahi ho

Jab lightweight service ho jo har baar refresh ho sakti hai

Jab side-effects se farq nahi padta

🧠 Ek Line Me:
Har baar injection hone par naya instance banta hai.
---------------------
💻 Example: Transient Service
Step 1: Interface & Implementation

public interface IGuidService
{
    string GetGuid();
}

public class TransientGuidService : IGuidService
{
    private string _guid = Guid.NewGuid().ToString();

    public string GetGuid()
    {
        return _guid;
    }
}
-------------------------------
Step 2: Register in Program.cs

builder.Services.AddTransient<IGuidService, TransientGuidService>();
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
------------------------
🔍 Output:
Guid1: abc-123
Guid2: xyz-789
-----------------------------------------
➡️ Har bar naya instance bana, even same request ke andar.
⚠️ Kab na use karein?
Jab service me shared state ho (jaise DBContext)

Jab expensive object creation ho

Jab service kisi scoped/singleton resource pe depend kare
--------------------------------------
🛠 Real Use Cases
Helper services

Stateless utilities (e.g., string formatter, logger adapter, calculator)

Lightweight service jo baar-baar chhoti computation kare
--------------------------------------------------
🔥 Summary Table
🔸 Lifetime	🔹 Transient
Lifetime	Har baar naye instance
Same request me	❌ Different instances
Best for	Stateless, lightweight services