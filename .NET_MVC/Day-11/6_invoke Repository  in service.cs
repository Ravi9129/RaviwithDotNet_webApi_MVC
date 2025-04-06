using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 6_invoke Repository  in service
    {
        
    }
}
------------------------------------------
Repository ko Service layer me invoke karne ka matlab hota hai data access logic ko business logic se separate karna. 
Service layer ka kaam hota hai domain/business logic handle karna aur Repository ka kaam hota hai data ko CRUD karna (DB se).

✅ Scenario:
Repository → Data Access

Service → Business Logic

Controller → Handle HTTP Requests
--------------------------------------------------
🧱 Steps
1️⃣ Interface: IProductService.cs

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}
--------------------------------------------------
2️⃣ Implementation: ProductService.cs

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;

    public ProductService(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepo.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepo.GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        // extra business logic here if needed
        await _productRepo.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepo.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepo.DeleteAsync(id);
    }
}
------------------------------------------
3️⃣ Register Services in Program.cs

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
-----------------------------------------------------------
4️⃣ Use in Controller

public class ProductController : Controller
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _service.GetAllProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _service.GetProductByIdAsync(id);
        return View(product);
    }
}
-----------------------------------------------------
✅ Benefits
🔹 Separation of concerns
🔹 Testability → You can mock service or repo
🔹 Scalability → Add validation, business rules in service
🔹 Clean architecture

