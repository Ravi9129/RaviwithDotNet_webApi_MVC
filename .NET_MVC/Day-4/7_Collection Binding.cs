using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 7_Collection Binding
    {
        
    }
}
------------------------------------
Collection Binding in ASP.NET Core
📌 Kya Hai?
Collection Binding ek technique hai jisme ASP.NET Core automatically arrays, lists, dictionaries ya complex objects ko HTTP request se map kar leta hai.

🔹 Example:
Agar query string, form data, ya JSON body me multiple values hain, toh wo ek collection (List, Array, Dictionary) me bind ho sakti hain.

🚀 Kaise Kaam Karta Hai?
ASP.NET Core Model Binding Engine automatically query string, route data, form data, ya JSON body se collections bind karta hai.

💡 ASP.NET Core inn jagahon se collection ko bind karta hai:
1️⃣ Query String: ?ids=1&ids=2&ids=3 → List<int> bind hoga.
2️⃣ Route Data: /users/1/2/3 → List<int> bind hoga.
3️⃣ Form Data: POST request ke andar values hongi jo List ya Array me convert ho sakti hain.
4️⃣ JSON Body: Agar POST request me JSON array hai toh usko bind kiya ja sakta hai.
--------------------------------------------------
🛠 Example 1: Query String se List<int> Bind Karna
👨‍💻 Controller Method
[HttpGet("get-users")]
public IActionResult GetUsers([FromQuery] List<int> ids)
{
    return Ok($"Received User IDs: {string.Join(", ", ids)}");
}
-----------------------------------
📤 Client Request:
GET /get-users?ids=1&ids=2&ids=3
------------------------------
📥 Response:
Received User IDs: 1, 2, 3
✅ Query String me multiple ids ko List<int> me bind kar diya!
--------------------------------------------
🛠 Example 2: Route Data se Collection Bind Karna
Agar tum /users/1/2/3 route ke andar values bhej rahe ho, toh unko bhi bind kiya ja sakta hai.
------------------------------------
👨‍💻 Route Setup
[HttpGet("users/{ids}")]
public IActionResult GetUsers([FromRoute] List<int> ids)
{
    return Ok($"User IDs from Route: {string.Join(", ", ids)}");
}
-------------------------------------------
📤 Client Request:

GET /users/1,2,3
---------------------------------------
📥 Response:

User IDs from Route: 1, 2, 3
✅ Route se List<int> bind ho gaya!
---------------------------------------------------
🛠 Example 3: JSON Body se List<Object> Bind Karna
Agar tum POST request me JSON bhej rahe ho, toh wo List<Model> ke andar bind ho sakti hai.
-----------------------------
👨‍💻 Model Class
public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
---------------------------------------------------
👨‍💻 Controller Method
[HttpPost("add-users")]
public IActionResult AddUsers([FromBody] List<UserModel> users)
{
    return Ok($"Received {users.Count} users.");
}
-----------------------------------------------
📤 Client Request (JSON Body):

[
    { "Id": 1, "Name": "John" },
    { "Id": 2, "Name": "Doe" }
]
--------------------------
📥 Response:
Received 2 users.
✅ JSON Body se List<UserModel> bind ho gaya!
--------------------------------------------------------------------
🛠 Example 4: Dictionary Binding
Agar request me key-value pairs hain, toh wo Dictionary me bhi bind ho sakte hain.
----------------------------------------
👨‍💻 Controller Method
[HttpGet("get-user-scores")]
public IActionResult GetUserScores([FromQuery] Dictionary<string, int> scores)
{
    return Ok(scores);
}
--------------------------------------------------------
📤 Client Request:

GET /get-user-scores?scores[John]=90&scores[Jane]=85
--------------------------------------------------------
📥 Response:
{
    "John": 90,
    "Jane": 85
}
✅ Dictionary<string, int> bind ho gaya!
------------------------------------------------------------
📌 Summary
✅ Query String, Route Data, JSON Body, aur Form Data se collection bind ho sakti hai.
✅ List<T>, Array[], Dictionary<TKey, TValue> bind ho sakti hain.
✅ [FromQuery], [FromRoute], [FromBody] ka sahi use karna zaroori hai.