using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 8_Hierarchical Configuration
    {
        
    }
}
-------------------------------------
 Hierarchical Configuration ke baare mein — ye .NET Core ka ek powerful feature hai jisme tu multi-level config values ko nested structure mein access kar sakta hai.

📘 Kya hota hai Hierarchical Configuration?
Hierarchical Configuration matlab config values ko multi-level nested format mein define karna — jaise tu folder ke andar folder rakh raha ho.

Yeh useful hota hai jab:

Tu modules ko organize karna chahta hai (jaise Payment, Email, Logging)

Har section ke under multiple keys hoti hain

Data ko logically group karna hota hai
---------------------------------------------
🔧 Example: appsettings.json
{
  "AppSettings": {
    "SiteTitle": "Mera Cool App",
    "Admin": {
      "Email": "admin@meraapp.com",
      "Phone": "9999999999"
    },
    "PaymentGateway": {
      "Razorpay": {
        "Key": "rzp_test_abc123",
        "Secret": "xyz456"
      }
    }
  }
}
--------------------------------------------
🧠 Access karna controller ke andar
public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        var title = _config["AppSettings:SiteTitle"];
        var adminEmail = _config["AppSettings:Admin:Email"];
        var razorpayKey = _config["AppSettings:PaymentGateway:Razorpay:Key"];

        ViewBag.Title = title;
        ViewBag.Email = adminEmail;
        ViewBag.Key = razorpayKey;

        return View();
    }
}
🧠 Colon : ka use hota hai nested property ko access karne ke liye.
------------------------------------
✅ Real-life Example Use Cases:
1. Payment Configuration:

"Payment": {
  "Paytm": {
    "ApiKey": "abc",
    "MerchantId": "M001"
  },
  "Razorpay": {
    "Key": "rzp_key",
    "Secret": "rzp_secret"
  }
}
-----------------------------------
2. Email Config:
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "Port": "587",
  "FromEmail": "noreply@abc.com"
}
⚡ Advanced: Bind to POCO Class
Tu config ko ek class mein bhi map kar sakta hai:
-----------------------------------------------
🔹 Step 1: Create class

public class RazorpayConfig
{
    public string Key { get; set; }
    public string Secret { get; set; }
}
------------------------------------
🔹 Step 2: Register in Program.cs

builder.Services.Configure<RazorpayConfig>(builder.Configuration.GetSection("AppSettings:PaymentGateway:Razorpay"));
------------------------------------
🔹 Step 3: Use in controller
private readonly RazorpayConfig _razorpay;

public HomeController(IOptions<RazorpayConfig> razorpayOptions)
{
    _razorpay = razorpayOptions.Value;
}
