using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 10_integration test
    {
        
    }
}
---------------------------------------------
Integration Test unit test se level upar hota hai. Yahaan aap real dependencies ke saath end-to-end flow test karte ho — jaise ki controller + service + database (in-memory).

Chalo ek full integration test example dekhte hain using xUnit, TestServer, WebApplicationFactory, and InMemoryDatabase.
----------------------------------------------------
⚙️ Setup
✅ Step 1: Install NuGet Packages

dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package xunit
dotnet add package Moq
----------------------------------------------------------
✅ Step 2: Sample Application
Suppose yeh aapka Product model hai:

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}
--------------------------------------------
✅ Step 3: DbContext

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}
-----------------------------------------------------
✅ Step 4: API Controller

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductsController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
}
--------------------------------------------------
✅ Step 5: Create Integration Test
📁 Test Project → ProductApi.IntegrationTests

public class ProductApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove real DB
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Add InMemory DB
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Create_And_Get_Product_Returns_Success()
    {
        // Arrange
        var product = new Product { Name = "Monitor" };
        var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

        // Act - POST
        var postResponse = await _client.PostAsync("/api/products", content);
        postResponse.EnsureSuccessStatusCode();

        var createdProduct = JsonSerializer.Deserialize<Product>(
            await postResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Act - GET
        var getResponse = await _client.GetAsync($"/api/products/{createdProduct.Id}");
        getResponse.EnsureSuccessStatusCode();

        var returnedProduct = JsonSerializer.Deserialize<Product>(
            await getResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.Equal("Monitor", returnedProduct.Name);
    }
}
--------------------------------------------------
🔍 Breakdown
Part	Description
WebApplicationFactory<Program>	Spins up real TestServer
InMemoryDatabase	No actual DB needed
HttpClient	Simulates real HTTP call
POST + GET	Real API test
----------------------------------------------------------
🔥 Benefits of Integration Testing
✅ End-to-end flow test hota hai.

✅ Multiple layers (Controller → Service → DB) test ho jaate hain.

✅ Less mocking, more real behavior.

✅ Perfect for CI/CD pipelines.
-----------------------------------------------------

Bhai agar tu chaahe to:

✅ Database migration ke sath bhi test bana sakta hai,

✅ Authenticated user ke sath test bana sakta hai,

✅ ya ek custom WebApplicationFactory bana sakta hai.