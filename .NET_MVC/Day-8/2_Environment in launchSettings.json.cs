using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 2_Environment in launchSettings.json
    {
        
    }
}
--------------------------
 Environment in launchSettings.json file ki — ye ASP.NET Core project me ek important setup point hai, jahan se tu app ka environment define kar sakta hai bina manually set command likhe. Chal real example ke saath todte hain!

🔥 Kya hota hai launchSettings.json?
Ye file Visual Studio / Rider / CLI se project run karne par environment aur profile settings define karti hai.
----------------------------------------------------------
Iska path hota hai:
Properties/launchSettings.json
---------------------------------------------------------
🔍 Real Use-case:
Tu chah raha hai ki:

Jab tu Visual Studio se run kare, toh app Development mode me chale

Jab tu kisi aur profile se run kare, toh Staging ya Production me

Yeh kaam tu launchSettings.json me kar sakta hai.
----------------------------------------
✅ Sample launchSettings.json
{
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "MyDotNetApp": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging"
      }
    }
  }
}
----------------------------------
🧠 Explanation in Apni Language:
profiles: Multiple environments/profiles define kar sakta hai

IIS Express: Visual Studio me IIS Express se run hone wala profile

MyDotNetApp: Tu direct CLI ya Visual Studio project se run karega

ASPNETCORE_ENVIRONMENT: Yehi main setting hai jo batata hai app kis mode me chalegi — Development, Staging, ya Production.
----------------------------
🧪 Kaise Check Karein?
Code me check karne ke liye tu ye likh sakta hai:

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Dev Mode");
}
else if (app.Environment.IsStaging())
{
    Console.WriteLine("Staging Mode");
}
else if (app.Environment.IsProduction())
{
    Console.WriteLine("Production Mode");
}
-----------------------------------------
💡 Note:
launchSettings.json ka effect sirf Visual Studio ya CLI se "dotnet run" karne pe hota hai.

Jab tu app ko publish karta hai (live deployment), tab ye file use nahi hoti. Uske liye tu environment manually set karega ya deployment script me define karega.
------------------------------------------
🎯 Summary:
launchSettings.json sirf local dev ke liye

ASPNETCORE_ENVIRONMENT se app ka environment set hota hai

Multiple profiles define karke tu alag-alag environment ke liye testing kar sakta hai

Publish ke time ye file kaam nahi aati

