using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 7_HTTP Logging Options
    {
        
    }
}
---------------------------------
HTTP Logging Options ASP.NET Core me configure karne ke liye hoti hain, jinke through tum decide kar sakte ho kya log karna hai, kitna body data log karna hai, kaunse headers log karne hain, aur kaunse media types allow karni hain logging ke liye.

🔧 HttpLoggingOptions – Properties
Ye options tum .AddHttpLogging(...) ke andar configure karte ho.

csharp
Copy
Edit
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096;
    options.ResponseBodyLogLimit = 4096;
    options.MediaTypeOptions.AddText("application/json");
    options.RequestHeaders.Add("User-Agent");
    options.ResponseHeaders.Add("Content-Type");
});
📘 Detailed Breakdown of Options:
1. LoggingFields
Define karta hai ki HTTP request/response me se kya-kya log karna hai.

Enum: HttpLoggingFields
-----------------------------------------------------
✅ Common Flags:
Value	Description
None	No logging
RequestPath	Logs URL path
RequestQuery	Logs query string
RequestHeaders	Logs headers from request
RequestBody	Logs body of the request
ResponseHeaders	Logs response headers
ResponseBody	Logs body of the response
All	Logs everything (recommended only for development)
------------------------------------------------------------
2. RequestBodyLogLimit
Kitna request body data (in bytes) log karna hai.

Default: 4096 (4 KB)
----------------------------------
options.RequestBodyLogLimit = 1024 * 10; // 10 KB
--------------------------------------------
3. ResponseBodyLogLimit
Kitna response body data (in bytes) log karna hai.

Default: 4096
---------------------------------------------------------
4. MediaTypeOptions
Body tabhi log hogi agar media type text/, application/json, ya koi custom type ho.

Tum custom media types allow kar sakte ho:

options.MediaTypeOptions.AddText("application/xml");

----------------------------------------------------
5. RequestHeaders / ResponseHeaders
Specify karo kaunse headers specifically log karne hain.


options.RequestHeaders.Add("Authorization"); // ⚠️ Only if safe
options.ResponseHeaders.Add("X-Custom-Header");
Pro Tip: Production me Authorization jese sensitive headers ko avoid karo.
-------------------------------------------------------------
🧪 Example: Minimal Logging

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseStatusCode;
});
-----------------------------------------
📁 Custom Logging Per Environment (Optional)

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}
