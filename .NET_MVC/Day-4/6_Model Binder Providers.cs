using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 6_Model Binder Providers
    {
        
    }
}
-------------------------------------
🔹 Model Binder Providers in ASP.NET Core
📌 Kya Hai?
ASP.NET Core me Model Binder Providers ek mechanism hai jo request se data ko Model Binding process me inject karta hai.

Model Binder Providers decide karte hain ki kaunsa Model Binder ek particular type ke liye use hoga.

Yeh ek pipeline ki tarah kaam karta hai jo IModelBinderProvider interface implement karta hai.

Custom Model Binder Provider tab use hota hai jab hume apne Custom Model Binder ko control karna hota hai.

🚀 Kaise Kaam Karta Hai?
Jab ASP.NET Core ek request process karta hai, toh Model Binding Pipeline different Model Binder Providers ko check karti hai:
1️⃣ Simple Type (int, string, bool, etc.) ➝ Default model binder use hota hai.
2️⃣ Complex Type (Custom Objects, Classes, etc.) ➝ [FromBody], [FromQuery], [FromRoute], etc. use hote hain.
3️⃣ Custom Type (Special Format, JSON in Query, Pipe-Separated Values, etc.) ➝ Custom Model Binder use hota hai.
--------------------------------------------
🛠 Example 1: Custom Model Binder Provider
Maan lo hume ek Custom Model Binder (Pipe-Separated Values) ka provider banana hai.
-------------------------
👨‍💻 Step 1: Model Class
public class UserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}
-----------------------------------------------
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
-------------------------------------------------------
👨‍💻 Step 3: Custom Model Binder Provider
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

public class UserModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(UserModel))
        {
            return new BinderTypeModelBinder(typeof(UserModelBinder));
        }
        return null;
    }
}
-------------------------------------------
👨‍💻 Step 4: Register Model Binder Provider in Startup
services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new UserModelBinderProvider());
});
🔹 Insert(0, new UserModelBinderProvider()) ka matlab hai ki yeh provider sabse pehle execute hoga.
🔹 Agar koi UserModel ka request aaye, toh yeh Custom Model Binder ko use karega.
------------------------------------------------------------
👨‍💻 Step 5: Controller Method
[HttpGet("get-user")]
public IActionResult GetUser(UserModel user)
{
    return Ok($"User {user.Name} with email {user.Email} received successfully!");
}
-------------------------------------------
📤 Client Request
GET /get-user?user=John Doe|john@example.com
🎯 Response
User John Doe with email john@example.com received successfully!
----------------------------------------------------------
🔹 Summary
✅ Model Binder Providers decide karte hain kaunsa Model Binder use hoga.
✅ Custom Model Binder Provider tab use hota hai jab multiple custom binders ko control karna ho.
✅ Startup file me Model Binder Provider ko register karna hota hai.
✅ Complex types ya special data formats ke liye useful hai.