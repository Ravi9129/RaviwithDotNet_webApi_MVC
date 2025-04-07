using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 9_ConfigureServices() extension
    {
        
    }
}
-----------------------------------
ConfigureServices() extension ki baat kar raha hai na — jisme hum services ko modular bana ke alag class me inject 
karte hain? Ye cheez bade projects me clean architecture ke liye must hai.

Chal full explain karta hoon real-world tarike se, bina table ke.
--------------------------------------------------------
🔥 Starting Point — What is a Configure Service Extension?
🔧 Definition:
ConfigureServices(IServiceCollection services) ke andar ka sara code ek point pe heavy ho jata hai.
Usko modular banane ke liye hum custom extension methods banate hain jaise:

services.AddMyCustomServices();
💡 Real-World Analogy:
Soch le ki Program.cs ya Startup.cs ek building hai.
Tere alag alag departments (logging, db, repo, auth) ko alag floors me shift karna hai.
Har floor ka setup extension method ban jata hai.
-------------------------------------------
🛠️ Implementation Steps
✅ 1. Create a static class:

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IMyService, MyService>();
        services.AddTransient<IOtherService, OtherService>();
        return services;
    }
}
-----------------------------------------
✅ 2. Use it in Program.cs or Startup.cs

builder.Services.AddCustomServices();
🎯 Benefits:
Code is modular — har feature ka apna extension.

Readability — Startup/Program clean dikhata hai.

Reusable — Extension ko multiple projects me reuse kar sakte ho.

Testability — Alag alag extensions ko unit test me verify kar sakte ho.
-----------------------------------------------------
🔄 Example: Real-World Use Case
🔐 Create a ServiceCollectionExtensions.cs for Authentication

public static class AuthServiceExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // JWT options here
            };
        });

        return services;
    }
}
---------------------------------------
🔧 Use in Program.cs

builder.Services.AddJwtAuthentication(builder.Configuration);
---------------------------------------------------------------------
🧪 Bonus — Split into Feature-Level Extensions
You can create:

AddRepositoryServices()

AddApplicationServices()

AddInfrastructureServices()

AddDatabaseContext()

AddLoggingServices()

AddCustomMiddlewares()

Isse teri architecture onion-layer ya clean architecture ke bilkul close ho jaati hai.
--------------------------------------------------------
🔚 Summary:
Create static class

Write extension method this IServiceCollection services

Keep your DI logic modular

Call that method in Program.cs or Startup.cs

