using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 5_Custom Model Binder
    {
        
    }
}
------------------------------------
🔹 Custom Model Binder in ASP.NET Core
📌 Kya Hai?
ASP.NET Core Model Binding ka kaam hai request se aane wale data ko C# object me convert karna.

Default model binding [FromBody], [FromQuery], [FromRoute], [FromForm] ke through hoti hai.

Custom Model Binder tab use hota hai jab hume custom format ya complex data types ko bind karna hota hai.
-----------------------------------------------
🛠 Example 1: Custom Model Binder for Complex Data (Pipe Separated Values)
Maan lo hume ek string "John Doe|john@example.com" ko UserModel me convert karna hai.
---------------------------------------------------
👨‍💻 Step 1: Model Class
public class UserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}
---------------------------------------------------------
👨‍💻 Step 2: Custom Model Binder
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

public class UserModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        var parts = value.Split('|');
        if (parts.Length != 2)
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid format. Use 'Name|Email'");
            return Task.CompletedTask;
        }

        var user = new UserModel
        {
            Name = parts[0],
            Email = parts[1]
        };

        bindingContext.Result = ModelBindingResult.Success(user);
        return Task.CompletedTask;
    }
}
---------------------------------------------------------
👨‍💻 Step 3: Apply Model Binder in Controller
[HttpGet("get-user")]
public IActionResult GetUser([ModelBinder(BinderType = typeof(UserModelBinder))] UserModel user)
{
    return Ok($"User {user.Name} with email {user.Email} received successfully!");
}
---------------------------------------------------
📤 Client Request
GET /get-user?user=John Doe|john@example.com
🎯 Response
User John Doe with email john@example.com received successfully!
--------------------------------------------
🛠 Example 2: Custom Model Binder for JSON Data in Query String
Maan lo query string me JSON data aata hai aur hume usko object me convert karna hai.
------------------------------------------------------
👨‍💻 Step 1: Custom Model Binder
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using System.Threading.Tasks;

public class JsonModelBinder<T> : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        try
        {
            var result = JsonSerializer.Deserialize<T>(value);
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid JSON format.");
        }

        return Task.CompletedTask;
    }
}
-----------------------------------------
👨‍💻 Step 2: Apply Model Binder in Controller
[HttpGet("get-user-json")]
public IActionResult GetUserJson([ModelBinder(BinderType = typeof(JsonModelBinder<UserModel>))] UserModel user)
{
    return Ok($"User {user.Name} with email {user.Email} received successfully!");
}
----------------------------------------------
📤 Client Request
GET /get-user-json?user={"name":"John Doe","email":"john@example.com"}
🎯 Response
User John Doe with email john@example.com received successfully!
--------------------------------------------
🔹 Summary
✅ Custom Model Binder tab use hota hai jab default model binding sufficient na ho.
✅ Complex Formats (Pipe, CSV, JSON in Query, etc.) ke liye Custom Model Binder useful hai.
✅ Custom Model Binder IModelBinder implement karke banaya jata hai.