using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-13
{
    public class 12_short-circuiting action filter 
    {
        
    }
}
----------------------------------------
 short-circuiting action filter ek powerful technique hai jahan tu controller action ko execute hone se pehle hi rok sakta hai — 
 jaise ek gatekeeper hota hai. Iska use real-world mein bahut hota hai — 
 authentication, IP blocking, custom policy, maintenance mode jaise scenarios mein.

🔍 What is Short-Circuiting in Action Filter?
Short-circuiting ka matlab hai:

Filter action ke execution se pehle hi request ko terminate kar de aur apni custom response return kar de.

Tu await next() ko call nahi karta, isse controller action kabhi execute nahi hota.
--------------------------------------------------
💡 Real World Scenario
Example: Tu chah raha hai ke agar request ek specific IP se aayi ho (e.g. from a banned user or bot), toh usse seedha block kar de.

🔧 How to Implement Short-Circuiting in an Action Filter

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

public class IpBlockFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress?.ToString();

        // Example banned IP
        if (remoteIp == "192.168.1.10")
        {
            context.Result = new ContentResult
            {
                Content = "Access Denied - IP Blocked",
                StatusCode = 403
            };

            // ❌ Short-circuiting: don't call next()
            return;
        }

        // ✅ Continue to action
        await next();
    }
}
---------------------------------------------
🧪 Output Behavior
Agar IP match karti hai → controller action execute nahi hota, sirf filter ka response milta hai.

Agar IP match nahi karti → action normal tarike se execute hota hai.
--------------------------------------------
🔁 Where Can You Use Short-Circuiting?
Scenario	Description
🚫 IP blocking	Block requests from specific IP addresses.
🔐 Token Validation	Agar token invalid hai, toh custom 401 return kar do.
🛑 Maintenance Mode	Server down message before any action hits.
💳 Subscription Check	Paid plan required before access.
-------------------------------------------------
🔐 Example 2: Short-Circuit on Missing Header

if (!context.HttpContext.Request.Headers.ContainsKey("X-Custom-Header"))
{
    context.Result = new BadRequestObjectResult("Missing required header");
    return; // ❌ Action won't be called
}
---------------------------------
🔧 Registering the Filter

services.AddControllersWithViews(options =>
{
    options.Filters.Add<IpBlockFilter>();
});
Or use [ServiceFilter] or [TypeFilter] on controller/action.
---------------------------------------------
🧠 Pro Tip
Short-circuiting = don't call await next().

Tu context.Result = new JsonResult(...) bhi use kar sakta hai for API.

Logging + early rejection is best for performance and security.

