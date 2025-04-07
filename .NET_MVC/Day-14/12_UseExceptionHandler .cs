using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-14
{
    public class 12_UseExceptionHandler 
    {
        
    }
}
------------------------------------------------
UseExceptionHandler middleware ka scene dekhte hain — ye ASP.NET Core ka built-in global error handling mechanism hai jo 
unhandled exceptions ko gracefully handle karta hai bina app crash hue.
Ye tu production-level error handling ke liye use karta hai.
-------------------------------------------------
🔍 Definition
app.UseExceptionHandler() ek middleware hai jo application-wide exception ko handle karta hai. 
Ye unhandled exceptions ke liye ek fallback pipeline setup karta hai jisme tu custom error page ya JSON response return kar sakta hai.
-------------------------------------------
🎯 Real-world Scenario
Manle tu ek production app chala raha hai. Kisi controller me unexpected exception aayi, 
aur tu nahi chahta ki stack trace ya sensitive details user ko dikhein.

Tu UseExceptionHandler setup karta hai, taki:

Log ho jaye error (e.g., Serilog)

User ko custom message mile ("Something went wrong")

App crash na ho
-----------------------------------------
✅ Basic Setup in Program.cs

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
----------------------------------------------
🔁 In HomeController.cs

public IActionResult Error()
{
    return View(); // Error.cshtml page render hota hai
}
---------------------------------------------
✅ Custom JSON Error Response (for APIs)

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        var error = new
        {
            Message = "An unexpected error occurred.",
            Path = exceptionHandlerPathFeature?.Path,
            Exception = exceptionHandlerPathFeature?.Error?.Message
        };

        var errorJson = JsonSerializer.Serialize(error);

        await context.Response.WriteAsync(errorJson);
    });
});
--------------------------------------------
📦 IExceptionHandlerPathFeature
Ye ek interface hai jo exception handling middleware ko current path & exception deta hai.


var feature = context.Features.Get<IExceptionHandlerPathFeature>();
var ex = feature?.Error;
var path = feature?.Path;
-----------------------------------------------
🛠 With Logging

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionFeature?.Error;

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "Unhandled exception");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Oops! Something broke.");
    });
});
--------------------------------------------------

--------------------------------
🚫 Don't Use It For
Handling expected exceptions (use try-catch in service/controller)

Fine-grained API error responses (use Exception filters or middleware)


-------------------------------------------
👇 Final Suggestion
Bhai agar tu API app bana raha to UseExceptionHandler ke saath custom JSON + Serilog logging + custom exception types ka combo best hai.