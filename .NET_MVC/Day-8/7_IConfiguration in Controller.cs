using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 7_IConfiguration in Controller
    {
        
    }
}
------------------------------------------------
IConfiguration in Controller ke baare mein — real project jaise feel ke sath. Ye ek bahut common aur useful feature hai .NET Core mein.

🔍 Kya hai IConfiguration?
IConfiguration ek built-in interface hai jo app ke config sources (jaise appsettings.json, env variables, etc.) se values read karne ka kaam karta hai.

✅ Use Case:
Man le tu ek API Key, ya koi DB Connection String, ya koi custom value read karna chahta hai directly controller ke andar.

Jaise:

Payment gateway ka API key

Admin panel ka default username

App title ya version info
-------------------------------------------------
🔧 Step-by-Step Implementation
🔹 1. Add data in appsettings.json:
{
  "AppSettings": {
    "SiteTitle": "Mera Cool App",
    "AdminEmail": "admin@meraapp.com"
  }
}
--------------------------------------
🔹 2. Inject IConfiguration in Controller:

using Microsoft.Extensions.Configuration;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        string siteTitle = _configuration["AppSettings:SiteTitle"];
        string adminEmail = _configuration["AppSettings:AdminEmail"];

        ViewBag.Title = siteTitle;
        ViewBag.Email = adminEmail;

        return View();
    }
}
------------------------------
💡 Accessing Nested Keys:
Agar tu deeply nested config use kare:
"PaymentGateway": {
  "Paytm": {
    "ApiKey": "abc123",
    "MerchantId": "M12345"
  }
}
-----------------------------------
Then controller mein:

string paytmKey = _configuration["PaymentGateway:Paytm:ApiKey"];
string merchantId = _configuration["PaymentGateway:Paytm:MerchantId"];
-----------------------------------------------
😎 Bonus: Type-Safe Configuration Alternative
Bade apps mein IOptions<T> ya strongly typed class better hoti hai. Lekin agar tu chhoti value chahiye directly — IConfiguration perfect hai!

