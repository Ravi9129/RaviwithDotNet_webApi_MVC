using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 15_Endpoint Selection Order
    {
        
    }
}
---------------------------------------
Endpoint Selection Order in ASP.NET Core 🚀
1️⃣ Endpoint Selection Kya Hai?
🔹 Jab ek HTTP request aati hai, toh ASP.NET Core determine karta hai ki kaunsa endpoint request ko handle karega.
🔹 Ye Routing Middleware ka kaam hota hai jo request ko sahi endpoint tak pahunchata hai.
🔹 Endpoint selection order ka matlab hai kaise ASP.NET Core endpoints ko prioritize karta hai jab multiple matching routes hote hain.

2️⃣ Endpoint Selection Order Kaise Work Karta Hai?
🔹 ASP.NET Core endpoints ko ek specific order me check karta hai:

1️⃣ Most Specific Route → Pehle Match Hoga
Jo route sabse specific hoga, wo pehle match hoga.

Example:
app.MapGet("/products/featured", () => "Featured Products");
app.MapGet("/products/{id}", (int id) => $"Product {id}");
✅ /products/featured → "Featured Products"
✅ /products/10 → "Product 10"
❌ /products/featured agar /products/{id} ke baad likha hota toh /products/featured ko {id} treat kiya jata.
---------------------------------------------------------
2️⃣ Exact Match → Placeholder Se Pehle Consider Hoga
Static routes dynamic parameters se pehle match hote hain.

Example:

app.MapGet("/about", () => "About Page");
app.MapGet("/{name}", (string name) => $"Hello {name}");
✅ /about → "About Page"
✅ /john → "Hello john"
❌ /about agar {name} ke baad likha hota toh /about ko bhi {name} treat kiya jata.
------------------------------------------------------
3️⃣ More Segments → Pehle Match Hoga
Jis route me zyada segments honge, wo pehle match hoga.

Example:

app.MapGet("/users/profile", () => "User Profile");
app.MapGet("/users/{id}", (int id) => $"User ID: {id}");
✅ /users/profile → "User Profile"
✅ /users/5 → "User ID: 5"
❌ /users/profile ko {id} match nahi karega kyunki /users/profile ek exact match hai.
---------------------------------------------------------
4️⃣ Route Constraints → Priority Milti Hai
Agar kisi route me constraint laga hai toh wo precedence le lega.

Example:

app.MapGet("/orders/{id:int}", (int id) => $"Order ID: {id}");
app.MapGet("/orders/{name}", (string name) => $"Order Name: {name}");
✅ /orders/100 → "Order ID: 100"
✅ /orders/special → "Order Name: special"
❌ /orders/50 ko {name} match nahi karega kyunki pehle {id:int} match karega.
-----------------------------------------------------
5️⃣ Route Parameters → Last Me Match Hote Hain
Jo route parameters use karte hain, wo last me match hote hain.

Example:

app.MapGet("/blog/latest", () => "Latest Blog Post");
app.MapGet("/blog/{id}", (int id) => $"Blog ID: {id}");
✅ /blog/latest → "Latest Blog Post"
✅ /blog/10 → "Blog ID: 10"
❌ /blog/latest agar {id} ke baad likha hota toh /blog/latest {id} me treat hota.
------------------------------------------------
3️⃣ Conclusion
✔ Static routes sabse pehle match hote hain.
✔ Zyada specific routes general routes se pehle match hote hain.
✔ Jo route zyada segments use karta hai, wo pehle match hota hai.
✔ Route constraints precedence lete hain.
✔ Parameters wale routes sabse last me check hote hain.