using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 9_controller unit test
    {
        
    }
}
-------------------------------------------
Controller ka unit test likhna ASP.NET Core MVC ya Web API me important hota hai, especially jab aap ensure karna chahte ho ki controller expected result return kar raha hai.

Chalo ek real-world example ke through Controller Unit Test samjhte hain with xUnit and Moq.
----------------------------
🧩 Scenario:
Aapke paas ek ProductController hai jo IProductService use karta hai:
------------------------------------------------
✅ 1. ProductController Example

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
}
----------------------------------------------
🧪 2. Unit Test for Controller

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);
    }

    [Fact]
    public async Task GetProduct_ReturnsOk_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop" };
        _mockService.Setup(s => s.GetProductByIdAsync(1))
                    .ReturnsAsync(product);

        // Act
        var result = await _controller.GetProduct(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal("Laptop", returnProduct.Name);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetProductByIdAsync(99))
                    .ReturnsAsync((Product)null);

        // Act
        var result = await _controller.GetProduct(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
--------------------------------------------
🔍 Breakdown
Part	Explanation
Mock<IProductService>	Mocked dependency
controller.GetProduct()	Call action method
Assert.IsType<OkObjectResult>	Verify response type
ReturnsAsync(product)	Simulate async behavior
NotFoundResult	Test for non-existing product

----------------------------------
📁 Folder Structure Suggestion

/Controllers
    ProductController.cs

/Services
    IProductService.cs
    ProductService.cs

/Tests
    ProductControllerTests.cs

/Models
    Product.cs
