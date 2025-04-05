using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 1_layout views in sections
    {
        
    }
}
------------------------------------
Layout Views me Sections ka use kaise hota hai.

Yeh concept tab aata hai jab tu layout me aisi jagah banana chahta hai jahan child views apna custom content inject kar saken — jaise custom script, header, footer ya sidebar ke liye.

🔥 Real Scenario:
Tu chah raha hai ki:

Layout me ek placeholder ho, jahan koi bhi view apna custom JavaScript ya CSS de sake

But wo optional ho — har view me mandatory na ho
-------------------------------------------------
✅ Step-by-Step Explanation
🔹 Step 1: Layout file me RenderSection use karo
<!-- Views/Shared/_Layout.cshtml -->
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"]</title>
    <link href="site.css" rel="stylesheet" />

    @RenderSection("HeadScripts", required: false) <!-- Optional Section -->
</head>
<body>
    <div class="main-body">
        @RenderBody()
    </div>

    @RenderSection("BottomScripts", required: false) <!-- Optional Section -->
</body>
</html>
------------------------------------
🔹 Step 2: Ab koi bhi view me tu section define kar sakta hai
<!-- Views/Home/Index.cshtml -->
@{
    ViewData["Title"] = "Home Page";
}

<h1>Welcome Home</h1>

@section BottomScripts {
    <script>
        console.log("This script is only for Home page");
    </script>
}
----------------------------
🔹 Section without RenderSection?
Agar layout me @RenderSection("BottomScripts") nahi hai, aur tu view me likh dega @section BottomScripts, toh error aayega:

The layout page does not define section 'BottomScripts'
---------------------------------------------------
🔹 Agar tu required section banata hai
@RenderSection("PageHeader", required: true)
Aur agar koi view me @section PageHeader nahi hai, toh app crash ho jaayega with error.
------------------------------------------------------------
🧠 Summary Table (Bina Table ke Samjhaun):
@RenderBody() → Jahan view ka main content inject hota hai

@RenderSection("name") → View ke andar custom block inject hota hai

required: true → Zaroori hai view me hona

required: false → Optional hai
---------------------------------------------------
✅ Use Cases of Sections:
Per-view custom JS ya CSS

Modal ya popup jisko view se inject karna hai

Dynamic page header ya breadcrumb

Alert messages, banners
-------------------------------------------
💡 Best Practice:
@RenderBody() always hona chahiye layout me (mandatory)

Sections ko optional hi rakho, unless sab views me zarurat ho

Scripts hamesha @RenderSection("BottomScripts") me inject karo (performance boost)