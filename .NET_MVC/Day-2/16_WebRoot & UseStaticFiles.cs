using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-2
{
    public class 16_WebRoot & UseStaticFiles
    {
        
    }
}
----------------------------------------------------------
WebRoot & UseStaticFiles in ASP.NET Core 🚀
1️⃣ WebRoot Kya Hota Hai?
🔹 WebRoot ek folder hota hai jisme static files store ki jati hain.
🔹 Static files CSS, JavaScript, images, fonts, etc. ho sakti hain jo directly browser me serve ki jati hain.
🔹 Default WebRoot folder ka naam "wwwroot" hota hai.

WebRoot Folder Ki Default Structure
bash
Copy
Edit
/wwwroot
   ├── css/
   │   ├── site.css
   ├── js/
   │   ├── script.js
   ├── images/
   │   ├── logo.png
✅ ASP.NET Core sirf WebRoot folder ke andar ki static files ko serve karta hai.
❌ Agar koi file wwwroot ke bahar hai toh wo directly access nahi hogi.
-----------------------------------------------------------------
2️⃣ Static Files Serve Karne Ke Liye UseStaticFiles Ka Use
🔹 By default, static files serve nahi hoti jab tak UseStaticFiles() middleware use na karein.
🔹 Ye middleware wwwroot ke andar ki files ko public access dene ka kaam karta hai.

UseStaticFiles Middleware Ka Setup (Program.cs)
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Enable serving of static files from wwwroot
app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");

app.Run();
✅ Ab browser se directly static files ko access kar sakte hain.
----------------------------------------------------
3️⃣ Example: Static Files Ko Kaise Serve Karein?
Step 1: Static File Ko wwwroot Me Rakhein
/wwwroot
   ├── css/style.css
   ├── js/script.js
   ├── images/logo.png
Step 2: Static File Ka Use HTML Me Karein
html
Copy
Edit
<!DOCTYPE html>
<html>
<head>
    <link rel="stylesheet" href="/css/style.css">
    <script src="/js/script.js"></script>
</head>
<body>
    <img src="/images/logo.png" alt="Logo">
</body>
</html>
✅ Agar UseStaticFiles() middleware enabled hai toh ye files serve ho jayengi.
---------------------------------------------------
4️⃣ Custom WebRoot Path Set Karna
🔹 Agar hume wwwroot ke alawa kisi aur folder ko WebRoot banana hai, toh hum WebRootPath set kar sakte hain.

Example: Custom WebRoot Path Set Karna (Program.cs)
var builder = WebApplication.CreateBuilder(args);

// Custom WebRoot folder set karein
builder.WebHost.UseWebRoot("public");

var app = builder.Build();

app.UseStaticFiles();

app.Run();
✅ Ab ASP.NET Core static files ko public folder se serve karega.
---------------------------------------------------------------
5️⃣ Static Files Ko Security Se Serve Karna
🔹 Sirf Specific Folder Se Static Files Serve Karna:
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "secure-files")),
    RequestPath = "/files"
});
✅ Ab sirf /files path se secure-files folder ki files access ho sakti hain.
-----------------------------------------------------
6️⃣ Conclusion
✔ WebRoot (wwwroot) ek default folder hai jisme static files rakhi jati hain.
✔ UseStaticFiles() middleware ko enable karna zaroori hai static files serve karne ke liye.
✔ Custom WebRoot set kar sakte hain agar hume default wwwroot nahi chahiye.
✔ Static files ko secure tarike se serve karne ke liye StaticFileOptions ka use hota hai.