using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 5_Script Tag Helper
    {
        
    }
}
-------------------------------------
Script Tag Helper ki — ye Razor view me <script> tags ke sath kam aata hai ASP.NET Core me, aur isse tu JavaScript files ko smartly inject kar sakta hai, especially jab tu cache busting, versioning, ya environment based loading karna chahta hai.

🔹 Script Tag Helper kya hai?
Ye ek Razor Tag Helper hai jo ASP.NET Core me <script> tag ke sath lagta hai. Iska kaam:

Path resolve karna (wwwroot se)

Cache busting karna (via version)
--------------------------------------
CDN or environment specific script load karwana

✅ Syntax:

<script src="~/js/site.js" asp-append-version="true"></script>
🧠 Real-life Example:
Tu chahta hai ki user ke browser me kabhi purani JS file cache na ho. Jab bhi site.js update ho, browser uska latest version load kare.
---------------------------------
🧪 Breakdown:
🔸 src="~/js/site.js"
~ ka matlab hota hai root folder (i.e. wwwroot)

Razor samajh jaata hai ki actual file ka path kya hai

🔸 asp-append-version="true"
Ye query string me file ka hash append karta hai

Jaise: /js/site.js?v=1Dj9fhd03jk4

Har bar jab file change hoti hai, version change hota hai — browser new file load karta hai
------------------------------------------
🧾 Without Tag Helper:

<script src="/js/site.js"></script>  <!-- Old JS may get cached -->
--------------------------------
✅ With Script Tag Helper:

<script src="~/js/site.js" asp-append-version="true"></script> <!-- Always fresh -->
-----------------------------------
🧠 Extra Use: Environment-based Scripts

<environment include="Development">
    <script src="~/lib/jquery/jquery.js" asp-append-version="true"></script>
</environment>

<environment exclude="Development">
    <script src="https://cdn.jsdelivr.net/jquery.min.js" crossorigin="anonymous"></script>
</environment>
-------------------------------------
📌 Iska fayda:

Development me local JS

Production me CDN version
---------------------------------------------
🎯 Benefits:
🚫 Cache issues se bachav

📦 Correct version of JS always loaded

✅ Clean, short syntax

⚙️ Smart handling in different environments
-------------------------------------
🔚 Summary:
Feature	Script Tag Helper
Path resolving	✅ Supports ~
Cache busting	✅ With asp-append-version
Env-based loading	✅ Works with <environment>
CDN/local switching	✅ Easily handled
