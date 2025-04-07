using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLID principles.Day-1
{
    public class 3_Clean Architecture
    {
        
    }
}
-------------------------------------------
Clean Architecture ka full bhandar kholte hain — interview-level, real-world level, 
production-level sab kuch samjhaunga step by step, bilkul detail me, bina table format ke.
Tu agar isse ek baar achhe se samajh gaya, 
to interviews me aag laga dega aur apne projects me bhi production-ready structure bana payega 🚀
-------------------------------------------------------
🔰 What is Clean Architecture?
Clean Architecture ek software architecture pattern hai jo code ko layers me divide karta hai so that:

Code maintainable ho

Reusable ho

Independent ho kisi bhi framework, database, UI se

🧠 Core idea: Business logic (core) ko kisi bhi outer technology (UI, DB, Web, etc.) se alag rakhna
------------------------------------------------------
💥 Real-World Example:
Soch:

Tu ek e-commerce app bana raha hai.

Kal ko tu Web UI hata ke Blazor ya Angular UI lagana chahta hai.

Ya SQL ki jagah NoSQL lagana chahta hai.

Agar tune Clean Architecture use kiya hai, to tu sirf outer layer change karega. Business rules untouched rahenge 🔥
------------------------------------------------------------------
🔧 Layered Structure (Andar se bahar ki taraf):
1️⃣ Entities (Core Business Logic)
Pure POCOs (classes)

Domain rules

Independent of any technology
--------------------------------------------------------
Example:

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public void ApplyDiscount(decimal percent)
    {
        Price -= Price * percent / 100;
    }
}
No Entity Framework, no JSON, no HTTP — just pure logic.
--------------------------------------------------
2️⃣ Use Cases (Application Layer / Services)
Orchestrates business rules

No DB code

Calls repositories via interfaces

Example:
---------------------------
public class CreateOrderUseCase
{
    private readonly IOrderRepository _repo;

    public CreateOrderUseCase(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(Order order)
    {
        // Validate, calculate, etc.
        order.Total = order.Items.Sum(i => i.Quantity * i.Price);
        await _repo.Add(order);
    }
}
-----------------------------------------
3️⃣ Interfaces (Ports)
Interfaces for Repositories, Services
--------------------------------------------
Input/Output boundaries


public interface IOrderRepository
{
    Task Add(Order order);
    Task<Order> GetById(int id);
}
--------------------------------------------
4️⃣ Infrastructure (DB, APIs, File systems, etc.)
Implements interfaces from core

Uses EF Core, Dapper, Mongo, etc.
------------------------------------
public class EfOrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public EfOrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order> GetById(int id)
    {
        return await _context.Orders.FindAsync(id);
    }
}
---------------------------------------------------------
5️⃣ Web/API/UI Layer (Framework layer)
Accepts user input, calls use cases

No business logic here

Can be ASP.NET Core MVC, API, Blazor, etc.
------------------
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _useCase;

    public OrdersController(CreateOrderUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        await _useCase.Execute(order);
        return Ok();
    }
}
--------------------------------------------------
🚦 Dependency Rule (Very Important)
"Dependencies always point inward."

Outer layer can depend on inner layer.

Inner layers must never depend on outer layers.
------------------------
So:

Controller → UseCase = ✅

UseCase → Controller = ❌

UseCase → Repo Interface = ✅

Repo → EF Core = ✅
-------------------------------------------------
🎯 Real Use Cases
1. Plugin Architecture
You can add logging, DBs, file storage, email systems as plugins — without touching the core logic.

2. Swappable UI or API
Want to switch from MVC to Blazor or React? No need to touch your business logic.

3. Testability
You can test UseCases and Entities without touching DB or APIs.
----------------------------------------------
💡 SOLID Principles Applied
S: Each layer has single responsibility.

O: New DBs, UI can be added via open/closed principle.

L: Interfaces can be swapped without breaking.

I: Interfaces are small — like IOrderRepository.

D: Repositories injected into UseCases (dependency inversion).
------------------------------------------------
🧠 Interview Ready Explanation
“Clean Architecture is a layered architecture where core business logic is isolated from technical concerns like DBs, 
frameworks, or UI. This improves testability, scalability, and makes the codebase adaptable to change. 
It follows SOLID principles and emphasizes inward dependency flow.”
----------------------------------
🔧 Clean Architecture Folder Structure (Common)

/src
  /Domain            --> Entities, ValueObjects, Enums
  /Application       --> UseCases, Interfaces, DTOs
  /Infrastructure    --> EF Core, external APIs, file system
  /WebAPI            --> Controllers, Filters, Middleware
  ------------------------------------------
✅ Bonus: Use Clean Architecture in .NET 8
Use Minimal APIs in outer layer

Use MediatR for CQRS in Application layer

Use EF Core in Infrastructure

Use AutoMapper for DTO mapping

