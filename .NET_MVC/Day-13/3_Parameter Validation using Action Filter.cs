using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 3_Parameter Validation using Action Filter
    {
        
    }
}
----------------------------------------------
Parameter Validation using Action Filter in ASP.NET Core — real-world mein jab tu kisi controller method ko call karta hai, 
toh usme aane wale parameters ko validate karna zaroori hota hai.

Isse tu ye ensure karta hai ke:

Invalid data na aaye controller method tak.

Model validation logic centralized rahe (na ke har action method mein repeat ho).

Chaliye sab kuch clearly samjhte hain. 👇

💡 What is Parameter Validation in Action Filter?
Parameter validation ka matlab hai ki controller action ke parameters ko check 
karna (jaise null, empty, out of range, custom rules, etc.) — ye validation tu OnActionExecuting method me karega.
---------------------------------------
🎯 Real-World Example
Tu ek API bana raha hai:

GET /user/details?id=0
Yahan id valid hona chahiye (non-zero, positive).

Tu chaahe toh sabhi actions me if-else likhe,
 but better approach hai ek reusable ActionFilter banana jo automatically ye validate kare har jagah.
----------------------------------------------
🔨 Custom Parameter Validation Action Filter
✅ Step 1: Create Custom Action Filter

public class ValidateIdAttribute : ActionFilterAttribute
{
    private readonly string _parameterName;

    public ValidateIdAttribute(string parameterName)
    {
        _parameterName = parameterName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.ContainsKey(_parameterName))
        {
            var value = context.ActionArguments[_parameterName];
            if (value == null || Convert.ToInt32(value) <= 0)
            {
                context.Result = new BadRequestObjectResult($"'{_parameterName}' must be greater than zero.");
            }
        }
    }
}
-----------------------------------------------
✅ Step 2: Use it on Controller Action

[ValidateId("id")]
public IActionResult GetUserById(int id)
{
    // This will only run if id > 0
    return Ok($"User found with ID = {id}");
}
----------------------------------------
✨ Bonus: Validate Complex Object (Model)

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
--------------------------------------
Use like this:
[ValidateModel]
public IActionResult PostUser(UserDto user)
{
    // Will only hit if model is valid
    return Ok("User created");
}
-----------------------------------------------------------
📦 Centralized Registration (Global Use)
---------------------------------------------
Tu chahe toh is filter ko globally register kar sakta hai:

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});
-----------------------------------------
🔐 Real-World Scenarios
Scenario	Filter Example
Validate id is positive	[ValidateId("id")]
Validate required query param	[ValidateQuery("status")]
Validate complex model	[ValidateModel]
Check if object exists in DB	Custom filter with DB check
-----------------------------------------------------
🧠 Tip: ActionArguments Dictionary
Ye dict action ke sabhi parameters ko hold karta hai:

var id = context.ActionArguments["id"];
Toh tu kisi bhi param ko dynamically validate kar sakta hai.
-------------------------------------
✅ Final Thoughts
✅ Action Filters ke through tu validation ko clean, reusable and centralized bana sakta hai — aur code duplication avoid kar sakta hai.