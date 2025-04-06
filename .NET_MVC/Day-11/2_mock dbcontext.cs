using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 2_mock dbcontext
    {
        
    }
}
---------------------------------
Entity Framework Core ke DbContext ko mock karna chahta hai unit testing ke liye — to ye ek tricky area hota hai because DbContext and DbSet are not interfaces by default. Lekin manageable hai — 2 main tarike hain:

✅ 1. Mock DbContext using In-Memory Provider (Recommended for most cases)
Ye real DbContext use karta hai but ek in-memory database ke sath.
----------------------------------------------
🛠 Setup:

dotnet add package Microsoft.EntityFrameworkCore.InMemory
------------------------------------
🎯 Sample DbContext and Model:

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}
--------------------------------------------
🧪 Unit Test with In-Memory DB

public class ProductService
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public List<Product> GetAll() => _context.Products.ToList();
}
-----------------------------------------
public class ProductServiceTests
{
    [Fact]
    public void GetAll_ReturnsAllProducts()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new AppDbContext(options);
        context.Products.Add(new Product { Id = 1, Name = "Test Product" });
        context.SaveChanges();

        var service = new ProductService(context);

        // Act
        var result = service.GetAll();

        // Assert
        Assert.Single(result);
        Assert.Equal("Test Product", result.First().Name);
    }
}
--------------------------------------------------
✅ 2. Mock DbSet using Moq (Advanced - Use for fine control)
Jab tu direct DbSet<T> methods jaise Find, Add, Remove ko mock karna chahta hai.

🔧 Mock DbSet<T>
-------------------
var data = new List<Product>
{
    new Product { Id = 1, Name = "Item1" },
    new Product { Id = 2, Name = "Item2" }
}.AsQueryable();

var mockSet = new Mock<DbSet<Product>>();
mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

var mockContext = new Mock<AppDbContext>();
mockContext.Setup(c => c.Products).Returns(mockSet.Object);