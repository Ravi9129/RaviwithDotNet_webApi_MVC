using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 11_integration test with response body
    {
        
    }
}
--------------------------------------------------------------
Agar aap Integration Test ke response body ko access karna chahte ho (jaise object return ho raha hai, uska content validate karna hai), toh HttpClient ke response se body ko read karke deserialize kar sakte ho.

Yeh raha full example using xUnit, HttpClient, and System.Text.Json for deserializing response body.
----------------------------------------------
✅ Example: Integration Test with Response Body Validation
🧪 Suppose aapka API endpoint yeh object return karta hai:

// Model
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}
--------------------------------------------------
🧪 Controller Action (GET API)

[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null)
        return NotFound();
    
    return Ok(product);
}
---------------------------------------------------------
🧪 Integration Test

public class ProductIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();

                // ✅ Seed Data
                db.Products.Add(new Product { Id = 1, Name = "Mouse" });
                db.SaveChanges();
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GetProduct_ReturnsProductWithCorrectName()
    {
        // Act
        var response = await _client.GetAsync("/api/products/1");
        response.EnsureSuccessStatusCode();

        // ✅ Read and Deserialize Response Body
        var json = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<Product>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
        Assert.Equal("Mouse", product.Name);
    }
}
------------------------------
🔍 Breakdown
Step	Explanation
await response.Content.ReadAsStringAsync()	Reads JSON string from the response
JsonSerializer.Deserialize<T>()	Converts JSON string to C# object
Assert.Equal()	Validates values from response body
--------------------------------------------
🧠 Pro Tips
Use PropertyNameCaseInsensitive = true to handle JSON casing mismatches.
------------------
For list results, use:
var list = JsonSerializer.Deserialize<List<Product>>(json);
For deep objects, create DTOs or view models as needed.