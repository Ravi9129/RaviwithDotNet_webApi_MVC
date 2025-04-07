using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLID principles.Day-1
{
    public class 5_Infrastructure Layer
    {
        
    }
}
-----------------------------------------
Infrastructure Layer in Clean Architecture — full detailed explanation ke saath, 
real-world examples aur practical scenarios. Jaise tere baaki topics explain kiye the, 
waise hi — bina kisi table format ke, seedha dil se 😎
---------------------------------------------------------
🔧 What is the Infrastructure Layer?
Infrastructure Layer Clean Architecture ka bahari layer hota hai — ye sari implementation details ko handle karta hai:

"Ye wo layer hai jahan tera Core bolta hai 'Mujhe yeh chahiye', aur Infrastructure bolta hai 'Lo bhai, yeh lo!'"
---------------------------------------------
Iska kaam hai:

Data access (Entity Framework, Dapper, MongoDB, etc.)

External services (SMTP, payment gateways, file systems, cloud storage)

Implement interfaces from Core

Configure DB context, logging, caching, email, etc.
----------------------------------------------------------
🧱 Infrastructure Layer ke Components:
1. Repository Implementations
Agar core me IOrderRepository hai, toh is layer me OrderRepository likhoge jo actual me SQL ya EF use karega.
-----------------------------------------------------
Core Interface:

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task SaveAsync(Order order);
}
-----------------------------------------------------
Infrastructure Implementation (EF Core):

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task SaveAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}
---------------------------------------------------
2. DbContext
Is layer me AppDbContext hota hai, jo EF Core ka context hai.

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
Ye context register hota hai DI me Startup ya Program.cs me.
---------------------------------------------------
3. External Services Implementations
Agar core me IEmailSender interface hai, toh infrastructure me uska implementation hota hai — SMTP, SendGrid, etc.

public class SmtpEmailSender : IEmailSender
{
    public Task SendAsync(string to, string subject, string body)
    {
        // SMTP code here
        return Task.CompletedTask;
    }
}
-------------------------------------
4. File Storage, Cloud, Caching, Logging
File upload/download => Local or Azure Blob

Caching => Redis, MemoryCache

Logging => Serilog, NLog

Payment Gateways => Stripe, RazorPay

Infrastructure ye sab handle karta hai — core bilkul bhi aware nahi hota in sab cheezon se.
-----------------------------------------------
🔁 Real-World Scenario: Invoice App
Suppose teri app invoices send karti hai:

Core me business logic hai invoice banane ka

Core bolta hai "Invoice ban gaya, ab email bhejna hai"
---------------------
Core has:

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body);
}
Infrastructure me SMTP ya SendGrid ka implementation likh diya

DI ke through Core ko chala diya
----------------
Result?

Core layer change nahi hui, lekin tu kal SMTP se SendGrid me migrate bhi kar de toh koi dikkat nahi 🔁
-----------------------------------------
📁 Typical Folder Structure

/Infrastructure
  /Persistence
    - AppDbContext.cs
    - OrderRepository.cs
  /Services
    - SmtpEmailSender.cs
    - AzureBlobStorageService.cs
  /Logging
    - SerilogLogger.cs
  InfrastructureModule.cs
  ------------------------------------------------
InfrastructureModule.cs isliye banate hain taaki saari services yahin se register ho jaayein DI me:

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddTransient<IEmailSender, SmtpEmailSender>();

        return services;
    }
}
--------------------------------------
🎯 Why Infrastructure Layer?
Decoupling: Core doesn't know EF, SMTP, Blob, or Serilog

Flexibility: Replace one tech with another (e.g. SQL → MongoDB)

Testability: Infra layer can be mocked

Separation of Concerns: Code is clean, maintainable
---------------------------------------------------
🧠 Interview Style Explanation
"Infrastructure layer contains all the implementation details required to fulfill the contracts defined in the core layer.
 These include data access, file storage, email services, and other external integrations.
  It follows the Dependency Inversion Principle by depending on abstractions defined in the core."
-------------------------------------------
✅ Summary
Core defines "what" needs to be done

Infrastructure defines "how" it's done

Infrastructure fulfills contracts (interfaces) defined by core

It contains EF DbContext, third-party service integrations, and config setups