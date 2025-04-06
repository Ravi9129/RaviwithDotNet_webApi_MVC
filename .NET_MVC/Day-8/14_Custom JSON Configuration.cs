using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 14_Custom JSON Configuration
    {
        
    }
}
-----------------------------------------
Custom JSON Configuration in .NET ke baare mein — ekdum tere style mein: no tables, bas real-life example, full explanation, kahan use karein, kyun karein, aur kaise karein.

🧠 Pehle samajh:
🔹 Kya hota hai Custom JSON Configuration?
.NET ka default config system appsettings.json padhta hai.
Lekin tu agar chahe to koi custom file bhi bana sakta hai, jaise:

plaintext
Copy
Edit
myconfig.json
Aur usme apni configuration daal sakta hai — jaise 3rd party settings, app secrets, logging configs, etc.

🔥 Kab use karte hain?
Jab tu config alag file me rakhna chahta hai for modularity

Jab tu chahata hai tenant-wise ya module-wise config (e.g. AuthConfig.json, PaymentConfig.json)

Jab kisi team ko sirf ek config de kar kaam chalana ho

Jab tu sensitive data ko appsettings se alag rakhna chahta hai
----------------------------------------------------
🔧 Step by Step — Kaise karte hain Custom JSON Config use:
🔹 Step 1: JSON file banao
📄 myconfig.json (Project root me daal)

{
  "PaymentSettings": {
    "MerchantId": "abc123",
    "GatewayUrl": "https://payment.com/api",
    "ApiKey": "secret-api-key"
  }
}
------------------------------------------
🔹 Step 2: Program.cs me Load karao

var builder = WebApplication.CreateBuilder(args);

// Add custom config file
builder.Configuration.AddJsonFile("myconfig.json", optional: true, reloadOnChange: true);

var app = builder.Build();
✅ optional: true — agar file missing ho to error nahi aayega
✅ reloadOnChange: true — file change hote hi config reload hoga
----------------------------------------------
🔹 Step 3: Configuration Read karo (Controller ya Service me)

public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        var merchantId = _config["PaymentSettings:MerchantId"];
        var apiKey = _config["PaymentSettings:ApiKey"];
        return Content($"Merchant: {merchantId}, Key: {apiKey}");
    }
}
--------------------------------------
🔹 Step 4: Strongly Typed Object me Bind karna ho to:
Step 4.1: Class banao

public class PaymentSettings
{
    public string MerchantId { get; set; }
    public string GatewayUrl { get; set; }
    public string ApiKey { get; set; }
}
------------------------------------------------
Step 4.2: Program.cs me bind karo

builder.Services.Configure<PaymentSettings>(
    builder.Configuration.GetSection("PaymentSettings"));
    ------------------------------------------------
Step 4.3: Controller me Inject karo

public class HomeController : Controller
{
    private readonly PaymentSettings _paymentSettings;

    public HomeController(IOptions<PaymentSettings> paymentSettings)
    {
        _paymentSettings = paymentSettings.Value;
    }

    public IActionResult Index()
    {
        return Content($"Gateway: {_paymentSettings.GatewayUrl}");
    }
}
--------------------------------------
✅ Real Use Case
Tu e-commerce bana raha hai.

Tu myconfig.json me alag se payment config, shipping config, ya coupon system ka config rakh raha hai.

Alag file me hone se clarity bhi rehti hai, aur jab chahiye deploy time pe update karne me asani.
---------------------------------------------
📌 Bonus Tip:
Agar tu multiple config files load karna chahta hai:

builder.Configuration
    .AddJsonFile("myconfig.json")
    .AddJsonFile("shipping.json")
    .AddJsonFile("coupons.json");
Har ek config section ko apni class me map kar sakta hai.
--------------------------------------------------
👊 Conclusion:
Custom JSON config ka use karna:

✅ Clean architecture
✅ Modular configuration
✅ Scalable system
✅ DevOps friendly