using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 5_Transient service
    {
        
    }
}
--------------------------------------------
Transient service lifetime ki — ye Dependency Injection ka ek mode hai jo temporary (ek baar ke use) ke liye perfect hai.

🔍 Transient Kya Hota Hai?
Transient services wo hoti hain jinka naya instance har baar create hota hai jab tu use karta hai. Chahe wo same request ho ya alag, har baar nayi copy.

✅ Kab Use Karte Hain?
Jab service stateless ho (koi data ya state hold nahi karti)

Jab lightweight object ho (heavy nahi ho create karne me)

Har bar fresh copy chahiye ho (jaise calculator, utility class, logger, etc.)

🧠 Ek Line Me:
Nayi request = Nayi service instance
Agar 10 baar inject kare = 10 alag objects milenge
---------------------------------------------------
💻 Real Example:
1. Service Interface & Implementation

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

----------------------------
2. Register as Transient in Program.cs

builder.Services.AddTransient<IGuidService, TransientGuidService>();
------------------------------------------
3. Controller Me Inject Karke Use
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
Guid1: 111-aaa-xyz
Guid2: 222-bbb-pqr
💥 Dono alag instance hain!

-----------------------------------
⚠️ Caution:
Agar Transient service me state rakhi (like DB connection), toh bugs aayenge

Har bar new object = performance issue (agar heavy object ho)
--------------------------------------------------
🔥 Summary:
🔹 Transient = Nayi baar, naya object
🔹 Stateless aur lightweight services ke liye best
🔹 Register with: AddTransient<TInterface, TImplementation>()