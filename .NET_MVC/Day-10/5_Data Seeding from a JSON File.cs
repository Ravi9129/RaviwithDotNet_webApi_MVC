using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 5_Data Seeding from a JSON File
    {
        
    }
}
---------------------------------------
Data Seeding from a JSON File in .NET — matlab tumhare paas ek JSON file hai jisme pehle se data pada hai, aur tum chahte ho ki woh data automatically database me seed ho jaye jab app start hoti hai.
------------------------------------------------
🔥 Real World Scenario:
🗂 JSON File (Example: data/users.json)

[
  {
    "FirstName": "Raj",
    "LastName": "Verma",
    "Email": "raj@example.com"
  },
  {
    "FirstName": "Neha",
    "LastName": "Sharma",
    "Email": "neha@example.com"
  }
]
✅ Step-by-Step Guide
----------------------------------------------
✅ Step 1: Model Class

public class UserData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
----------------------------------------------------------
✅ Step 2: Add the JSON file to your project
📁 wwwroot/data/users.json
OR
📁 SeedData/users.json

Make sure Copy to Output Directory → Copy if newer
---------------------------
✅ Step 3: Read & Seed in DbContext
You can do this inside your DbContext.OnModelCreating() or a separate static method.
-----------------------------------------------------
✨ Option 1: Seed inside DbContext constructor or Seed method

public static class JsonDataSeeder
{
    public static void SeedUsersFromJson(AppDbContext context, IWebHostEnvironment env)
    {
        var jsonPath = Path.Combine(env.ContentRootPath, "SeedData/users.json");
        if (!File.Exists(jsonPath)) return;

        var jsonData = File.ReadAllText(jsonPath);
        var users = JsonSerializer.Deserialize<List<UserData>>(jsonData);

        if (!context.Users.Any())
        {
            foreach (var user in users)
            {
                context.Users.Add(new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }

            context.SaveChanges();
        }
    }
}
----------------------------------------------------
✅ Step 4: Call Seeder in Program.cs
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    JsonDataSeeder.SeedUsersFromJson(db, env);
}
-----------------------------------------------------
💡 Bonus: Seed Roles/Users from JSON with Identity?
Yes bro — same method, just deserialize into IdentityUser, IdentityRole types and use UserManager/RoleManager.


