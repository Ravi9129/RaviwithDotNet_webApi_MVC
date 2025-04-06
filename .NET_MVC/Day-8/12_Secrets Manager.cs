using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 12_Secrets Manager
    {
        
    }
}
---------------------------------
Secrets Manager .NET ka ek feature hai jo sensitive data (jaise DB password, API keys, etc.) ko securely store karne ke liye use hota hai — bina appsettings.json me likhe.

🔐 Secrets Manager — Real Explanation
Problem:
Agar tu DB password, JWT keys, Stripe keys appsettings.json me likh dega aur code GitHub pe daal dega — toh leak ho sakta hai!

Solution:
.NET ka User Secrets Manager tuje yeh sensitive data local machine pe safe rakhne deta hai.
----------------
📦 Kab Use Karte Hain?
Scenario	Example
Dev mode me API keys safe rakhna	"Stripe:SecretKey"
DB passwords git me na jaana dena	"ConnectionStrings:DefaultConnection"
JWT secret key hidden rakhna	"Jwt:Key"
Local env me experiment karna	"MyApp:DevToken"
⚙️ Step-by-Step Setup (Bhai ke Style Me)
---------------------------------
✅ Step 1: Project Me Secrets Manager Enable Kar
Terminal/CMD me likh:

dotnet user-secrets init
--------------------------
🔧 Ye command .csproj file me ye line add karega:

<UserSecretsId>myapp-guid-here</UserSecretsId>
-------------------------------
✅ Step 2: Secret Add Kar
dotnet user-secrets set "Jwt:Key" "MySuperSecretJwtKey"
------------------------------------
Aur ek example:

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.;Database=SecureDB;"
--------------------------------------
✅ Step 3: Access Secrets in Code
public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        var jwtKey = _config["Jwt:Key"];
        var conn = _config.GetConnectionString("DefaultConnection");

        ViewBag.Key = jwtKey;
        return View();
    }
}
-----------------------------
📁 Secrets Kahaan Store Hote Hain? (Windows)
C:\Users\<YourUser>\AppData\Roaming\Microsoft\UserSecrets\<guid>\secrets.json
⚠️ Sirf local machine pe stored hota hai. Git me nahi jaata.

----------------------------------------------
🔥 Real-Life Use Case
Tu JWT Auth bana raha hai:

// appsettings.json (default dev fallback)
"Jwt": {
  "Issuer": "MyApp",
  "Audience": "MyUsers",
  "Key": "dummydevkey"
}
-------------------------------------------
But tu real key user secrets me store karega:

dotnet user-secrets set "Jwt:Key" "realSuperSecretKey123456"
Ab production me toh ye file use nahi hogi, secure hai!

