using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 6_Image Tag Helper 
    {
        
    }
}
----------------------------------------------
Image Tag Helper ki — ye Razor view me <img> tag ke saath use hota hai, ASP.NET Core me. Jaise Script Tag Helper JS files ke liye smart kaam karta hai, waise hi Image Tag Helper images ke path, versioning, aur optimization me madad karta hai.

🖼️ Kya hai Image Tag Helper?
Image Tag Helper ASP.NET Core ka ek feature hai jo <img> tag ko Razor ke saath integrate karta hai, taki:

Tu wwwroot folder se image load kar sake using ~

Automatic versioning ho sake (cache busting)

Clean aur maintainable code ban sake

✅ Syntax:

<img src="~/images/logo.png" asp-append-version="true" />
----------------------------------
🧪 Breakdown:
🔹 src="~/images/logo.png"
~ ka matlab hai root directory (wwwroot)

Tu relative path use kar sakta hai — easy to manage

🔹 asp-append-version="true"
Ye image file ka hash append karta hai as query string

Jaise: /images/logo.png?v=6hfd8724hf8

Jab file change hoti hai, hash change hota hai — browser fresh image load karta hai
--------------------------------
🧠 Real-life Scenario:
Maan le tu logo.png update karta hai, but user ke browser me old cached image load ho raha hai. Agar asp-append-version="true" use karega to image ka version badlega and new image load hogi — no cache issue.
-------------------------------
🧾 Without Tag Helper:

<img src="/images/logo.png" />
-------------------------------------
✅ With Image Tag Helper:

<img src="~/images/logo.png" asp-append-version="true" />
-----------------------------------------------
🌐 Environment-based Images (optional):
<environment include="Development">
    <img src="~/images/dev-logo.png" asp-append-version="true" />
</environment>

<environment exclude="Development">
    <img src="https://cdn.example.com/logo.png" />
</environment>
---------------------------------------------
📦 Benefits:
🧠 Smart path resolving

🌀 Automatic cache busting

🌍 Easy environment-specific switching

✅ Better maintainability

✨ Razor syntax ke sath clean code
--------------------------------------------------
🔚 Summary (ek line me):
Image Tag Helper Razor ke <img> tags ko smarter banata hai — cache busting, cleaner paths, aur environment ke hisaab se image load karne me help karta hai.

