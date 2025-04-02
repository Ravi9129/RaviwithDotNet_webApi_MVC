using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 1_Controllers 
    {
        
    }
}
--------------------------------------------
Controllers in ASP.NET Core 🚀
1️⃣ Controller Kya Hota Hai?
🔹 Controller ek class hoti hai jo HTTP requests ko handle karti hai.
🔹 Ye MVC (Model-View-Controller) pattern ka part hoti hai.
🔹 Controllers client se aane wale requests ko process karte hain, data manipulate karte hain, aur response return karte hain.
------------------------------------------------------------------------
2️⃣ Controller Ka Basic Structure
🔹 Controller ek C# class hoti hai jo ControllerBase ya Controller class inherit karti hai.
🔹 Controller Controllers folder ke andar hoti hai.

Example: Simple Controller (ProductsController.cs)
using Microsoft.AspNetCore.Mvc;

[Route("api/products")]  // Base route
[ApiController]  // Indicates it's a Web API Controller
public class ProductsController : ControllerBase
{
    [HttpGet]
    public string GetProducts()
    {
        return "List of Products";
    }

    [HttpGet("{id}")]
    public string GetProductById(int id)
    {
        return $"Product Details for ID {id}";
    }
}
✅ /api/products request GetProducts() method ko call karegi.
✅ /api/products/5 request GetProductById(5) method ko call karegi.
-----------------------------------------------------
3️⃣ Controller Kis Liye Use Hota Hai?
Request Handling: Client se aayi request ko process karta hai.

Data Fetching & Processing: Database ya API se data retrieve karta hai.

Response Return Karna: View ya JSON data return karta hai.

Business Logic Execution: Request ke basis pe calculations ya validations karta hai.
------------------------------------------
4️⃣ Types of Controllers
1️⃣ MVC Controller (Controller Class)
🔹 Ye View return karne ke liye use hota hai.
🔹 Mostly Web Applications me use hota hai.

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
✅ return View(); ek HTML View return karega.
------------------------------------------------
2️⃣ API Controller (ControllerBase Class)
🔹 Ye JSON ya XML response return karta hai.
🔹 Web APIs ke liye use hota hai.

using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Id = 1, Name = "Laptop", Price = 1000 });
    }
}
✅ return Ok(); JSON response return karega.
--------------------------------------------------
5️⃣ Routing in Controllers
🔹 Routing se URL request ko correct action method se map kiya jata hai.
🔹 Controllers me attribute routing ya conventional routing dono use ho sakti hain.

Example: Attribute Routing
[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpGet("{id}")]
    public string GetOrderById(int id)
    {
        return $"Order Details for ID {id}";
    }
}
✅ Request: GET /api/orders/10
✅ Response: "Order Details for ID 10"
-----------------------------------------------
6️⃣ Action Methods in Controllers
🔹 Action Methods woh functions hain jo HTTP request handle karte hain.

Common HTTP Methods
Method	Attribute	Usage
GET	[HttpGet]	Data fetch karne ke liye
POST	[HttpPost]	Data create karne ke liye
PUT	[HttpPut]	Data update karne ke liye
DELETE	[HttpDelete]	Data delete karne ke liye
---------------------------
Example: CRUD Actions
[Route("api/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public string GetAllCustomers() => "All Customers";

    [HttpPost]
    public string AddCustomer() => "Customer Added";

    [HttpPut("{id}")]
    public string UpdateCustomer(int id) => $"Customer {id} Updated";

    [HttpDelete("{id}")]
    public string DeleteCustomer(int id) => $"Customer {id} Deleted";
}
✅ GET /api/customers → "All Customers"
✅ POST /api/customers → "Customer Added"
✅ PUT /api/customers/2 → "Customer 2 Updated"
✅ DELETE /api/customers/3 → "Customer 3 Deleted"
------------------------------------------------------------
7️⃣ Model Binding in Controllers
🔹 Client se aane wale data ko automatically C# objects me convert karna Model Binding kehlata hai.
🔹 Example: JSON body ko object me convert karna.

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

[HttpPost]
public IActionResult AddProduct([FromBody] Product product)
{
    return Ok($"Product {product.Name} added with price {product.Price}");
}
-------------------------------
✅ Request (POST /api/products with JSON body):

{
    "id": 1,
    "name": "Phone",
    "price": 500
}
-------
✅ Response: "Product Phone added with price 500"
----------------------------------------------------------------
8️⃣ Dependency Injection in Controllers
🔹 Controllers me dependency injection ka use hota hai services ko inject karne ke liye.
🔹 Example: Database service inject karna.

public interface IProductService
{
    List<string> GetProducts();
}

public class ProductService : IProductService
{
    public List<string> GetProducts() => new List<string> { "Laptop", "Phone", "Tablet" };
}

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(_productService.GetProducts());
    }
}
✅ IProductService inject hone ke baad GetProducts() call karega.
---------------------------------------------------
9️⃣ Conclusion
✔ Controllers HTTP requests ko handle karne ke liye use hote hain.
✔ MVC Controllers HTML View return karte hain, aur API Controllers JSON data return karte hain.
✔ Routing request ko correct method tak pahunchane ka kaam karta hai.
✔ Action Methods CRUD operations handle karne ke liye use hote hain.
✔ Model Binding client se aayi data ko object me convert karta hai.
✔ Dependency Injection services ko efficiently inject karne ke liye use hoti hai.