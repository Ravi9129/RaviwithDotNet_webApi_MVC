using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 11_custom exceptions
    {
        
    }
}
---------------------------------------
custom exceptions ek aise tarika hai jisse tu apne application ke liye specific error types define kar sakta hai — taki:

Code readable ho,

Exception handling clean ho,

Specific errors ka proper response ya log ho.

Ye especially useful hoti hai layered architecture me jaise Service, Repository, 
ya Domain layer me jab tu clearly batana chahta hai:
"Kya galti hui hai, kyu hui hai, aur kya client ko batana hai?"

🔍 Definition
Custom exception is a user-defined class that inherits from System.Exception (or a derived type like ApplicationException) and 
represents application-specific error conditions.
----------------------------------------------
🔥 Real World Scenario
Tu ek eCommerce site bana raha hai. Kisi ne wrong ProductId diya ya unauthorized access try kiya.

Instead of throwing Exception har baar, tu create karega:

ProductNotFoundException

UnauthorizedAccessAppException

InsufficientBalanceException
------------------------------
Isse:

Tu controller me specifically catch kar sakta hai.

Middleware me ya filters me differentiate kar sakta hai.

Proper status code aur message de sakta hai.
-------------------------------------------------
✅ How to Create Custom Exception

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId)
        : base($"Product with ID {productId} not found.")
    {
    }
}
----------------------------------------------
🔁 With Additional Properties:

public class InsufficientBalanceException : Exception
{
    public decimal RequiredAmount { get; }

    public InsufficientBalanceException(decimal requiredAmount)
        : base($"Insufficient balance. Need at least {requiredAmount:C}")
    {
        RequiredAmount = requiredAmount;
    }
}
-----------------------------------------
✅ How to Throw It

if (product == null)
{
    throw new ProductNotFoundException(productId);
}

if (user.Balance < order.Total)
{
    throw new InsufficientBalanceException(order.Total);
}
------------------------------------------------
✅ Handle in Middleware
Tu exception middleware me handle kar sakta hai custom exceptions ke hisaab se:


catch (Exception ex)
{
    context.Response.ContentType = "application/json";

    switch (ex)
    {
        case ProductNotFoundException:
            context.Response.StatusCode = 404;
            break;

        case InsufficientBalanceException:
            context.Response.StatusCode = 400;
            break;

        default:
            context.Response.StatusCode = 500;
            break;
    }

    var result = JsonSerializer.Serialize(new
    {
        status = context.Response.StatusCode,
        error = ex.Message
    });

    await context.Response.WriteAsync(result);
}
--------------------------------------------------
✅ Controller Usage (Optional)

[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
{
    var product = await _productService.GetProductAsync(id);

    if (product == null)
        throw new ProductNotFoundException(id);

    return Ok(product);
}
--------------------------------------------
✅ Custom Exception Base Class (Advanced)
Tu ek base exception bana sakta hai jo sab custom exceptions inherit karein:

public abstract class AppException : Exception
{
    public int StatusCode { get; }

    protected AppException(string message, int statusCode = 400)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
------------------------------------------
Example child:

public class NotFoundException : AppException
{
    public NotFoundException(string entity, object id)
        : base($"{entity} with ID {id} was not found.", 404)
    {
    }
}
-------------------------------------------------
⚡ Benefits of Custom Exceptions
✅ Makes error handling clean and expressive
✅ Helps in mapping errors to HTTP status codes
✅ Enables layered architecture error separation
✅ Easy to log and debug

