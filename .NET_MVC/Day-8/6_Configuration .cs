using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 6_Configuration 
    {
        
    }
}
------------------------------------------------------
Configuration in ASP.NET Core ke baare mein — real-life examples ke sath, teri language mein.

🧠 Kya hota hai Configuration?
ASP.NET Core mein Configuration ka matlab hota hai:

App ke settings ko read karna — jaise connection strings, API keys, environment-specific values, etc.

Ye values alag-alag sources se aati hain — jaise:

appsettings.json

Environment variables

Command-line arguments

Secrets.json (for dev)

Azure KeyVault, etc.
----------------------------------------------------------------
🔧 Real-World Use Case:
Man le tu ek ecommerce app bana raha hai. Usmein:

Payment API key chahiye

Database ka connection string alag honi chahiye dev aur prod mein

Logging ka level set karna hai (Information, Warning, Error)

Ye sab values tu hardcode nahi karega — tu rakhega configuration files mein.
---------------------------------------------------
✅ Kaise Use Karte Hain?
1. appsettings.json
{
  "AppSettings": {
    "SiteTitle": "Mera Blog",
    "ApiKey": "12345-abcde"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MyDb;Trusted_Connection=True;"
  }
}
------------------------------------------------
2. Program.cs mein Register hota hai:

var builder = WebApplication.CreateBuilder(args);

// Access config
var config = builder.Configuration;
string title = config["AppSettings:SiteTitle"];
string connStr = config.GetConnectionString("DefaultConnection");
------------------------------------------------------------
3. Strongly Typed Class ke saath:
public class AppSettings
{
    public string SiteTitle { get; set; }
    public string ApiKey { get; set; }
}
-------------------------
Program.cs mein bind:

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
------------------------------------------
Controller ya service mein inject:

public class HomeController : Controller
{
    private readonly AppSettings _settings;

    public HomeController(IOptions<AppSettings> settings)
    {
        _settings = settings.Value;
    }

    public IActionResult Index()
    {
        var title = _settings.SiteTitle;
        return View();
    }
}
---------------------------------
🔄 Environment-wise Configuration:
Tu bana sakta hai environment-specific files:

appsettings.Development.json

appsettings.Production.json

Aur ASP.NET Core automatically override karega values based on environment.

🔐 Secrets.json (only for dev)
----------------------------------------------
Tu sensitive data dev mein rakh sakta hai:
dotnet user-secrets init
dotnet user-secrets set "AppSettings:ApiKey" "secret-value"
Ye data appsettings.json jaise hi accessible hoga.
--------------------------------------------
🔗 Configuration Order (Priority):
Command-line args

Environment variables

appsettings.{env}.json

appsettings.json

User Secrets (for dev only)

Hardcoded defaults (agar kuch bhi na mile)
------------------------------
💡 Tip:
Tu builder.Configuration["Key"] ka use karke kisi bhi setting ko directly le sakta hai — bina strongly typed class ke bhi.