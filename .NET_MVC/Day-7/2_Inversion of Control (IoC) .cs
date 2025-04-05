using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 2_Inversion of Control (IoC) 
    {
        
    }
}
---------------------------------
Inversion of Control (IoC) ka concept Dependency Inversion Principle ka hi practical implementation hai. Dono bhai-bhai jese hain — lekin IoC broader concept hai aur DIP ek principle jo uska use karta hai.

Tere style me aasan bhaasha me, real-life aur .NET code ke examples ke saath samjhaata hoon 👇

🔄 Kya Hota Hai Inversion of Control (IoC)?
Simple Definition:

Jab control kisi aur ke paas chala jaata hai — humare code ka control kisi aur system ya container ke paas hota hai — jise bolte hain: Inversion of Control.

🎯 Matlab:
Apun object create nahi karega, koi aur karega — mostly framework ya container.

🔥 Real Life Example:
Tu zomato se khana order karta hai:

Agar tu restaurant jaa ke khana bana ke lata — that’s normal control.

Lekin agar tu Zomato app me click kare aur wo delivery boy bheje — that’s Inversion of Control.

Control tu ne chhod diya, Zomato ne le liya. That’s IoC.
----------------------------------------
💻 Programming World Me:
🔴 Traditional Way (Without IoC):
public class HomeController : Controller
{
    private readonly ProductService _productService;

    public HomeController()
    {
        _productService = new ProductService(); // direct dependency
    }
}
😵 Tu khud service ka object bana raha hai = control tere paas hai.
--------------------------------------------
✅ IoC Way (Dependency Injection):
public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }
}
--------------------------------
Aur phir tu register karta hai Startup me:

builder.Services.AddScoped<IProductService, ProductService>();
Ab object banana aur inject karna ka kaam .NET framework karega, tu nahi.

📦 Yehi hota hai Inversion of Control.
-----------------------------------------
🤝 IoC Container
.NET me jo object manage karta hai use bolte hai IoC Container
👉 Wo decide karta hai:

Kya object banega?

Uske dependencies kya hain?

Life-cycle kya hogi?

.NET Core me default IoC container use hota hai (ServiceCollection) via AddScoped, AddTransient, AddSingleton.
----------------------------------
🚀 Summary
Term	Asaan Bhaasha
IoC	"Control ulta kar diya" — object banana framework kare
DIP	"High-level logic should depend on interface, not concrete"
IoC Container	"Factory jo dependencies resolve karta hai"
-------------------------------------------
💬 Final Line:
"IoC ek technique hai jisme control hum se framework ke paas chala jaata hai, 
aur DIP uska ek rule hai jisse hum loosely coupled code likhte hain."