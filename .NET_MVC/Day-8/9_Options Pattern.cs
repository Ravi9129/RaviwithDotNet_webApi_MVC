using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 9_Options Pattern
    {
        
    }
}
--------------------------------
Options Pattern ke baare mein — ye .NET Core ka ek advanced aur clean approach hai configuration values ko handle karne ke liye. Ye tab kaam aata hai jab tu config values ko strongly-typed class ke through access karna chahta hai, bina baar-baar IConfiguration["key"] likhe.

🔍 Kya hota hai Options Pattern?
Options Pattern ka matlab hota hai:
🔹 Appsetting values ko ek class mein map karna
🔹 Aur us class ko dependency injection se inject karwana
🔹 Taa ki tu configuration ko strongly-typed tarike se access kar sake.
------------------------------------------
🧠 Kab Use Karte Hain?
Jab config structure nested ya large ho

Jab tu clean, testable code chahta ho

Jab config values baar-baar use ho rahi ho

Jab tu environment-specific config access kar raha ho
----------------------------------------
✅ Real-life Example Step by Step
🗂 Step 1: appsettings.json mein config likh
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "Port": 587,
  "FromEmail": "noreply@demo.com"
}
------------------------------------------
👨‍💻 Step 2: Create Strongly-Typed Class
public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string FromEmail { get; set; }
}
-------------------------------------------
🧩 Step 3: Register Options Pattern in Program.cs
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
    ---------------------------------------------------
💉 Step 4: Inject IOptions<EmailSettings> in Controller/Service
using Microsoft.Extensions.Options;

public class HomeController : Controller
{
    private readonly EmailSettings _emailSettings;

    public HomeController(IOptions<EmailSettings> emailOptions)
    {
        _emailSettings = emailOptions.Value;
    }

    public IActionResult Index()
    {
        string smtp = _emailSettings.SmtpServer;
        string email = _emailSettings.FromEmail;
        int port = _emailSettings.Port;

        ViewBag.EmailInfo = $"{smtp}:{port} - {email}";

        return View();
    }
}
-----------------------------
🧪 Benefits (Fayde):
Feature	Fayda
✅ Strongly Typed	Intellisense aur validation
✅ Clean Code	IConfiguration[""] ka repetition nahi
✅ Reusable	Service mein inject kar ke baar-baar use kar
✅ Testable	Easily mock ho sakta hai unit testing ke liye
🔁 Variants
1. IOptions<T>
Default use case

Static config values (restart app to reflect changes)
---------------------------------------
2. IOptionsSnapshot<T>
Per-request scoped values (change allowed per request)

Used in Scoped services or controllers
--------
public HomeController(IOptionsSnapshot<EmailSettings> emailOptions)
----------------------------------------------------
3. IOptionsMonitor<T>
Runtime value changes ka support

Useful for background services, hosted services
------------
public MyBackgroundService(IOptionsMonitor<EmailSettings> monitor)
{
    monitor.OnChange(newVal => {
        // Real-time config updated
    });
}
-------------------------------------
🧨 Bonus: Validation with DataAnnotations
public class EmailSettings
{
    [Required]
    public string SmtpServer { get; set; }

    [Range(1, 65535)]
    public int Port { get; set; }
}
----------------------------------------------
Then register with validation:
builder.Services
    .AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"))
    .ValidateDataAnnotations();  // ⚠️ throws if config is invalid