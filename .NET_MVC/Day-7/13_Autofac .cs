using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-7
{
    public class 13_Autofac 
    {
        
    }
}
-------------------------------
Autofac ki — .NET me ek advanced Dependency Injection container hai jo built-in DI se zyada flexible aur powerful features deta hai.

🔍 Autofac kya hai?
Autofac ek Inversion of Control (IoC) container hai jo .NET applications me Dependency Injection ka kaam karta hai.

.NET ke default DI system se zyada features deta hai jaise:

Property injection

Module-based registration

Lifetime scopes

Conditional bindings

Open generic support

Constructor selection, etc.
---------------------------------------------------------
🛠 Step-by-step setup (ASP.NET Core ke saath)
-------------------------------------------
Step 1: NuGet Packages Install karo
Install-Package Autofac.Extensions.DependencyInjection
-----------------------------------------------
Step 2: Program.cs me setup
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Autofac ko batana hai ki use is app ke liye use karna hai
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register karo dependencies Autofac ke through
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<MyService>().As<IMyService>().InstancePerDependency();
});

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();
app.Run();
------------------------------------------------
Step 3: Controller me use karo
public class HomeController : Controller
{
    private readonly IMyService _myService;

    public HomeController(IMyService myService)
    {
        _myService = myService;
    }

    public IActionResult Index()
    {
        var msg = _myService.GetMessage();
        return Content(msg);
    }
}
---------------------------------------------
🧠 Example Scenario (Real Life)
Suppose tumhara e-commerce app hai:
IProductService → ProductService

ICartService → CartService

ILoggerService → FileLoggerService

Autofac ke through, tum ye sab services dynamically register kar sakte ho.
---------------------------------------------------------
🔁 Lifetime Scope (Autofac Style):

containerBuilder.RegisterType<MyService>().As<IMyService>().InstancePerLifetimeScope();
Lifetime	Autofac Method
Transient	.InstancePerDependency()
Scoped (per request)	.InstancePerLifetimeScope()
Singleton	.SingleInstance()
---------------------------------
💡 Bonus Feature: Register All Classes of Interface
containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
    .Where(t => t.Name.EndsWith("Service"))
    .AsImplementedInterfaces();
    ----------------------------------------------------
🎯 Jab tumhe use karna chahiye:
Jab default DI system se kaam na ban raha ho

Jab tum plugin-based ya module-based architecture bana rahe ho

Jab tum Aspect-Oriented Programming jaise advanced concepts chahte ho