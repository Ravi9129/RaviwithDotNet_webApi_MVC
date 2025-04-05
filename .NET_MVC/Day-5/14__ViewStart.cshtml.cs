using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 14__ViewStart.cshtml
    {
        
    }
}
-------------------------
_ViewStart.cshtml ke baare mein — ye ASP.NET Core MVC ka ek special file hai jo har view render hone se pehle execute hoti hai. Chal isko real-life example se deep samjhte hain:

🔥 Q: _ViewStart.cshtml kya hoti hai?
Yeh ek Razor file hoti hai jo har view ke render hone se pehle chalti hai.

🔹 Iska kaam hota hai common code ya layout setting ko sabhi views ke liye centralize kar dena.

File Path: Views/_ViewStart.cshtml

✅ Real Scenario:
Tu chah raha hai ki tere sabhi views ek hi layout ko use karein, toh har view me manually layout set karne ke bajay, ek hi jagah _ViewStart.cshtml me define kar de.
---------------------------------
🔹 Step 1: _ViewStart.cshtml banana
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
Yeh har view ke load hone se pehle ye layout assign kar dega.
-----------------------------------------------------
🔹 Step 2: Ab har view me ye likhne ki zarurat nahi:
<!-- Pehle har view me likhna padta tha -->
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
Ab tu ye hata sakta hai, because _ViewStart.cshtml already set kar raha hai.

💡 Aur kya kar sakte hain _ViewStart.cshtml me?
-----------------------------------------
Common ViewData:

@{
    ViewData["AppName"] = "MyApp";
}
-----------------------------------------------
Theme select ya dynamic layout:

@{
    var isAdmin = true;
    Layout = isAdmin ? "~/Views/Shared/_AdminLayout.cshtml" : "~/Views/Shared/_UserLayout.cshtml";
}
-----------------------------------
🎯 Use Karne ke Fayde:
Fayda	Kya hota hai
DRY Principle follow hota hai	Layout har view me likhne ki zarurat nahi
Centralized layout config	Ek jagah change karo, sabhi views pe effect
Dynamic layouts possible	Role ya route ke basis pe alag-alag layouts set kar sakte ho
Faster dev speed	Kam duplication, clean architecture
--------------------------------------------
🔄 Important Note:
Agar kisi specific view me tu custom layout set karega, toh wo override kar deta hai _ViewStart.cshtml ka layout.

@{
    Layout = "~/Views/Shared/_CustomLayout.cshtml";
}
-----------------------------------------
📌 Summary:
_ViewStart.cshtml har view se pehle execute hoti hai

Mostly used for Layout assign karna

Ek hi jagah se sab views ka layout control ho jata hai

Can have ViewData, conditionals, or logic for layout