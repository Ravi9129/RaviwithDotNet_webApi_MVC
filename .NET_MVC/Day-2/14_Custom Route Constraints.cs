using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 14_Custom Route Constraints
    {
        
    }
}
-------------------------------------------------
Custom Route Constraints in ASP.NET Core 🚀
1️⃣ Custom Route Constraints Kya Hote Hain?
🔹 ASP.NET Core built-in route constraints provide karta hai (e.g., int, bool, datetime, etc.), lekin kabhi kabhi custom validation chahiye hoti hai jo built-in constraints se possible nahi hoti.
🔹 Custom route constraints ka use apni validation logic likhne ke liye hota hai jo route parameters ko validate kare.
🔹 Ek example: Agar hume sirf odd numbers accept karne hain toh built-in constraints me aisa koi option nahi.
🔹 Solution: Hum custom route constraint bana sakte hain jo sirf odd numbers allow kare! 🚀
--------------------------------------------
2️⃣ Custom Route Constraint Kaise Banaye?
Step 1: Custom Route Constraint Class Banaye
🔹 Sabse pehle IRouteConstraint ko implement karke ek class banani hoti hai.
🔹 Isme Match() method override karni hoti hai jo parameter ko validate karegi.
---------------------------------------------------
Example: Custom Route Constraint Jo Sirf Odd Numbers Allow Kare

using Microsoft.AspNetCore.Routing;

public class OddNumberRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
                      RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value) && int.TryParse(value?.ToString(), out int number))
        {
            return number % 2 != 0; // Sirf odd numbers allow karega
        }
        return false;
    }
}
✅ Ye constraint sirf odd numbers allow karega (1, 3, 5, ...).
❌ Agar even number aaye (2, 4, 6, ...) toh 404 Not Found error dega.
----------------------------------------------------------
Step 2: Custom Constraint Ko Register Kare
🔹 Ab hume Startup.cs ya Program.cs me apni constraint ko register karna hoga.

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("odd", typeof(OddNumberRouteConstraint));
});

var app = builder.Build();
✅ Ab ASP.NET Core is constraint ko pehchaanega.
---------------------------------------------------------
Step 3: Custom Route Constraint Ka Use Kare
🔹 Ab hum apni constraint ko route me use kar sakte hain.

app.MapGet("/items/{id:odd}", (int id) =>
{
    return $"Valid Odd Number: {id}";
});
✅ /items/5 → "Valid Odd Number: 5"
❌ /items/8 → 404 Not Found
------------------------------------------------------
3️⃣ Example: Custom Route Constraint for Hexadecimal IDs
🔹 Agar hume sirf hexadecimal values allow karni ho (0-9, A-F), toh ek custom constraint likh sakte hain.

public class HexRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
                      RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value))
        {
            var hexPattern = "^[0-9A-Fa-f]+$";
            return Regex.IsMatch(value?.ToString() ?? "", hexPattern);
        }
        return false;
    }
}
--------------------------------------
Register the Constraint
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("hex", typeof(HexRouteConstraint));
});
---------------------------------------
Use the Constraint in Route

app.MapGet("/code/{id:hex}", (string id) =>
{
    return $"Valid Hex Code: {id}";
});
✅ /code/1A3F → "Valid Hex Code: 1A3F"
❌ /code/XYZ → 404 Not Found
---------------------------------------------
4️⃣ Conclusion
✔ Custom Route Constraints se hum complex validations apply kar sakte hain jo built-in constraints me possible nahi.
✔ IRouteConstraint interface ka use karke hum apni custom logic likh sakte hain.
✔ Har custom constraint ko register karna zaroori hota hai.
✔ Custom constraints APIs ko secure aur efficient banate hain.