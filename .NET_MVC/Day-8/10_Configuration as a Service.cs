using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 10_Configuration as a Service
    {
        
    }
}
-------------------------------
"Configuration as a Service" ke baare mein — yaani tu apne app ke configuration settings ko service ke through inject karke use kare, taa ki code zyada modular, reusable aur testable ho jaaye.

🔥 Kya hota hai Configuration as a Service?
Yeh ek approach hai jisme:

🧠 Configuration ko ek class me wrap karte hain (usually strongly typed)
🔄 Us class ko service ke through inject karte hain
🎯 Aur fir poori app me us service ko use karte hain

Yani:
👉 appsettings.json →
👉 Strongly typed class →
👉 Inject via service (DI) →
👉 Use wherever needed

✅ Kab Use Karte Hain?
Jab config complex ya nested ho

Jab config ko multiple jagah access karna ho

Jab config ka logic bhi ho (e.g., value calculate karni ho)

Jab testing easy aur decoupled code chahiye
----------------------------------------------------
🔧 Real-Life Example: Step by Step
🗂 Step 1: appsettings.json me config
"PaymentSettings": {
  "GatewayUrl": "https://api.razorpay.com",
  "ApiKey": "xyz123",
  "Currency": "INR"
}
----------------------------------
👨‍💻 Step 2: Strongly Typed Class (POCO)
public class PaymentSettings
{
    public string GatewayUrl { get; set; }
    public string ApiKey { get; set; }
    public string Currency { get; set; }
}
---------------------------------------------------------
🧩 Step 3: Service Create Karo
public interface IPaymentConfigService
{
    string GetGatewayUrl();
    string GetApiKey();
    string GetCurrency();
}

public class PaymentConfigService : IPaymentConfigService
{
    private readonly PaymentSettings _settings;

    public PaymentConfigService(IOptions<PaymentSettings> options)
    {
        _settings = options.Value;
    }

    public string GetGatewayUrl() => _settings.GatewayUrl;
    public string GetApiKey() => _settings.ApiKey;
    public string GetCurrency() => _settings.Currency;
}
----------------------------------------------
🧷 Step 4: Program.cs me Register Karo
builder.Services.Configure<PaymentSettings>(
    builder.Configuration.GetSection("PaymentSettings"));

builder.Services.AddSingleton<IPaymentConfigService, PaymentConfigService>();
-------------------------------------------
💉 Step 5: Inject Service Wherever You Want
public class OrderController : Controller
{
    private readonly IPaymentConfigService _paymentConfig;

    public OrderController(IPaymentConfigService paymentConfig)
    {
        _paymentConfig = paymentConfig;
    }

    public IActionResult Checkout()
    {
        var url = _paymentConfig.GetGatewayUrl();
        var key = _paymentConfig.GetApiKey();

        // Use these for payment logic...
        ViewBag.Url = url;
        ViewBag.Key = key;

        return View();
    }
}
----------------------------------
📦 Fayde (Advantages):
🔹 Feature	🔍 Fayda
✅ Clean Code	Config logic alag ho jata hai
✅ Reusable	Service ko kahi bhi inject kar sakte ho
✅ Testable	Easily mock ho sakta hai testing me
✅ DRY	Config access bar-bar likhne ki jarurat nahi
✅ Extendable	Config ke sath kuch logic bhi add kar sakte ho

------------------------
👑 BONUS: Live Reload with IOptionsMonitor

Agar tu chahe ki config file change ho toh bina restart ke reflect ho, to IOptionsMonitor use kar:

public PaymentConfigService(IOptionsMonitor<PaymentSettings> options)
{
    _settings = options.CurrentValue;

    options.OnChange(newValue =>
    {
        _settings = newValue;
        // Real-time update
    });
}