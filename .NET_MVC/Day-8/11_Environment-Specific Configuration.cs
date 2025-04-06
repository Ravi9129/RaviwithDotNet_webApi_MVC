using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 11_Environment-Specific Configuration
    {
         
    }
}
-------------------------------------------
Environment-Specific Configuration ke baare mein — yaani har environment (Development, Staging, Production) ke liye alag-alag config kaise use karte hain .NET Core me.

🔥 Kya hota hai "Environment-Specific Configuration"?
Jab tu chahata hai ki:

Development me local DB chale,

Staging me test server ka URL ho,

Production me secure real data use ho,

Toh har environment ke liye alag config file ya settings define ki jati hai. Isse tu secure, flexible aur scalable code likhta hai.
---------------------------------------------
✅ Kab use karte hain?
Jab alag environment pe deploy karna ho

Jab sensitive config (DB, API Keys) har environment me alag ho

Jab testing/staging ke data aur logic alag ho

Jab error handling ya logging ko customize karna ho
-------------------------------------------
🔧 Step-by-Step: Environment Specific Configuration Setup
🧾 Step 1: appsettings.json Files
Default config file:
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=dev-db;Database=AppDB;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
----------------------------------------------
Environment-specific config:

// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DevDB;"
  }
}

// appsettings.Production.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=ProdDB;"
  }
}
--------------------------------------
⚙️ Step 2: Program.cs (Automatic Load)
.NET Core khud hi environment ke hisab se config file load karta hai:

var builder = WebApplication.CreateBuilder(args);

// Loads: appsettings.json + appsettings.{Environment}.json
// e.g. Development, Staging, Production
-----------------------------------
🏷 Step 3: Set Environment
Local Machine (launchSettings.json):
"profiles": {
  "MyApp": {
    "environmentVariables": {
      "ASPNETCORE_ENVIRONMENT": "Development"
    }
  }
}
------------------------------
Production Server (e.g. Linux/Windows):

export ASPNETCORE_ENVIRONMENT=Production
------------------------------------
🧪 Step 4: Use Config Based on Environment
Example: inject config

public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        var connStr = _config.GetConnectionString("DefaultConnection");
        ViewBag.Conn = connStr;

        return View();
    }
}
------------------------------------------
✅ BONUS: Use IWebHostEnvironment to Check

public class Startup
{
    private readonly IWebHostEnvironment _env;

    public Startup(IWebHostEnvironment env)
    {
        _env = env;
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else if (_env.IsProduction())
        {
            app.UseExceptionHandler("/Error");
        }
    }
}