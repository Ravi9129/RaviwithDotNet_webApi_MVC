using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 7_Singleton 
    {
        
    }
}
--------------------------------------------
Singleton ki — ye Dependency Injection ka sabse powerful aur risky lifetime hai. Dhyan se samajhna 😄

🔍 Singleton Kya Hota Hai?
Singleton services poore application ke lifetime me sirf ek baar banayi jaati hain.
Uske baad jitne bhi jagah inject karein — sabko wahi same instance milta hai.

✅ Kab Use Karte Hain?
Jab service me shared global state ho

Jab service banane me expensive operation ho (e.g., heavy configuration, caching, logging)

Jab service me thread-safe logic ho

🧠 Ek Line Me:
Pura app lifetime = Ek hi instance
App start hua toh service create hoti hai — fir wahi repeat hoti hai sab jagah
--------------------------------
💻 Real Example:
1. Service Interface & Implementation

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
----------------------------------
2. Register as Singleton in Program.cs

builder.Services.AddSingleton<IGuidService, SingletonGuidService>();
-------------------------------------
3. Use in Controller

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
-------------
👀 Output:

Guid1: aaa-bbb-ccc
Guid2: aaa-bbb-ccc
💥 App me chahe jitni baar bhi call ho — result same hi rahega.

-------------------------------
✅ Real Use Cases:
ILogger<T> → by default Singleton hota hai

IConfiguration, IHostEnvironment → mostly singleton

Custom ICacheService, FeatureToggleService, GlobalSettingsService
---------------------------
⚠️ Caution:
NOT thread-safe? = Problem! Multiple users same data share karenge
---------------------------------------
Avoid using Singleton for:

DbContext ❌

Request-based data ❌

Don't inject Scoped services inside Singleton — runtime error milega
------------------------------------------
🔥 Summary:
🔹 Singleton = Ek instance for entire app lifetime
🔹 Shared across all users & requests
🔹 Best for logging, caching, config, and expensive services
🔹 Register with: AddSingleton<TInterface, TImplementation>()