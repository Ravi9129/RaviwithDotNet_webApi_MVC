using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 7_pros and cons of respositry pattern
    {
        
    }
}
------------------------------------------
Repository pattern ek common architectural pattern hai jo data access logic ko encapsulate karta hai, so that aapka business logic (Service layer) data source se loosely coupled ho.

✅ Pros (Advantages) of Repository Pattern
1️⃣ Separation of Concerns (SoC)
Business logic (Service Layer) aur Data access logic (Repository) ko separate karta hai.

Maintainable code structure.
----------------------------------------------------------
2️⃣ Unit Testing Friendly
Repositories ko mock karke easily test likh sakte ho.


var mockRepo = new Mock<IProductRepository>();
3️⃣ Loose Coupling
Service ya Controller ko DB context ka direct access nahi dena padta.
--------------------------------------------
4️⃣ Centralized Data Logic
Data access logic ek hi jagah rehta hai → Consistent queries, no duplication.
----------------------------------------------
5️⃣ Multiple Data Sources Support
Repository se data SQL DB, NoSQL, CSV, JSON, etc. kisi bhi source se fetch kiya ja sakta hai.
----------------------------------------------
6️⃣ Domain Driven Design (DDD) Friendly
Repository ek aggregate root ki tarah kaam karta hai, perfect fit for DDD approach.
---------------------------------------------
❌ Cons (Disadvantages) of Repository Pattern
1️⃣ Over-Engineering for Simple Apps
Chhoti applications ke liye unnecessary complexity introduce karta hai.
-----------------------------------------------
2️⃣ Extra Layer = More Code
Repository + Interface + Service = More files = More boilerplate.
------------------------------------------
3️⃣ Duplication with EF Core
EF Core khud ek repository/UnitOfWork jaisa kaam karta hai. Toh custom repository kabhi-kabhi redundant lagta hai.
---------------------------------------------
4️⃣ Query Limitations
Complex queries ke liye har method ko expose karna padta hai. Custom methods banana padta hai.


Task<List<Product>> GetProductsWithDiscountAsync();
------------------------------------------
5️⃣ Can Hide EF Features
Advanced EF features like .Include, .ThenInclude, .AsSplitQuery() hide ho jaate hain abstraction me.
-----------------------------------
🧠 When To Use?
✅ Large applications
✅ Domain-Driven Design
✅ You want testability
✅ Multiple data sources
✅ Clean architecture
-----------------------------------------------
🚫 When NOT To Use?
❌ Simple CRUD apps
❌ EF Core-only minimal APIs
❌ When performance is critical and abstraction slows things down

