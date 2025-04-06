using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 8_Mock Repository 
    {
        
    }
}
-------------------------------------------------
Mock Repository ka use hum unit testing mein karte hain, jahan hume actual database access nahi chahiye hota. 
Isse hum business logic ko test kar paate hain without touching the real database.

✅ Scenario:
Aapke paas ek IProductRepository hai, jise aap ProductService me use karte ho.
 Aapko ProductService ka test likhna hai without hitting the real database.

🛠️ Step-by-Step: Mock Repository with Moq
1️⃣ Create Interface (IProductRepository)

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
---------------------------------------
2️⃣ Service that uses Repository

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> GetProductAsync(int id)
    {
        return await _repository.GetProductByIdAsync(id);
    }
}
----------------------------------------
3️⃣ Create Unit Test using Moq

using Moq;
using Xunit;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductAsync_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();

        var expectedProduct = new Product { Id = 1, Name = "Laptop" };
        mockRepo.Setup(repo => repo.GetProductByIdAsync(1))
                .ReturnsAsync(expectedProduct);

        var service = new ProductService(mockRepo.Object);

        // Act
        var result = await service.GetProductAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Laptop", result.Name);
    }
}

--------------------------------------------------------
✅ Benefits of Mocking Repository
No DB dependency

Fast & Reliable tests

Test edge cases easily

Inject any behavior (e.g., throw exceptions)

