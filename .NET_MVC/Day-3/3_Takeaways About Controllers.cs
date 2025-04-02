using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 3_Takeaways About Controllers
    {
        
    }
}
------------------------------------------------------
Takeaways About Controllers in ASP.NET Core 🚀
1️⃣ Controllers Kya Hain?
🔹 Controllers MVC pattern ka main component hain, jo request handle karte hain.
🔹 Ye user requests ko process karte hain aur response return karte hain.
🔹 Ek controller multiple action methods rakh sakta hai jo alag-alag HTTP requests ko handle karte hain.
-------------------------------------------------------------
2️⃣ Controllers Ka Basic Structure
using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(new string[] { "Laptop", "Phone", "Tablet" });
    }
}
🔹 [ApiController] → Iska use API development me model validation aur data binding ko simplify karta hai.
🔹 [Route("api/products")] → Controller ke base route ko define karta hai.
🔹 ControllerBase → Ye base class hai jo API controllers ke liye zaroori functionalities provide karti hai.
---------------------------------------------------------
3️⃣ Controllers Ke Important Features
✅ 1. Routing Support
🔹 Controllers attribute routing aur conventional routing dono support karte hain.
🔹 Example:

[Route("api/orders")]
public class OrdersController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        return Ok($"Order Details for ID {id}");
    }
}
📌 Request: GET /api/orders/5
📌 Response: "Order Details for ID 5"
-----------------------------------------------------------------
✅ 2. Multiple Action Methods
🔹 Controllers ke andar multiple methods ho sakte hain jo different HTTP requests handle kar sakein.
🔹 Example:
[HttpPost]
public IActionResult AddProduct([FromBody] string product)
{
    return Ok($"Product '{product}' added successfully");
}
📌 Request: POST /api/products
📌 Body: "Smartphone"
📌 Response: "Product 'Smartphone' added successfully"
------------------------------------------------------------------
✅ 3. Model Binding & Validation
🔹 Controllers automatically request body ya query parameters ko method parameters me bind kar sakte hain.
🔹 Example:

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

[HttpPost]
public IActionResult AddProduct([FromBody] Product product)
{
    if (string.IsNullOrEmpty(product.Name))
    {
        return BadRequest("Product Name is required!");
    }
    return Ok($"Added Product: {product.Name}");
}
📌 Agar Product Name missing hoga to BadRequest return hoga.
-------------------------------------------------
✅ 4. Dependency Injection
🔹 Controllers automatically dependency injection support karte hain.
🔹 Example:

private readonly IProductService _productService;

public ProductsController(IProductService productService)
{
    _productService = productService;
}

[HttpGet]
public IActionResult GetAllProducts()
{
    var products = _productService.GetProducts();
    return Ok(products);
}
📌 Yahan IProductService inject ho raha hai jo GetProducts method ko call karega.
--------------------------------------------------------------
✅ 5. Return Types in Controllers
🔹 Controllers different types ke responses return kar sakte hain:

Ok(object) → Success response

BadRequest() → Invalid request

NotFound() → Resource not found

NoContent() → 204 response without data

Created() → Naya resource create hone par response

Example:
[HttpGet("{id}")]
public IActionResult GetProductById(int id)
{
    if (id <= 0)
    {
        return BadRequest("Invalid Product ID");
    }
    return Ok($"Product Details for ID {id}");
}
📌 Request: GET /api/products/-1
📌 Response: "Invalid Product ID"

-----------------------------------------------------------------
4️⃣ Real-World Scenario Example
🔹 Ek E-commerce Application me Controllers ka use:

ProductsController → Products fetch karne, add/update/delete karne ke liye

OrdersController → Orders create/update/delete karne ke liye

UsersController → User authentication & profile management ke liye

[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        return Ok($"Order Details for Order ID {id}");
    }
}
📌 Request: GET /api/orders/101
📌 Response: "Order Details for Order ID 101"
---------------------------------------------------------------------------
5️⃣ Conclusion
✔ Controllers ASP.NET Core application ka backbone hain.
✔ Multiple action methods ek hi controller ke andar define kar sakte hain.
✔ Routing, Dependency Injection, Model Binding & Validation ka support hota hai.
✔ Controllers structured aur scalable web APIs banane me madad karte hain.