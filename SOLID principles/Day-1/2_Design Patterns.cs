using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLID principles.Day-1
{
    public class 2_Design Patterns
    {
        
    }
}
-------------------------------------------
🔰 What are Design Patterns?
Design Patterns woh tried-and-tested standard solutions hote hain jo software development me commonly aane wale problems 
ke liye banaye gaye hain. Inka use karne se:

Code reusable hota hai

Maintainable hota hai

Readable aur testable hota hai

Team collaboration better hoti hai
-------------------------------------------
🔥 3 Major Categories:
Creational Patterns – Object creation manage karte hain.

Structural Patterns – Class aur object ke relationships define karte hain.

Behavioral Patterns – Communication aur responsibilities handle karte hain.
--------------------------------------------------
✅ 1. Singleton Pattern (Creational)
Purpose:
Ensure ek hi object banega class ka.
--------------------------------------------------
🔍 Real-world Scenario:
Logging system (har part of app se same logger instance)

DB connection pool

App configuration (shared settings)
-----------------------------------------
🧑‍💻 Code (C#):

public class AppConfig
{
    private static AppConfig _instance;
    private static readonly object _lock = new object();

    private AppConfig() { }

    public static AppConfig Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new AppConfig();
                return _instance;
            }
        }
    }

    public string GetSetting(string key) => "value";
}
Use anywhere with: AppConfig.Instance.GetSetting("Key")
---------------------------------------------------
✅ 2. Factory Pattern (Creational)
Purpose:
Object creation ko encapsulate karta hai so client ko pata nahi hota kaunsa class instantiate ho raha hai.
----------------------------------------
🔍 Real-world Scenario:
Payment gateway (CreditCard, UPI, Wallet)

Shape drawing tool (Circle, Square, Triangle)
------------------------------------------
🧑‍💻 Code:

public interface IPayment { void Process(); }

public class CreditCardPayment : IPayment
{
    public void Process() => Console.WriteLine("CreditCard Payment");
}

public class UpiPayment : IPayment
{
    public void Process() => Console.WriteLine("UPI Payment");
}

public class PaymentFactory
{
    public static IPayment Create(string method)
    {
        return method switch
        {
            "credit" => new CreditCardPayment(),
            "upi" => new UpiPayment(),
            _ => throw new NotImplementedException()
        };
    }
}
Call: PaymentFactory.Create("upi").Process();
--------------------------------------------------
✅ 3. Builder Pattern (Creational)
Purpose:
Complex object ko step-by-step create karne ke liye.

🔍 Real-world Scenario:
Meal order (Burger, Fries, Drink)

Email creation (subject, body, attachments)
----------------
🧑‍💻 Code:

public class Email
{
    public string To, Subject, Body;

    public override string ToString() =>
        $"To: {To}, Subject: {Subject}, Body: {Body}";
}

public class EmailBuilder
{
    private Email _email = new Email();

    public EmailBuilder SetTo(string to)
    {
        _email.To = to;
        return this;
    }

    public EmailBuilder SetSubject(string subject)
    {
        _email.Subject = subject;
        return this;
    }

    public EmailBuilder SetBody(string body)
    {
        _email.Body = body;
        return this;
    }

    public Email Build() => _email;
}
------------------------------
Use:

var email = new EmailBuilder()
    .SetTo("user@example.com")
    .SetSubject("Hello")
    .SetBody("How are you?")
    .Build();
    -----------------------------------------------------
✅ 4. Adapter Pattern (Structural)
----------------------------------
Purpose:
Do incompatible interfaces ko compatible banata hai.

🔍 Real-world Scenario:
Legacy system + modern APIs integration

Charger adapter (3-pin to 2-pin)
----------------------------
🧑‍💻 Code:

public class OldPrinter
{
    public void PrintOld(string text) =>
        Console.WriteLine($"[OldPrint] {text}");
}

public interface IPrinter
{
    void Print(string text);
}

public class PrinterAdapter : IPrinter
{
    private OldPrinter _oldPrinter = new OldPrinter();
    public void Print(string text) => _oldPrinter.PrintOld(text);
}
------------------------------------------
Client can call:


IPrinter printer = new PrinterAdapter();
printer.Print("New way to print");
---------------------------------------------
✅ 5. Decorator Pattern (Structural)
Purpose:
Dynamically functionality add karna bina class ko modify kiye.

🔍 Real-world Scenario:
Add logging to service

Add compression/encryption to data stream

Pizza toppings!
----------------------
🧑‍💻 Code:

public interface IMessage
{
    string GetContent();
}

public class SimpleMessage : IMessage
{
    public string GetContent() => "Hello";
}

public class EncryptedMessage : IMessage
{
    private IMessage _message;
    public EncryptedMessage(IMessage message) => _message = message;

    public string GetContent() =>
        $"Encrypted({_message.GetContent()})";
}
---------------------
Use:

IMessage message = new EncryptedMessage(new SimpleMessage());
Console.WriteLine(message.GetContent()); // Encrypted(Hello)
--------------------------------------------
✅ 6. Strategy Pattern (Behavioral)
Purpose:
Algorithm ko encapsulate karna aur run-time pe choose karna.
--------------------------
🔍 Real-world Scenario:
Sorting (quick, bubble, merge)

Payment strategy (card, UPI, wallet)

Discount strategy in eCommerce

🧑‍💻 Code:

public interface IDiscountStrategy
{
    double GetDiscount(double amount);
}

public class FlatDiscount : IDiscountStrategy
{
    public double GetDiscount(double amount) => amount - 50;
}

public class PercentageDiscount : IDiscountStrategy
{
    public double GetDiscount(double amount) => amount * 0.9;
}

public class Bill
{
    private IDiscountStrategy _strategy;

    public Bill(IDiscountStrategy strategy) => _strategy = strategy;

    public void Generate(double amount) =>
        Console.WriteLine("Final: " + _strategy.GetDiscount(amount));
}
------------------------------------
Use:

var bill = new Bill(new PercentageDiscount());
bill.Generate(1000);
--------------------------------------------------------------------
✅ 7. Observer Pattern (Behavioral)
Purpose:
One-to-many dependency. One object changes → all subscribers notified.
---------------------------------------------------
🔍 Real-world Scenario:
Email/newsletter subscription

UI updates

Stock market tickers
-------------------------------------------------------
🧑‍💻 Code:

public interface ISubscriber
{
    void Update(string news);
}

public class NewsSubscriber : ISubscriber
{
    private string _name;
    public NewsSubscriber(string name) => _name = name;

    public void Update(string news) =>
        Console.WriteLine($"{_name} received: {news}");
}

public class NewsPublisher
{
    private List<ISubscriber> _subscribers = new();

    public void Subscribe(ISubscriber s) => _subscribers.Add(s);

    public void Notify(string news)
    {
        foreach (var s in _subscribers)
            s.Update(news);
    }
}
--------------------------------------------------
Use:

var publisher = new NewsPublisher();
publisher.Subscribe(new NewsSubscriber("Amit"));
publisher.Subscribe(new NewsSubscriber("Neha"));
publisher.Notify("C# 13 Released!");
---------------------------------------------------------
✅ 8. Mediator Pattern (Behavioral)
Purpose:
Communicating components talk via mediator instead of talking to each other directly.
----------------------------------------------------
🔍 Real-world Scenario:
Chat rooms

Air traffic control

UI elements coordination
-------------------------------------------
🧑‍💻 Code:

public interface IChatMediator
{
    void Send(string message, User sender);
}

public class ChatRoom : IChatMediator
{
    private List<User> _users = new();

    public void Register(User user) => _users.Add(user);

    public void Send(string message, User sender)
    {
        foreach (var u in _users)
            if (u != sender)
                u.Receive(message);
    }
}

public class User
{
    private IChatMediator _mediator;
    private string _name;

    public User(string name, IChatMediator mediator)
    {
        _name = name;
        _mediator = mediator;
    }

    public void Send(string message) => _mediator.Send(message, this);
    public void Receive(string message) =>
        Console.WriteLine($"{_name} received: {message}");
}
---------------------------------------------
Use:

var room = new ChatRoom();
var u1 = new User("Alice", room);
var u2 = new User("Bob", room);
room.Register(u1);
room.Register(u2);

u1.Send("Hello Bob!");
----------------------------------------------------
🔚 Wrap-up for now!
Bhai ye to bas base level ke sabse most useful patterns the. Tere liye:

Har pattern ka use case clear

Real-world relatable hai

Code ready to use hai

Interview bolne layak definitions mil gayi

