using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 3_Dependency Injection
    {
        
    }
}
----------------------------------------
Dependency Injection (DI) ki — ye Inversion of Control (IoC) ka ek popular implementation technique hai.

🔍 Dependency Injection Kya Hota Hai?
Jab ek class apni dependency khud create karne ke bajaye, usse baahar se inject kiya jaata hai, toh usse Dependency Injection kehte hain.

👉 Matlab: "Main jo chahiye wo khud nahi banaunga, tu de de."
--------------------------------------
🎯 Real Life Example:
Soch tu ek chai peene wala banda hai:

Agar tu khud chai banaye: tight coupling

Agar koi tujhe ready-made chai de de (e.g., chaiwala): Dependency Injected
------------------------------------------------
💻 Code Level Pe:
❌ Without Dependency Injection (Tight Coupling):

public class OrderService
{
    private EmailService _emailService = new EmailService();

    public void PlaceOrder()
    {
        _emailService.SendEmail();
    }
}
🔗 Yaha OrderService tightly coupled hai EmailService se. Test karna mushkil, flexible nahi.
---------------------------------------------
✅ With Dependency Injection:

public class OrderService
{
    private readonly IEmailService _emailService;

    public OrderService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void PlaceOrder()
    {
        _emailService.SendEmail();
    }
}
---------------------------------------
Aur phir Startup me register karte hain:

builder.Services.AddScoped<IEmailService, EmailService>();
🎉 Ab OrderService independent hai, EmailService dynamically inject hoga.
----------------------------------------
🔁 Types of Dependency Injection
Constructor Injection ✅ (most common)

public HomeController(IProductService service) { }
---------------------------------------------
Property Injection
public IProductService ProductService { get; set; }
----------------------------------------------
Method Injection

public IActionResult DoSomething([FromServices] IProductService service) { }
💎 Benefits of DI
✅ Loose Coupling
✅ Easily Testable (Mocking)
✅ Reusability
✅ Flexibility in changing implementation
✅ Clean Architecture
-------------------------------------------
🧠 Example Real Scenario:
Tere paas ek controller hai jo ICustomerService pe depend hai:

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public IActionResult Index()
    {
        var data = _customerService.GetAll();
        return View(data);
    }
}
------------------------------
Startup me:

builder.Services.AddScoped<ICustomerService, CustomerService>();
Ab test ke time tu mock ICustomerService de sakta hai. Production me real CustomerService milega.
-----------------------
💬 Last Line Bhai:
"Dependency Injection ek tareeka hai dependency ko inject karne ka — jisse code flexible, testable aur maintainable ban jaata hai."