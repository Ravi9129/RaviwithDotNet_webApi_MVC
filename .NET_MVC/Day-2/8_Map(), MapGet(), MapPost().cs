using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 8_Map(), MapGet(), MapPost()
    {
        
    }
}
------------------------------------
Map(), MapGet(), MapPost() in ASP.NET Core
ASP.NET Core minimal APIs me Map(), MapGet(), aur MapPost() routing ke liye use hote hain. Yeh methods define karte hain ki kaunsi HTTP request kis route par jayegi.
----------------------------------------------------------
1️⃣ Map()
Map() kisi specific path par ek custom middleware define karne ke liye use hota hai.

Example: Map() Middleware

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/hello", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from /hello route!");
    });
});

app.Run();
✅ Agar user /hello route par request karega, toh response milega:
📝 "Hello from /hello route!"

⚡ Use case: Jab kisi specific route par ek custom response ya middleware add karna ho.
-------------------------------------------------------------
2️⃣ MapGet()
MapGet() GET requests handle karne ke liye use hota hai.

Example: MapGet()
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/welcome", () => "Welcome to ASP.NET Core!");

app.Run();
✅ Agar user /welcome par GET request bhejega, toh response milega:
📝 "Welcome to ASP.NET Core!"

⚡ Use case: Jab sirf GET requests ko handle karna ho.
---------------------------------------------------------
3️⃣ MapPost()
MapPost() POST requests handle karta hai.

Example: MapPost()

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/submit", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    string requestBody = await reader.ReadToEndAsync();
    return $"Received Data: {requestBody}";
});

app.Run();
✅ Agar user /submit par POST request bhejega, toh server received data ko return karega.

⚡ Use case: Jab sirf POST requests ko handle karna ho.

4️⃣ Difference Between Map(), MapGet(), and MapPost()
Method	Request Type	Use Case
Map()	Any	Custom middleware
MapGet()	GET	Sirf GET requests handle karna
MapPost()	POST	Sirf POST requests handle karna
----------------------------------------------------------
5️⃣ Full Example: Using MapGet(), MapPost(), and Map()
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/custom", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from /custom middleware!");
    });
});

app.MapGet("/getdata", () => "This is GET request response");

app.MapPost("/postdata", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    string requestBody = await reader.ReadToEndAsync();
    return $"Received POST Data: {requestBody}";
});
-------------------------------------
app.Run();
✅ Agar user /custom route par jayega toh middleware execute hoga.
✅ Agar user /getdata par GET request karega toh "This is GET request response" milega.
✅ Agar user /postdata par POST request karega toh received body data return hoga.
--------------------------------------
6️⃣ Conclusion
✔ Map() kisi bhi request type ke liye custom middleware add karne ke liye hai.
✔ MapGet() sirf GET requests ke liye hai.
✔ MapPost() sirf POST requests ke liye hai.