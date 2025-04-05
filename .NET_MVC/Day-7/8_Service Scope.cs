using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 8_Service Scope
    {
        
    }
}
----------------------------------
Service Scope ki — yeh Scoped lifetime ka poora concept hai, jo ASP.NET Core Dependency Injection ka ek core feature hai.

🔍 Service Scope kya hota hai?
Scoped services har ek HTTP Request ke liye ek naya instance create karti hain.
Aur usi request ke andar agar kahin bhi baar-baar use ho, toh same instance use hota hai.

✅ Kab use karte hain?
Jab aapko per-request data maintain karna ho

Jab aapko DB Context use karna ho (EF Core)

Jab service ka state sirf ek HTTP request tak hi valid ho

🧠 Ek Line Me:
Har HTTP Request ke liye ek fresh instance milta hai
Request ke andar shared hota hai, lekin dusri request me naya
-------------------------------------
💻 Example: Scoped Service
Step 1: Service Interface & Class

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
-----------------------------------
Step 2: Register in Program.cs
builder.Services.AddScoped<IGuidService, ScopedGuidService>();
----------------------------------------
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
-------------------------------------
✅ Output:

Same HTTP Request:
Guid1: abc-xyz
Guid2: abc-xyz  ✅ Same instance
------------------------------------
Next HTTP Request:
Guid1: pqr-lmn
Guid2: pqr-lmn  ✅ New instance
⚠️ Real Use Cases
DbContext (Entity Framework)

Services that work with request headers, tokens, user info

Unit of Work pattern
---------------------------
❌ Galtiyaan Jo Avoid Karni Hai:
Scoped service ko Singleton ke andar inject mat karo
→ runtime error aayega (invalid operation: "cannot consume scoped from singleton")
-----------------------------------------------
💡 Bonus: Scope Ka Manual Use (Advanced Scenario)
Kabhi kabhi khud bhi ek service scope banaana padta hai, jaise background task me:
----------
using (var scope = serviceProvider.CreateScope())
{
    var scopedService = scope.ServiceProvider.GetRequiredService<IMyService>();
    scopedService.DoWork();
}
-------------------------------------------------
🔥 Summary
🔸 Concept	🔹 Scoped Service
Instance Lifetime	Per HTTP Request
Shared inside request	✅ Yes
Shared across requests	❌ No
Use When	Request-specific logic, DbContext, Auth
