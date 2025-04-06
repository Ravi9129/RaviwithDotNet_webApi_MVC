using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 11_DbContext 
    {
        
    }
}
------------------------------------------------------
DbContext ASP.NET Core / Entity Framework Core ka dil hai ❤️ — iska kaam hota hai database se baat karna. Iske through tu database me data insert, update, read, delete sab kuch kar sakta hai.

🔧 Real Life Analogy:
Soch le DbContext = Bridge jo tera code aur database ke beech connection banata hai.

🔥 Real-World Example: Blog App
Tere paas ek blog app hai, jisme tu blog posts save karta hai.
-----------------------------------------------------------------------
🔹 Step 1: Pehle Model Bana

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
----------------------------------------------
🔹 Step 2: Create DbContext Class

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
}
📌 DbSet<BlogPost> ka matlab — BlogPosts table banegi aur usme BlogPost model ke hisaab se data hoga.
----------------------------------------------------
🔹 Step 3: Register DbContext in Program.cs

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
⚠️ Connection string appsettings.json me define hoti hai.
---------------------------------------------------------------
🔹 Step 4: appsettings.json me connection string

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BlogDB;Trusted_Connection=True;"
}
------------------------------------------------------
🔹 Step 5: Use DbContext in Controller

public class BlogController : Controller
{
    private readonly AppDbContext _context;

    public BlogController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var posts = _context.BlogPosts.ToList();  // Data fetch
        return View(posts);
    }

    [HttpPost]
    public IActionResult Create(BlogPost post)
    {
        _context.BlogPosts.Add(post);  // Data insert
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
----------------------------
💡 DbContext Ka Use Kab Hota Hai?
Scenario	Kaam
Data fetch karna	_context.BlogPosts.ToList()
Data insert karna	_context.BlogPosts.Add(post)
Data update karna	_context.Update(post)
Data delete karna	_context.Remove(post)
Save karna	_context.SaveChanges()
------------------------------------------------------
🔥 Points Yaad Rakh:
DbContext ek class hoti hai jo Entity Framework ka part hoti hai.

Har table ke liye ek DbSet<T> define karte hain.

Isse tu query likhne bina database se baat kar sakta hai.

Register karna padta hai services me (AddDbContext).