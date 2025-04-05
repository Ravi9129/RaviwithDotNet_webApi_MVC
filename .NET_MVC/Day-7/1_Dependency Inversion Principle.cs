using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 1_Dependency Inversion Principle
    {
        
    }
}
-------------------------------------
Dependency Inversion Principle ek solid principle hai — SOLID principles me se "D" — aur ye architecture aur code maintainability ke liye badi cheez hai.

Main tere language me real-life se leke .NET world tak sab samjhaata hoon 👇

🔷 Kya Hai Dependency Inversion Principle?
Definition (asaan bhaasha me):
High-level modules should not depend on low-level modules. Both should depend on abstractions.
Abstractions should not depend on details. Details should depend on abstractions.
---------------------------------
Asli matlab?
Tera high-level code (business logic) directly low-level code (database, APIs, services) pe depend na kare.

Dono interface ya abstract class pe depend karein — taaki loosely coupled code ban sake.
------------------------------------
📦 Real-Life Example
🔴 Galat Design (Direct Dependency):

public class EmailService
{
    public void Send(string to, string message)
    {
        // SMTP email bhejna
    }
}

public class OrderProcessor
{
    private EmailService _emailService = new EmailService();

    public void Process()
    {
        // Order process logic
        _emailService.Send("user@example.com", "Order processed");
    }
}
Kya dikkat hai?

OrderProcessor tightly coupled hai EmailService ke saath

Agar kal ko EmailService badli — to OrderProcessor bhi badalna padega 😓
-------------------------------------------------------------
✅ Sahi Design (DIP Follow):
Step 1: Interface banao

public interface IMessageService
{
    void Send(string to, string message);
}
---------------------------------------------
Step 2: Implement karo
public class EmailService : IMessageService
{
    public void Send(string to, string message)
    {
        // Email bhejna
    }
}
-------------------------------------------
Step 3: Abstraction inject karo
public class OrderProcessor
{
    private readonly IMessageService _messageService;

    public OrderProcessor(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Process()
    {
        // Order process logic
        _messageService.Send("user@example.com", "Order processed");
    }
}
-----------------------------------------------
🧪 Real .NET Core Example (with DI)
// Program.cs or Startup.cs
builder.Services.AddScoped<IMessageService, EmailService>();
builder.Services.AddScoped<OrderProcessor>();
----------------------------------------------------
Ab .NET Core khud injection karega:

public class HomeController : Controller
{
    private readonly OrderProcessor _orderProcessor;

    public HomeController(OrderProcessor orderProcessor)
    {
        _orderProcessor = orderProcessor;
    }

    public IActionResult Index()
    {
        _orderProcessor.Process();
        return View();
    }
}
------------------
🎯 Fayda Kya Hai?
Fayda	Explanation
🔄 Easily Swappable	Tu EmailService ki jagah SmsService laga sakta hai bina high-level code badle
🧪 Unit Testing Friendly	Tu mocks ya fakes inject karke easily test kar sakta hai
🔌 Loosely Coupled	Code independent hota hai implementation details se
📈 Scalable	Future changes ya nayi features me asaani hoti hai
--------------------------------------
🚀 Final Ek Line Me:
"Apne code ko implementation pe depend na hone do. Interface pe depend karo. Aur .NET ki DI container se kaam aur asaan ho jaata hai."