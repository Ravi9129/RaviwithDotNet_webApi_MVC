using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLID principles.Day-1
{
    public class 1_Solid_principle
    {
        
    }
}
---------------------------------
🧠 Pehle Basic Overview de le:
S.O.L.I.D ek acronym hai 5 object-oriented design principles ka jo maintainable, scalable, 
aur loosely coupled systems banane ke liye follow kiye jaate hain.

✅ 1. Single Responsibility Principle (SRP)
Definition:
A class should have only one reason to change. Matlab, ek class sirf ek kaam kare.
-----------------------------------------------
🔍 Real-World Example:
Soch le tu ek class banata hai InvoiceManager:

public class InvoiceManager
{
    public void CreateInvoice() { /* logic */ }
    public void SaveToDb() { /* logic */ }
    public void SendEmail() { /* logic */ }
}
-------------------------------------------
Yahan problem ye hai ki:

Invoice banana bhi yahi kar rahi hai

DB me save bhi yahi kar rahi hai

Email bhi yahi bhej rahi hai

Violation of SRP!
--------------------------------------------
✅ Correct Way:
Split kar dete hain:

public class InvoiceCreator
{
    public void CreateInvoice() { }
}

public class InvoiceRepository
{
    public void SaveToDb() { }
}

public class EmailSender
{
    public void SendEmail() { }
}
Ab har class ka ek hi reason hoga change hone ka.
----------------------------------------------------------------
✅ 2. Open/Closed Principle (OCP)
Definition:
Classes should be open for extension but closed for modification.
--------------------------------
🔍 Real-World Example:
Soch le ek shipping calculator bana hai:

public class ShippingCalculator
{
    public double Calculate(string type)
    {
        if (type == "Standard") return 50;
        if (type == "Express") return 100;
        return 0;
    }
}
Agar kal ko naye shipping types aaye, tu baar baar Calculate method ko modify karega — violation of OCP.
-----------------------------------------
✅ Correct Way (Using Polymorphism):

public interface IShipping
{
    double Calculate();
}

public class StandardShipping : IShipping
{
    public double Calculate() => 50;
}

public class ExpressShipping : IShipping
{
    public double Calculate() => 100;
}

public class ShippingCalculator
{
    public double Calculate(IShipping shipping) => shipping.Calculate();
}
Ab naye shipping type aayen to tu bas naye class likh, existing code untouched rahega.
----------------------------------------------------------------------
✅ 3. Liskov Substitution Principle (LSP)
Definition:
Subclasses should be substitutable for their base classes without breaking the behavior.
--------------------------
🔍 Real-World Example:
Tu ek Bird base class banata hai aur subclass me Penguin inherit karta hai:

public class Bird
{
    public virtual void Fly() { Console.WriteLine("Flying"); }
}

public class Penguin : Bird
{
    public override void Fly() { throw new Exception("Penguins can't fly!"); }
}
Violation of LSP — subclass ne parent ka contract tod diya.
------------------------------
✅ Correct Way:
Break abstraction into meaningful interfaces:

public interface IBird { }
public interface IFlyable
{
    void Fly();
}

public class Sparrow : IBird, IFlyable
{
    public void Fly() { Console.WriteLine("Sparrow flying"); }
}

public class Penguin : IBird
{
    // No fly method
}
Ab koi bhi code jo IFlyable use kare, usse surety hai ki woh fly karega.
-------------------------------------------------------------------------------------
✅ 4. Interface Segregation Principle (ISP)
Definition:
Clients should not be forced to depend on interfaces they don’t use.
-----------------------------------------------
🔍 Real-World Example:
Tu IMachine interface banata hai:

public interface IMachine
{
    void Print();
    void Scan();
    void Fax();
}
-------------------------------------------
Aur class OldPrinter bana raha hai jo fax support nahi karti:

public class OldPrinter : IMachine
{
    public void Print() { }
    public void Scan() { }
    public void Fax() => throw new NotImplementedException();
}
Violation of ISP — OldPrinter ko fax implement karna pad raha bina use kiye.
-----------------------------------------------
✅ Correct Way:
Split interfaces:

public interface IPrinter { void Print(); }
public interface IScanner { void Scan(); }
public interface IFax { void Fax(); }

public class OldPrinter : IPrinter, IScanner { /* only what it supports */ }
-------------------------------------------------
✅ 5. Dependency Inversion Principle (DIP)
Definition:
High-level modules should not depend on low-level modules. Both should depend on abstractions.
-------------------------
🔍 Real-World Example:
Soch le ReportGenerator class directly ExcelExporter pe dependent hai:

public class ExcelExporter
{
    public void Export() { }
}

public class ReportGenerator
{
    private ExcelExporter _exporter = new ExcelExporter();
    public void Generate() => _exporter.Export();
}
Violation of DIP — tight coupling with ExcelExporter.
-----------------------------------------------------------------
✅ Correct Way:
Use interface abstraction:

public interface IExporter
{
    void Export();
}

public class ExcelExporter : IExporter
{
    public void Export() { }
}

public class PdfExporter : IExporter
{
    public void Export() { }
}

public class ReportGenerator
{
    private readonly IExporter _exporter;
    public ReportGenerator(IExporter exporter)
    {
        _exporter = exporter;
    }

    public void Generate() => _exporter.Export();
}
Ab ReportGenerator kisi bhi exporter se kaam chala sakta hai — Excel, PDF, etc. Loose coupling achieved.
----------------------------------------------
🔐 Interview Me Kaise Bolenge?
“I always design systems using the SOLID principles. It helps me write clean, maintainable, testable, and extensible code. 
For example, instead of modifying a class again and again for every new condition, 
I prefer extending it — following the Open/Closed principle. Similarly, 
I decouple dependencies using interfaces and constructor injection — ensuring the Dependency Inversion Principle is applied.”
--------------------------------------------------------------------
🔨 Real Project Use Case
Imagine tu ek eCommerce app bana raha:

SRP: OrderManager sirf order handle kare, PaymentManager sirf payment.

OCP: Shipping options ko extend kar sake bina code modify kiye.

LSP: DiscountStrategy subclasses sab same contract follow karen.

ISP: Payment gateways jo sirf card support karte unko wallet/fraud detection features na force kiya jaye.

DIP: OrderProcessor ko PaymentProcessor ke interface se decouple karke multiple payment providers se connect kar sake.

