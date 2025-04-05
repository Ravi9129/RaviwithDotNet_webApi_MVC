using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 11_AddSingleton()
    {
        
    }
}
-------------------------------------
AddSingleton() ke baare mein — yeh Dependency Injection ka ek aisa type hai jisme service application ke pure lifetime ke liye ek hi instance bana ke rakhta hai.

🔍 AddSingleton() kya karta hai?
Jab tum kisi service ko AddSingleton() se register karte ho, to sirf ek baar uska instance create hota hai,
aur wohi same instance har jagah inject hota hai — chahe controller ho, middleware ho, ya dusra service ho.

✅ Kab use karein?
Jab service stateless ho

Jab service shared data ya config hold karti ho

Jab service ko bar-bar create karna costly ho (performance ke liye)

🧠 Ek Line Me:
Application ke start hote hi ek instance banta hai, aur wahi har jagah lifetime tak chalta hai.
-----------------------------------------
💻 Example: Singleton Service
Step 1: Interface & Implementation
public interface IGuidService
{
    string GetGuid();
}

public class SingletonGuidService : IGuidService
{
    private string _guid = Guid.NewGuid().ToString();

    public string GetGuid()
    {
        return _guid;
    }
}
-------------------------------
Step 2: Register in Program.cs
builder.Services.AddSingleton<IGuidService, SingletonGuidService>();
-----------------------------------
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
------------
🧾 Output (Har Request Pe):
Guid1: 1234-5678-ABCD
Guid2: 1234-5678-ABCD ✅ (hamesha same instance)
--------------------------------
⚠️ Kab na use karein?
Jab service me user-specific ya request-specific data ho

Jab service me mutable (badalne wali) state ho

Jab DbContext jaisi cheeze ho — yeh singleton nahi ho sakti ❌
------------------------------------
🛠 Real Use Cases
Configuration services (IConfiguration)

Logger instances (ILogger<T>)

Caching service (IMemoryCache, custom cache)

Static lookups (country list, config)
----------------------------------
🔥 Summary:
🔸 Lifetime	🔹 Singleton
Lifetime	Application-wide
Same for all	✅ Yes (same object everywhere)
Created	Only once (at startup or first use)
Best for	Shared config, logging, caching
---------------------------------------
💡 Tip:

// Register
builder.Services.AddSingleton<MyLogger>();
// Use anywhere
public class MyService
{
    private readonly MyLogger _logger;
    public MyService(MyLogger logger)
    {
        _logger = logger;
    }
}