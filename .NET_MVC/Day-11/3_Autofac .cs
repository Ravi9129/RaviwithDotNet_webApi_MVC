using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 3_Autofac 
    {
        
    }
}
---------------------------------------------------------------
Autofac ek powerful Dependency Injection (DI) container hai jo .NET applications mein advanced control aur flexibility deta hai beyond the default .NET Core DI container.

🔥 Why Use Autofac Over Default DI?
Constructor injection + property injection

Lifetime scopes (child containers)

Module-based registration (clean code)

Interception, decorators, and AOP-style features

Convention-based registration (register all types by interface, etc.)
----------------------------------------------------------------
✅ Step-by-Step: Use Autofac in ASP.NET Core
🧱 Step 1: Install Package

dotnet add package Autofac.Extensions.DependencyInjection
-------------------------------------------------------
🧱 Step 2: Modify Program.cs or Startup.cs
👉 In .NET 6 / 7 / 8 (Program.cs)

using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Replace default service provider with Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register your services in the Autofac container
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<MyService>().As<IMyService>();
    containerBuilder.RegisterModule(new MyCustomModule());
});

var app = builder.Build();

// Normal app setup
app.MapControllers();
app.Run();
----------------------------------------
✅ Step 3: Use the Service

public class HomeController : Controller
{
    private readonly IMyService _myService;

    public HomeController(IMyService myService)
    {
        _myService = myService;
    }

    public IActionResult Index()
    {
        var message = _myService.GetMessage();
        return Content(message);
    }
}
-----------------------------------------------------
📦 Custom Module (Optional but Clean)

public class MyCustomModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MyService>().As<IMyService>().InstancePerLifetimeScope();
        // More registrations...
    }
}
-----------------------------------------------------------
💡 Lifetime Scopes in Autofac
Autofac Lifetime Method	Equivalent in .NET DI
SingleInstance()	AddSingleton()
InstancePerLifetimeScope()	AddScoped()
InstancePerDependency()	AddTransient()
----------------------------------------------------------
⚡ Bonus: Register All Services Automatically

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
        .Where(t => t.Name.EndsWith("Service"))
        .AsImplementedInterfaces();
});