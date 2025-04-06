using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 13_Environment Variables Configuration
    {
        
    }
}
----------------------------------------
Environment Variables Configuration in .NET ko tere style me samjhte hain — bina table ke, full real-world examples ke sath. Yeh ek bahut powerful aur secure way hai config manage karne ka, especially deployment ke time.
------------------------------
🔥 Kya hota hai Environment Variables?
Environment variables OS-level key-value pairs hote hain jo application ko batate hain kuch dynamic ya sensitive information, jaise:

DB connection string

JWT key

Email server

Third-party API key

Deployment environment (Development / Staging / Production)
--------------------------------------------
✅ Kyu Use Karte Hain?
Security: appsettings.json me sensitive data mat daal, instead env variables use kar.

Environment specific settings: Dev, QA, Staging, Prod — har jagah alag config rakhna.

CI/CD friendly: Jenkins, GitHub Actions, Azure — sab environment variables easily support karte hain.
---------------------------------------
🔧 Real Scenario:
Tere paas ye appsettings.json hai:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AppDb;"
  },
  "Jwt": {
    "Key": "hardcoded-key"
  }
}
But tu JWT key aur connection string ko env variable se overwrite karna chahta hai.

⚙️ Step-by-Step — How to Use Env Variables in .NET
---------------------
🔹 Step 1: Set Env Variable (Windows)
PowerShell / CMD me likh:

$env:ConnectionStrings__DefaultConnection="Server=.;Database=EnvAppDb;"
$env:Jwt__Key="super-secret-env-jwt-key"
💡 Double underscore __ likhna hota hai for nested sections (jaise "Jwt:Key").
--------------------------------------------
🔹 Step 2: Access in Code (Auto-merged)
.NET IConfiguration automatically merge karta hai:

public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        var conn = _config.GetConnectionString("DefaultConnection");
        var key = _config["Jwt:Key"];
        return Content($"Conn: {conn}, Key: {key}");
    }
}
👆 Yeh env variables ko priority deta hai agar same key appsettings.json me bhi ho.
--------------------------------------------------
🔹 Step 3: LaunchSettings.json me bhi set kar sakta hai (dev only)

"profiles": {
  "MyApp": {
    "environmentVariables": {
      "Jwt__Key": "dev-jwt-env-key",
      "ConnectionStrings__DefaultConnection": "Server=.;Database=DevDb;"
    }
  }
}
-----------------------------------
🔹 Step 4: Docker ya Azure me bhi easy set hoti hain
Dockerfile me:
ENV Jwt__Key=prod-jwt-env
ENV ConnectionStrings__DefaultConnection=Server=db;Database=ProdDB;
---------------------------------------
Azure portal me:

Configuration > Application Settings > Add Key-Value
----------------------------------------
🔥 Real Use Case
Tu production me deploy kar raha hai:

Code me appsettings.json hai par Jwt:Key = dummykey
---------------------------------
Production server pe env variable set hai:

export Jwt__Key="super-prod-secret-key"
Toh runtime pe .NET use karega super-prod-secret-key, not dummy one. Secure ✅
-----------------------------------------
📌 Pro Tips:
env vars > appsettings.Production.json > appsettings.json ← Priority order

Use double underscore __ for nested configs

Deployment pipeline me env variables inject karwana safest hai

