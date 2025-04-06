using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 5_Repository Pattern
    {
        
    }
}
----------------------------------------------------
Repository Pattern ek design pattern hai jo data access logic ko application logic se alagh karta hai, jisse code clean, testable aur maintainable ban jaata hai.

✅ Why Repository Pattern?
🔹 Data access logic alag hota hai
🔹 Easy to mock in unit tests
🔹 Fewer changes if DB changes
🔹 Interface-based abstraction
🔹 SRP (Single Responsibility Principle) follow karta hai

🧱 Structure
Imagine kar, ek app mein Product ka CRUD chahiye.
--------------------------------------------------------
1️⃣ IProductRepository - Interface

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
----------------------------------------------------------
2️⃣ ProductRepository - Implementation

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
----------------------------------------------
3️⃣ Program.cs - Registering Repository

builder.Services.AddScoped<IProductRepository, ProductRepository>();
------------------------------------------------------------
4️⃣ Inject in Controller

public class ProductController : Controller
{
    private readonly IProductRepository _repo;

    public ProductController(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _repo.GetAllAsync();
        return View(products);
    }
}
------------------------------------------------------
✨ Benefits
✅ Loosely Coupled
✅ Easily testable (mock repo)
✅ Single source of DB logic
✅ Good for DDD (Domain Driven Design)

