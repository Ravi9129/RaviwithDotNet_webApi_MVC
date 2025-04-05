using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 1_Environment 
    {
        
    }
}
---------------------------
Environment ke baare mein ASP.NET Core mein. Yeh ek simple concept hai, lekin real projects mein bahut kaam ka hota hai. Chalo easy aur real-life example ke saath samjhte hain.

🔍 Environment hota kya hai?
Environment ka matlab hai:

Application kis mode ya phase mein run ho rahi hai — Development, Staging, ya Production.

ASP.NET Core mein app jab run hoti hai, toh ek environment variable set hota hai jiska naam hota hai:
----------------------
ASPNETCORE_ENVIRONMENT
Iska use karke hum environment-specific configuration, settings, ya code chala sakte hain.
-------------------------------------
✅ Kab aur Kyu use karte hain?
🔹 Development:
Jab developer app bana raha hota hai

Detailed error messages, debugging tools allowed
----------------------------------------
🔹 Staging:
Production ke jaise setup

Real testing, no dev tools
------------------------------------------
🔹 Production:
Live users ke liye

Errors hidden, logging enabled, high performance
------------------------------------------------------
🧠 Real-World Example:
Tu ek e-commerce app bana raha hai. Tere paas ek setting hai:

{
  "PaymentGateway": "DummyGateway"
}
---------------------------------------------------------
Tujhe chahiye ki:

Development mein DummyGateway use ho

Production mein RealGateway use ho

Toh tu appsettings.Development.json aur appsettings.Production.json files alag-alag bana ke manage kar sakta hai.

🔧 Environment Set kaise karte hain?
--------------------------------
1. Visual Studio me:
Project → Properties → Debug → Environment variables:
ASPNETCORE_ENVIRONMENT = Development
------------------------------------------------
2. Command Line se:
set ASPNETCORE_ENVIRONMENT=Production
dotnet run
---------------------------------------
3. launchSettings.json:
"profiles": {
  "MyApp": {
    "environmentVariables": {
      "ASPNETCORE_ENVIRONMENT": "Development"
    }
  }
}
--------------------------------------------
🔄 Use kaise karte hain code mein?
1. Program.cs me condition laga ke:

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // show detailed error
}
else
{
    app.UseExceptionHandler("/Home/Error"); // show friendly error
}
------------------------------------
2. Views mein bhi use ho sakta hai:
@if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    <div>Ye Dev Mode ka message hai</div>
}
----------------------------------
3. Controller ya Service ke andar:

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _env;

    public HomeController(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult Index()
    {
        if (_env.IsDevelopment())
        {
            return Content("App is running in Development");
        }

        return Content("App is Live");
    }
}