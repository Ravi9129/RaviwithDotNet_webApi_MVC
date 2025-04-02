using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 13_Model Class
    {
        
    }
}
----------------------------
Model Class in ASP.NET Core 🚀
1️⃣ Model Class Kya Hai?
🔹 Model Class ek C# class hoti hai jo data structure define karti hai.
🔹 Ye data ko represent karti hai aur mostly database tables se mapping ke liye use hoti hai.
🔹 Models ka use data validation, business logic aur data transfer ke liye hota hai.
-----------------------------------------------------
2️⃣ Simple Example of Model Class
Maan lo ek E-commerce website hai jisme ek Product model define karna hai.
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
📌 Yahaan Product class ek model hai jo ek product ka structure define karti hai.
------------------------------------------------
3️⃣ Model Class Real-Life Use Case
✅ 1. Model Class ko Controller me use karna

[HttpGet("product/{id}")]
public IActionResult GetProduct(int id)
{
    var product = new Product
    {
        Id = id,
        Name = "Laptop",
        Price = 75000,
        Category = "Electronics"
    };

    return Ok(product);
}
📌 Yahaan model class ka object create karke uska data return kiya gaya hai.
----------------------------------------------------
✅ 2. Model Class ko Database se Connect karna (Entity Framework Core ke saath)

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
📌 Yahaan DbContext ka use kiya gaya hai jo Product model ko database table se map karega.
------------------------------------------------
4️⃣ Model Class ke Benefits
✔ Data ko Structure Provide Karta Hai – Model class clear aur maintainable code likhne me madad karti hai.
✔ Validation aur Business Logic – Isme validation attributes laga sakte hain.
✔ Entity Framework ke saath Integrate hota hai – Model classes database tables se mapping ke liye use hoti hain.
✔ MVC Architecture ka Part Hai – M in MVC (Model-View-Controller) Model hota hai.
-----------------------------------------
5️⃣ Model Class me Validation ka Use
ASP.NET Core me Data Annotations ka use karke model validation kar sakte hain.
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000")]
    public decimal Price { get; set; }

    [Required]
    public string Category { get; set; }
}
📌 Yahaan Required, StringLength, aur Range attributes ka use karke validation lagayi gayi hai.
--------------------------------------------------------------
6️⃣ Model Class Kab Use Karein?
✔ Jab database se data fetch aur store karna ho.
✔ Jab API endpoints me data exchange karna ho.
✔ Jab data validation aur business rules implement karni ho.
--------------------------------------------------
7️⃣ Conclusion
✅ Model Class ek essential part hai ASP.NET Core applications ka.
✅ Ye data ka structure define karti hai aur database mapping me help karti hai.
✅ Validation aur business logic ko maintain karne me bhi useful hoti hai.