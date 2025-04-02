using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 5_UseWhen
    {
        
    }
}
-------------------------------------
app.UseWhen() in ASP.NET Core
1️⃣ UseWhen Kya Hota Hai?
app.UseWhen() ek conditional middleware hai jo sirf specific conditions par execute hota hai. Agar condition true ho, toh middleware execute hoga, warna bypass ho jayega.

💡 Use Case: Jab tumhe sirf kuch specific requests par middleware chalana ho, jaise:
✔ API requests par logging
✔ Admin routes par authentication
✔ Mobile aur Web clients ke liye alag processing

2️⃣ Basic Syntax

app.UseWhen(condition, subPipeline => 
{
    subPipeline.UseMiddleware<MyCustomMiddleware>();
});
✅ Condition true hone par middleware execute hoga.
-----------------------------------------------------------------------
3️⃣ Real-World Examples of UseWhen
Example 1: Sirf /admin Route Par Middleware Lagana

app.UseWhen(context => context.Request.Path.StartsWithSegments("/admin"), appBuilder =>
{
    appBuilder.UseMiddleware<AdminAuthMiddleware>(); // 👈 Ye middleware sirf "/admin" routes ke liye chalega
});
✅ Agar request /admin se start ho rahi hai, toh AdminAuthMiddleware execute hoga.
-------------------------------------------------------
Example 2: Sirf JSON Requests Ke Liye Logging Middleware
app.UseWhen(context => context.Request.ContentType == "application/json", appBuilder =>
{
    appBuilder.UseMiddleware<JsonLoggingMiddleware>();
});
✅ Sirf JSON requests ke liye logging middleware chalega.
-------------------------------------------------
Example 3: Mobile vs Web Clients Ke Liye Alag Middleware

app.UseWhen(context => context.Request.Headers["User-Agent"].ToString().Contains("Mobile"), appBuilder =>
{
    appBuilder.UseMiddleware<MobileSpecificMiddleware>(); // 👈 Sirf Mobile Clients Ke Liye
});
✅ Agar request mobile se aayi hai, toh alag middleware execute hoga.
--------------------------------------------
4️⃣ Conclusion
✔ UseWhen() conditional middleware execution ke liye useful hai.
✔ Agar ek middleware sirf specific conditions me chahiye, toh UseWhen() best hai.
✔ Performance optimize karne me madad karta hai, kyunki unnecessary middleware execute nahi hota.