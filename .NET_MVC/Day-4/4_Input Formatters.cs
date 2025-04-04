using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 4_Input Formatters
    {
        
    }
}
------------------------------------
🔹 Input Formatters in ASP.NET Core
📌 Kya Hai?
ASP.NET Core Input Formatters request ke body me aane wale data ko C# object me convert karte hain. Ye kaam [FromBody] attribute ke sath hota hai.

📌 Kaam Kya Hai?
✅ Request body me jo data aata hai (JSON, XML, form-data, text, etc.), usko deserialization karke C# model me bind karta hai.
✅ Input Formatters ASP.NET Core pipeline ka part hote hain.

🔹 Types of Input Formatters in ASP.NET Core
ASP.NET Core by default 2 input formatters support karta hai:

JSON Input Formatter (System.Text.Json ya Newtonsoft.Json)

XML Input Formatter (XmlSerializerInputFormatter ya DataContractSerializerInputFormatter)

Agar controller method me [FromBody] use kiya gaya hai, toh ASP.NET Core automatically ek suitable input formatter select karega.
-------------------------------------------------------
🛠 Example 1: JSON Input Formatter (Default)
✅ ASP.NET Core JSON data ko automatically deserialize karta hai using System.Text.Json.

👨‍💻 Model Class
public class UserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}
-----------------------------------------
🚀 API Controller
[HttpPost("create-user")]
public IActionResult CreateUser([FromBody] UserModel user)
{
    return Ok($"User {user.Name} with email {user.Email} created successfully!");
}
------------------------------------
📤 Client Request (JSON Body)

{
    "name": "John Doe",
    "email": "john@example.com"
}
🎯 Response
User John Doe with email john@example.com created successfully!
--------------------------------------------------
🛠 Example 2: Enabling XML Input Formatter
By default, ASP.NET Core sirf JSON support karta hai. Agar hume XML bhi support karna hai toh hume manually enable karna padega.

📌 Steps to Enable XML Formatter
--------------------------------------------------
1️⃣ Startup Configuration (Program.cs or Startup.cs)

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();  // XML Formatter Enable
    -------------------------------------------------------
2️⃣ 👨‍💻 API Controller
[HttpPost("create-user")]
public IActionResult CreateUser([FromBody] UserModel user)
{
    return Ok($"User {user.Name} with email {user.Email} created successfully!");
}
--------------------------------------------
3️⃣ 📤 Client Request (XML Body)

<UserModel>
    <Name>John Doe</Name>
    <Email>john@example.com</Email>
</UserModel>
4️⃣ 🎯 Response

User John Doe with email john@example.com created successfully!

--------------------------------------------------------------------------

🔹 Custom Input Formatter (Advanced)
Kabhi kabhi hume custom format (jaise CSV, Protobuf, ya custom XML) parse karna hota hai. Iske liye hume Custom Input Formatter banana padta hai.
🛠 Example: Custom CSV Input Formatter
1️⃣ 👨‍💻 Create Custom CSV Formatter

using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Threading.Tasks;

public class CsvInputFormatter : TextInputFormatter
{
    public CsvInputFormatter()
    {
        SupportedMediaTypes.Add("text/csv");
        SupportedEncodings.Add(Encoding.UTF8);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var request = context.HttpContext.Request;
        using var reader = new StreamReader(request.Body, encoding);
        var line = await reader.ReadLineAsync();
        var parts = line.Split(',');

        var user = new UserModel
        {
            Name = parts[0],
            Email = parts[1]
        };

        return await InputFormatterResult.SuccessAsync(user);
    }
}
---------------------------------------------------
2️⃣ 👨‍💻 Register CSV Formatter in Program.cs

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Add(new CsvInputFormatter());
});
-----------------------------------------------------------
3️⃣ 📤 Client Request (CSV Body)
John Doe,john@example.com
-----------------------------
4️⃣ 🎯 Response
User John Doe with email john@example.com created successfully!
-------------------------------------
🔹 Summary
✅ Input Formatters automatically request body ko C# object me convert karte hain.
✅ Default Formatters: JSON (default) & XML (manual enable karna padta hai).
✅ Custom Formatters: Agar koi custom format (CSV, Protobuf, etc.) chahiye toh Custom Input Formatter likhna padta hai.

