using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 12_View Injection
    {
        
    }
}
---------------------------------------
View Injection ki — yaani ki View ke andar dependency injection kaise karein, bina controller ke data pass karein.

🔍 View Injection kya hota hai?
View Injection ka matlab hai:
Directly Razor View ke andar service ko inject karna, bina controller ke through pass kare.
-------------------------------
📌 Syntax:
@inject ServiceType PropertyName
ServiceType: Tumhara service ya interface (e.g., ILogger<T>, IConfiguration, custom services)

PropertyName: View ke andar jo naam use karna hai uska
------------------------------------------
🔧 Example 1: Injecting IConfiguration in View
Step 1: Inject in View
@inject IConfiguration Configuration

<p>App Name: @Configuration["AppSettings:AppName"]</p>
Output:
App Name: MyAwesomeApp
-------------------------------
🔧 Example 2: Injecting Custom Service in View
Step 1: Service Interface & Class
public interface IMessageService
{
    string GetWelcomeMessage();
}

public class MessageService : IMessageService
{
    public string GetWelcomeMessage()
    {
        return "Welcome, Bhai! This is from injected service.";
    }
}
-------------------------
Step 2: Register in Program.cs
builder.Services.AddSingleton<IMessageService, MessageService>();
------------------------------
Step 3: Use in Razor View
@inject IMessageService MessageService

<h2>@MessageService.GetWelcomeMessage()</h2>
Output:
Welcome, Bhai! This is from injected service.
-----------------------------------------------
✅ Kab use karein?
Jab tumhe view ke andar hi service ka data chahiye ho

Jab tum layout, partial views, ya components me reusable logic inject karna chahte ho

Jab controller se unnecessarily data forward karna avoid karna ho
-----------------------------------
⚠️ Kab na karein?
Jab data controller ke context se hi related ho (matlab user-specific ya request-specific)

Jab service heavy ho ya DI overuse ho raha ho
-------------------------------
🛠 Real Life Use Cases:
Scenario	Service Injected
Layout me App name config	IConfiguration
Layout me custom logger	ILogger<Layout>
Partial view me user service	IUserService
Footer ya Header me static data	IContentService
-------------------------------------
💡 Bonus: View Injection in _Layout.cshtml
@inject IConfiguration Config

<title>@Config["AppSettings:Title"]</title>