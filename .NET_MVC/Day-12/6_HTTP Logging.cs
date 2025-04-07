using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 6_HTTP Logging
    {
        
    }
}
---------------------------------
HTTP Logging .NET me ek powerful feature hai jo HTTP requests and responses ka details (headers, body, status codes, etc.) log karta hai. Ye debugging, tracing, aur security ke liye kaafi useful hota hai.

✅ What is HTTP Logging in .NET?
HTTP Logging middleware incoming and outgoing HTTP traffic ko log karta hai.
---------------------------------------------------------------
🧱 Add HTTP Logging in ASP.NET Core
🔧 Step 1: Add Middleware in Program.cs or Startup.cs

var builder = WebApplication.CreateBuilder(args);

// Add HTTP Logging Service
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;

    // Optional: Customize headers/body logging
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.MediaTypeOptions.AddText("application/json");
    logging.RequestHeaders.Add("User-Agent");
});

var app = builder.Build();

// Enable Middleware
app.UseHttpLogging();

app.UseRouting();
app.MapControllers();
app.Run();
--------------------------------------------------
🔍 What It Logs
By default (when HttpLoggingFields.All is used):

HTTP Method (GET, POST, etc.)

Request Path and Query

Headers

Status Code

Request/Response Body (up to limit)

Content Types
----------------------------------------------------
🧪 Example Log Output

info: Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware
      Request:
      Protocol: HTTP/1.1
      Method: GET
      Path: /api/products
      Headers:
      User-Agent: PostmanRuntime/7.28.4

info: Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware
      Response:
      StatusCode: 200
      Content-Type: application/json
      -------------------------------------------------------------
🔐 Note on Body Logging
By default, body is only logged for media types like text/*, application/json, etc.

Binary files (like images) are not logged for performance & security.
-------------------------------------------------------
💡 Tip: Customize Fields

logging.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders |
                        HttpLoggingFields.ResponsePropertiesAndHeaders;
                        -------------------------------------------------------------------------------------
🛑 Caution
Don’t log sensitive data (passwords, tokens) in production.

Always configure HTTP logging wisely with environment-based filters.