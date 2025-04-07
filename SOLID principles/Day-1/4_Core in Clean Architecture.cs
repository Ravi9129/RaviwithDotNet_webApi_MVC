using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLID principles.Day-1
{
    public class 4_Core in Clean Architecture
    {
        
    }
}
---------------------------------------------------
Clean Architecture ka "Core" samjhte hain — matlab ki Core Layer kya hoti hai, kyun hoti hai,
 usme kya rehta hai, kaise likhte hain, real world me kya faayda hota hai, sab kuch detail me, real world scenario ke saath.
-------------------------------------------------
🔰 What is the Core in Clean Architecture?
Core ekdum center wala layer hai Clean Architecture ka.

💡 Ye wo jagah hai jahan Business Rules hote hain — jo tere application ka actual "dimaag" hai.

Kisi framework se dependent nahi

Kisi DB ya API se dependent nahi

Sirf aur sirf Business Entities, Rules, aur Interfaces hote hain
-------------------------------------------
🧱 Core Layer ke Components:
1. Entities (Business Models)
Ye wo classes hoti hain jo tera business represent karti hain.

Example: E-commerce Product Entity

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public void ApplyDiscount(decimal percentage)
    {
        Price -= (Price * (percentage / 100));
    }
}
No Entity Framework attributes

No [JsonProperty] or [Http...] attributes

Just pure logic ✅
-------------------------------------------
2. Value Objects
Entities ke parts hote hain jo logically ek unit hain.
---------------------------
Example: Money Value Object

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
}
---------------------------------------
3. Interfaces (Ports)
Ye define karte hain ki core ko kis kis cheez ki zarurat hai

Lekin implementation nahi dete
-------------------------
Example:

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task SaveAsync(Order order);
}
------------------------------------------
🧠 Tera Core sirf bolta hai "Mujhe IOrderRepository chahiye"
Wo yeh nahi jaanta ki tum SQL use kar rahe ho ya MongoDB — that's the power 💪
---------------------------------
4. Domain Services (optional)
Agar koi business logic kisi ek entity ke andar fit nahi ho rahi, toh usse ek service me daal do.

Example:

public interface IPricingService
{
    decimal CalculatePrice(Product product);
}
-----------------------------------------
💥 Real-World Scenario:
Maan le ek app bana raha hai — Car Rental system.

Ab isme business rules hain:

User ek car rent kar sakta hai

Agar user VIP hai toh usse 10% discount milega

Agar user ne 5+ cars rent ki hain, toh usse loyalty bonus milega

Ye sab rules kaha aayenge? — Tere Core me, kisi bhi controller me nahi.
------------------------------------
public class CarRentalService
{
    public decimal CalculateRental(User user, Car car)
    {
        decimal price = car.BasePrice;

        if (user.IsVIP)
            price *= 0.90m;

        if (user.RentedCarsCount > 5)
            price -= 50;

        return price;
    }
}
Yeh logic testable bhi hai, reusable bhi hai, scalable bhi hai.
---------------------------------------
🚀 Why Core is Powerful
Feature	Benefit
Independent	Kisi bhi framework se free
Testable	DB ke bina test possible
Reusable	Web, Desktop, Mobile sab me use ho sakta
Maintainable	Changes easy hain, tight coupling nahi

----------------------------------------------------
📁 Typical Core Folder Structure
/Core
  /Entities
    - Product.cs
    - Order.cs
  /Interfaces
    - IOrderRepository.cs
  /Services
    - PricingService.cs
  /ValueObjects
    - Money.cs
    --------------------------------------------------
🧠 Interview Explanation
"Core layer is the heart of a Clean Architecture application. It contains entities,
 business logic, and interfaces that define contracts for interaction. It is completely independent of 
 any external libraries, frameworks, or infrastructure, making it reusable, testable, and future-proof."
------------------------------------------------
🔧 Testing Core Logic
Test core logic directly without involving DB:

[Fact]
public void VIPUser_Should_Get_Discount()
{
    var user = new User { IsVIP = true };
    var car = new Car { BasePrice = 1000 };
    var service = new CarRentalService();

    var result = service.CalculateRental(user, car);

    Assert.Equal(900, result);
}
-----------------------------------------
Bhai, Core likhna matlab teri app ka "Dimaag" banana. Frameworks, APIs, DBs aate jate rahenge — lekin tera dimaag wahi rahega 😄