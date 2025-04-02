using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 2_Multiple Action Methods
    {
        
    }
}
-------------------------------
Multiple Action Methods in ASP.NET Core 🚀
1️⃣ Multiple Action Methods Kya Hote Hain?
🔹 Ek hi controller me multiple HTTP request handlers define kar sakte hain.
🔹 Har action method ek specific HTTP request (GET, POST, PUT, DELETE) handle karta hai.
🔹 Action methods me routing ka bhi use hota hai taake correct request correct method ko execute kare.

2️⃣ Example: Multiple Action Methods in One Controller
Scenario: Ek "ProductsController" hai jo CRUD operations handle karta hai.

using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    // 1️⃣ GET - Sabhi products return karega
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(new string[] { "Laptop", "Phone", "Tablet" });
    }

    // 2️⃣ GET - Ek specific product return karega ID ke basis par
    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        return Ok($"Product Details for ID {id}");
    }

    // 3️⃣ POST - Naya product add karega
    [HttpPost]
    public IActionResult AddProduct([FromBody] string product)
    {
        return Ok($"Product '{product}' added successfully");
    }

    // 4️⃣ PUT - Existing product update karega
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] string updatedProduct)
    {
        return Ok($"Product {id} updated to '{updatedProduct}'");
    }

    // 5️⃣ DELETE - Product delete karega ID ke basis par
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        return Ok($"Product {id} deleted successfully");
    }
}
------------------------------------------------------
3️⃣ Kaise Kaam Karega? (API Testing Example)
✅ 1. Get All Products
📌 Request: GET /api/products
📌 Response:

["Laptop", "Phone", "Tablet"]
------------------------------------------------------
✅ 2. Get Product by ID
📌 Request: GET /api/products/5
📌 Response:
"Product Details for ID 5"
--------------------------------------------------
✅ 3. Add a New Product
📌 Request: POST /api/products
📌 Body:
"Smartwatch"
---------------------------------
📌 Response:

"Product 'Smartwatch' added successfully"
-------------------------------------------
✅ 4. Update a Product
📌 Request: PUT /api/products/3
📌 Body:

"Gaming Laptop"
-------------------------------------------
📌 Response:
"Product 3 updated to 'Gaming Laptop'"
-------------------------------------------------------------
✅ 5. Delete a Product
📌 Request: DELETE /api/products/2
📌 Response:

"Product 2 deleted successfully"
4️⃣ Multiple Action Methods Kaise Kaam Karte Hain?
1️⃣ Routing (Attribute Routing)

[Route("api/products")] se base route define hota hai.

[HttpGet("{id}")] se ID-based GET request handle hoti hai.
-------------------------------------------------------------
2️⃣ Different HTTP Methods

[HttpGet] → Data fetch karne ke liye

[HttpPost] → Naya data insert karne ke liye

[HttpPut] → Existing data update karne ke liye

[HttpDelete] → Data delete karne ke liye
------------------------------------------------------
3️⃣ Model Binding & Parameters

{id} URL parameter ke zariye action method tak data pahunchata hai.

[FromBody] se request body ka data extract hota hai.
--------------------------------------------------
5️⃣ Real-World Scenario
🚀 Ek E-commerce Website ka API Controller Example:

GET /api/orders → Sare orders return karega.

GET /api/orders/12 → Order ID 12 ka data return karega.

POST /api/orders → Naya order create karega.

PUT /api/orders/12 → Order ID 12 ka data update karega.

DELETE /api/orders/12 → Order ID 12 delete karega.
-------------------------------------------
6️⃣ Conclusion
✔ Multiple action methods ek hi controller me alag-alag HTTP requests handle karte hain.
✔ Routing, Model Binding, aur HTTP Methods ke basis par correct action method execute hota hai.
✔ CRUD operations ko efficiently implement karne ke liye ye approach best hai.