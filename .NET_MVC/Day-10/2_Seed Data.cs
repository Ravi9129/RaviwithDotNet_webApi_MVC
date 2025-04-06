using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 2_Seed Data
    {
        
    }
}
--------------------------------------
Seed Data ko — ye concept har real-world project me use hota hai jab application start hote hi kuch default data database me insert karna padta hai (jaise: roles, categories, admin user etc.)

🔍 Seed Data Kya Hota Hai?
Jab app ya database first time ban raha hota hai, to default/sample data automatically insert karne ko Seeding kehte hain.

Jaise:

Pehla admin user

Default roles (Admin, User)

Predefined categories (Electronics, Books)

Initial product list
-------------------------------------------------
🧠 Real Life Example
Socho ek E-Commerce site hai. Tum chahte ho ki jab app chalaye, tab kuch categories already DB me ho:
[
  { "Id": 1, "Name": "Mobiles" },
  { "Id": 2, "Name": "Books" },
  { "Id": 3, "Name": "Clothes" }
]
Isse har baar manually insert karne ki zarurat nahi padti.
---------------------------------------------------------
⚙️ .NET Core Entity Framework Me Seeding Kaise Karte Hain?
--------------
Step 1: Create a Model

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}
--------------------------------------------
Step 2: Add DbSet to DbContext

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Mobiles" },
            new Category { Id = 2, Name = "Books" },
            new Category { Id = 3, Name = "Clothes" }
        );
    }
}
HasData() method seed data insert karne ke liye use hota hai.
----------------------------------------
Step 3: Add Migration

dotnet ef migrations add SeedCategory
------------------------------------------
Step 4: Update Database

dotnet ef database update
Iske baad data automatically insert ho jayega jab DB create/update hoga.
------------------------------------
🔐 Seed Admin User (Thoda Advance)

modelBuilder.Entity<User>().HasData(
    new User { Id = 1, Username = "admin", Role = "Admin", Password = "1234" }
);
----------------------------------------------------------
🎯 Kab Use Karte Hain?
Situation	Seed Data
Dev/Test environment	✅ Zaroori hai default data ke liye
Demo project banate waqt	✅ Look & feel ke liye
Production (admin role, etc.)	✅ Pehla bar banate waqt hi

--------------------------------------------
🔄 Important Notes:
Seed data insert hota hai only once, agar woh primary key pehle se hai to ignore karega.

HasData() ko OnModelCreating() ke andar likhna padta hai

Use migrations ke through DB me apply hota hai.
--------------------------------------------------------------
✅ BONUS: Runtime Seeding (Program.cs se)
------------------------
Agar tum chahte ho ki data runtime pe insert ho:

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new Category { Name = "Mobiles" },
            new Category { Name = "Books" }
        );
        db.SaveChanges();
    }
}
