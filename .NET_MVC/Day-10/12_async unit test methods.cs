using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 12_async unit test methods
    {
        
    }
}
--------------------------------------------
async unit test methods likhna aajkal zaroori hai, 
kyunki aapka production code bhi async hota hai — toh naturally test bhi async hona chahiye.

Chal dekhte hain xUnit me async test methods kaise likhte hain, saath hi examples bhi deta hoon. 👇
-----------------------------------------------
✅ 1. Basic Async Test Method in xUnit

public class ProductServiceTests
{
    [Fact]
    public async Task GetAllProducts_ReturnsCorrectCount()
    {
        // Arrange
        var service = new ProductService();

        // Act
        var products = await service.GetAllProductsAsync();

        // Assert
        Assert.Equal(3, products.Count);
    }
}
🧠 Note:

Use async Task return type

[Fact] works with async out of the box in xUnit
--------------------------------------------------
✅ 2. Async with Assert.ThrowsAsync
Agar koi method exception throw kare:

[Fact]
public async Task DeleteProduct_WhenIdInvalid_ThrowsException()
{
    var service = new ProductService();

    await Assert.ThrowsAsync<ArgumentException>(() =>
        service.DeleteProductAsync(-1));
}
-------------------------------------
✅ 3. Using [Theory] with InlineData and Async

[Theory]
[InlineData(1)]
[InlineData(2)]
public async Task GetProductById_ReturnsProduct(int productId)
{
    var service = new ProductService();

    var result = await service.GetProductByIdAsync(productId);

    Assert.NotNull(result);
}
---------------------------------------------------
✅ 4. Mocking Async Dependencies with Moq
Agar service kisi repo ko call karta hai jo async hai:

[Fact]
public async Task GetAll_ReturnsAllItems()
{
    // Arrange
    var mockRepo = new Mock<IProductRepository>();
    mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Product> { new(), new() });

    var service = new ProductService(mockRepo.Object);

    // Act
    var result = await service.GetAllAsync();

    // Assert
    Assert.Equal(2, result.Count);
}
------------------------------------------------------
✅ 5. Custom ITestOutputHelper in Async Tests

public class ProductTests
{
    private readonly ITestOutputHelper _output;

    public ProductTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task TestLoggingOutput()
    {
        await Task.Delay(100);
        _output.WriteLine("Async test completed.");
        Assert.True(true);
    }
}