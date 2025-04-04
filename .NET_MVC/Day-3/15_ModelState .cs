using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 15_ModelState 
    {
        
    }
}
--------------------------------------------
ModelState in ASP.NET Core 🚀
1️⃣ ModelState Kya Hai?
🔹 ModelState ek built-in feature hai jo model validation aur data binding errors ko track karta hai.
🔹 Ye controller me request data ko validate karne ka kaam karta hai.
🔹 Agar ModelState.IsValid == false hai, iska matlab hai model validation fail ho gayi hai.
--------------------------------------------------------------------
2️⃣ ModelState Ka Basic Example
Agar ek User Registration API hai jo User Model ko accept karti hai:

public class User
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Range(18, 60, ErrorMessage = "Age must be between 18 and 60.")]
    public int Age { get; set; }
}
📌 Yahaan Required, EmailAddress, aur Range validation lagayi gayi hai.
-----------------------------------------------------------------
3️⃣ ModelState Validation Controller Me Kaise Kaam Karti Hai?
Agar client se koi invalid data aaye toh ModelState.IsValid false return karega.

[HttpPost("register")]
public IActionResult Register([FromBody] User user)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("User registered successfully!");
}
Kaise Kaam Karega?
🔹 Agar valid request aayi toh 200 OK return karega.
🔹 Agar invalid request aayi toh 400 Bad Request return karega aur validation errors dikhayega.
-----------------------------------------------------------------
4️⃣ ModelState Error Messages Kaise Milengi?
Agar ModelState invalid hai toh ModelState.Errors se error details nikal sakte hain.

[HttpPost("register")]
public IActionResult Register([FromBody] User user)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
        return BadRequest(new { Errors = errors });
    }

    return Ok("User registered successfully!");
}
📌 Agar koi validation fail hoti hai toh response me error messages json format me milengi.
-----------------------------------------------------------
5️⃣ ModelState Me Errors Manually Add Karna
Kabhi-kabhi hume custom error messages manually add karni padti hai.

[HttpPost("custom-validation")]
public IActionResult ValidateData([FromBody] User user)
{
    if (user.Name == "Admin")
    {
        ModelState.AddModelError("Name", "Admin is not allowed as a username.");
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("Validation passed!");
}
📌 Agar user Name = "Admin" bhejega toh manually error add ho jayegi.
----------------------------------------------------------------------
6️⃣ ModelState Clear Karna
Agar ModelState me pehle se errors hain aur hume naye validation ke liye clear karna ho toh ModelState.Clear() use kar sakte hain.

ModelState.Clear();
🔹 Ye tab useful hota hai jab ek hi page par multiple actions ho aur naye request me old validation errors na dikhein.
-------------------------------------------------
7️⃣ Summary:
✅ ModelState automatic validation check karta hai.
✅ Agar validation fail hoti hai toh ModelState.IsValid == false return hota hai.
✅ Hum manually ModelState.AddModelError() se custom errors add kar sakte hain.
✅ Errors ko extract karne ke liye ModelState.Values.SelectMany(v => v.Errors) use hota hai.
✅ Agar ModelState ko reset karna ho toh ModelState.Clear() use kar sakte hain.