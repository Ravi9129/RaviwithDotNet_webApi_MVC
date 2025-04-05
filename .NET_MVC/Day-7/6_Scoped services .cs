using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 6_Scoped services 
    {
        
    }
}
-------------------------------------------------
Scoped services ki — ye bhi Dependency Injection ka ek important lifetime hai, jo especially web apps ke liye kaafi useful hai.

🔍 Scoped Kya Hota Hai?
Scoped service ek HTTP request ke dauraan ek hi instance provide karti hai.
Agar controller, service, aur repository ek hi request ke andar scoped service use kar rahe hain, toh sabko same object milega.

✅ Kab Use Karte Hain?
Jab tu stateful data manage kar raha hai request ke scope ke andar

Jab service me user-specific context ho (like authenticated user, request data)

Jab tu Entity Framework DbContext use kar raha ho (default me scoped hota hai)

🧠 Ek Line Me:
Ek HTTP request = Ek service instance
Multiple components within that request = Same object

💻 Real Example:
1. Service Interface & Implementation
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
-----------------------
2. Register as Scoped in Program.cs

builder.Services.AddScoped<IGuidService, ScopedGuidService>();
---------------------------------------------------
3. Controller Me Inject Karke Dekh

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

👀 Output:
Guid1: 333-xxx-abc
Guid2: 333-xxx-abc
💥 Dono same hain, kyunki ek hi HTTP request ke andar call hua hai.

-----------------------------------
🔐 Real Use Case: EF Core

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("Default")));
🧠 Ye DbContext scoped hota hai by default, taaki ek hi request me multiple repository use karein toh sab same context se kaam karein — varna "multiple active result sets" error aa sakta hai.
--------------------------------------------------
⚠️ Caution:
Scoped services ko Singleton ke andar inject mat karna, runtime error aayega.

Best for web APIs and MVC, kyunki HTTP request ka concept hota hai.
------------------------------------------------
🔥 Summary:
🔹 Scoped = Ek HTTP request = Ek instance
🔹 Best for user/request-specific logic
🔹 Ideal for EF Core DbContext
🔹 Register with: AddScoped<TInterface, TImplementation>()