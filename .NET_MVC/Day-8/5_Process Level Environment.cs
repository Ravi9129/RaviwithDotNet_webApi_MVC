using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 5_Process Level Environment
    {
        
    }
}
-----------------------------------------
Process Level Environment ke baare mein .NET Core / ASP.NET Core mein — real-life use-case ke sath, easy bhasha mein.

🔍 Kya hota hai Process Level Environment?
Ye Operating System ke level par set hota hai — jiska effect poore application ke process pe padta hai. Ye value ASPNETCORE_ENVIRONMENT ki hoti hai jo batati hai ki app kis mode mein chal rahi hai:

Development

Staging

Production

ya koi custom jaise QA, Testing, etc.

💡 Real-life Use-case:
Man lo tera app Production server pe deploy hai, tu nahi chahta ki error pages dikhaye jayein end-users ko — to usme app Production mode me hi run honi chahiye.

Lekin Developer machine pe tu debugging ke liye full errors dekhna chahta hai, to waha Development mode hona chahiye.
----------------------------------------
✅ Kaise Set Karte Hain?
🔸 Windows (CMD):
setx ASPNETCORE_ENVIRONMENT "Development"
➡️ Ye system-level environment variable set karega. System reboot ke baad bhi persist karega.
-----------------------------------------------------
🔸 PowerShell:
[System.Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Staging", "Machine")
--------------------------------------
🔸 Linux/macOS (Bash):

export ASPNETCORE_ENVIRONMENT=Production

➡️ Ye sirf current session ke liye set hota hai. Agar permanently chahiye toh ~/.bashrc ya ~/.profile me likhna padta hai.
-----------------------------------------------------
🚀 Kaise Read Karte Ho Application ke Andar?
var environment = builder.Environment.EnvironmentName;

if (environment == "Development")
{
    // Dev-specific logic
}
-------------------------------------------------------
Ya controller mein:
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
            // Dev mode logic
        }

        return View();
    }
}
--------------------------------
🎯 Important:
Agar ASPNETCORE_ENVIRONMENT set nahi hai → default hota hai Production.

Ye process start hone se pehle set hona chahiye.

Production mein kabhi bhi Development mode na chhodo — security risk ho sakta hai!
-----------------------------------------------------
🧪 Real Example in Program.cs:

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}