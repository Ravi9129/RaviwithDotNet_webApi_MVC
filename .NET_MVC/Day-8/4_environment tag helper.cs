using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 4_environment tag helper
    {
        
    }
}
-----------------------------
<environment> tag helper ki jo Razor Views mein use hota hai — aur kaise alag-alag environment (jaise Development, Production, Staging) ke basis par HTML content ya scripts render karne ke liye use hota hai.

📌 What is <environment> Tag Helper?
Ye Razor ka ek built-in tag helper hai jo sirf us waqt kaam karta hai jab ASP.NET Core app kisi specific environment mein ho.
---------------------------------- 
✅ Syntax:
<environment include="Development">
    <!-- Dev-specific HTML/JS/CSS -->
</environment>

<environment exclude="Development">
    <!-- Prod/Staging specific HTML/JS/CSS -->
</environment>
💡 Real Use-case:
--------------------------------
🔧 Problem:
Tu chah raha hai ki Development mein unminified scripts load ho jayein (easy debugging ke liye), lekin Production mein minified version load ho performance ke liye.

✅ Solution:
<environment include="Development">
    <script src="~/js/site.js"></script>
</environment>

<environment exclude="Development">
    <script src="~/js/site.min.js"></script>
</environment>
------------------------------------------------
👆 Is example mein:

Jab app Development mode mein chalegi → site.js load hoga.

Jab Production ya koi aur env hoga → site.min.js load hoga.
-----------------------------------------------------
🎯 Multiple Environments Include:
<environment include="Development,Staging">
    <link rel="stylesheet" href="~/css/debug.css" />
</environment>
Ye debug.css sirf Development aur Staging mein load karega.

⚙️ Kaha se ye Environment set hota hai?
-----------------------------------------------
launchSettings.json mein hota hai:
"profiles": {
  "IIS Express": {
    "environmentVariables": {
      "ASPNETCORE_ENVIRONMENT": "Development"
    }
  }
}
Ya fir system variable se ya manually Program.cs mein set kar sakta hai.
-----------------------------------------
🧪 Bonus Example:
<environment include="Development">
    <div class="alert alert-info">You're in Development Mode</div>
</environment>