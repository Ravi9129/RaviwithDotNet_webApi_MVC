using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 13_Route Constraints
    {
        
    }
}
--------------------------------------------
Route Constraints in ASP.NET Core 🚀
1️⃣ Route Constraints Kya Hote Hain?
🔹 Route constraints ka use hota hai route parameters ke format ko restrict karne ke liye.
🔹 Ye ensure karte hain ki sirf valid data hi route parameters me aaye.
🔹 Agar request me invalid data aaye, toh ASP.NET Core automatically 404 Not Found error de deta hai.
🔹 Constraints {parameter:constraint} format me likhe jate hain.
-------------------------------------
2️⃣ Route Constraints Ka Use Kyu Hota Hai?
✅ Security – Malicious inputs ko block karne ke liye.
✅ Validation – Route parameters me valid data ensure karne ke liye.
✅ Efficiency – Agar incorrect data aaye toh unnecessary processing avoid hoti hai.
---------------------------------
3️⃣ Common Route Constraints
Constraint	Use Case	Example
int	Sirf integer values allow karta hai	/products/{id:int}
bool	Sirf true ya false allow karta hai	/status/{active:bool}
datetime	Sirf valid DateTime values allow karta hai	/report/{date:datetime}
decimal	Sirf decimal numbers allow karta hai	/price/{amount:decimal}
guid	Sirf valid GUID allow karta hai	/users/{id:guid}
min(value)	Minimum integer value restrict karta hai	/age/{years:min(18)}
max(value)	Maximum integer value restrict karta hai	/discount/{percent:max(50)}
length(min,max)	String ki length ko restrict karta hai	/username/{name:length(3,10)}
alpha	Sirf alphabets allow karta hai (ASP.NET Core 7+)	/name/{username:alpha}
regex(pattern)	Regular expression ka use karta hai	/product/{code:regex(^[A-Z]{3}[0-9]{3}$)}
------------------------------------------------------
4️⃣ Example: Integer Route Constraint

app.MapGet("/products/{id:int}", (int id) =>
{
    return $"Product ID: {id}";
});
✅ /products/10 → Valid Request
❌ /products/abc → 404 Not Found
-------------------------------------------------------
5️⃣ Example: Boolean Route Constraint

app.MapGet("/status/{active:bool}", (bool active) =>
{
    return active ? "Active" : "Inactive";
});
✅ /status/true → Valid ("Active")
✅ /status/false → Valid ("Inactive")
❌ /status/yes → 404 Not Found
-----------------------------------------------------
6️⃣ Example: DateTime Route Constraint
app.MapGet("/report/{date:datetime}", (DateTime date) =>
{
    return $"Report Date: {date.ToShortDateString()}";
});
✅ /report/2024-04-01 → Valid
❌ /report/today → 404 Not Found
--------------------------------------------------------
7️⃣ Example: Minimum & Maximum Value Constraint

app.MapGet("/age/{years:min(18)}", (int years) =>
{
    return $"Age: {years}";
});
✅ /age/20 → Valid
❌ /age/15 → 404 Not Found
---------------------------------------------------
8️⃣ Example: Length Constraint

app.MapGet("/username/{name:length(3,10)}", (string name) =>
{
    return $"Username: {name}";
});
✅ /username/Ravi → Valid
✅ /username/JohnDoe → Valid
❌ /username/J → 404 Not Found
------------------------------------------------------
9️⃣ Example: Regex Constraint

app.MapGet("/product/{code:regex(^[A-Z]{3}[0-9]{3}$)}", (string code) =>
{
    return $"Product Code: {code}";
});
✅ /product/ABC123 → Valid
❌ /product/123ABC → 404 Not Found
--------------------------------------------------
🔟 Conclusion
✔ Route constraints se APIs aur routes secure aur efficient bante hain.
✔ Agar constraint match na kare toh automatic 404 Not Found error return hota hai.
✔ Multiple constraints ek sath bhi use kar sakte hain:

-----------------------
app.MapGet("/data/{id:int:min(1):max(100)}", (int id) => $"Data ID: {id}");
✔ ASP.NET Core 7+ me alpha constraint bhi available hai.