using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 17_Custom Validation
    {
        
    }
}
------------------------------------------
Custom Validation in ASP.NET Core 🚀
Custom validation ka use tab hota hai jab built-in attributes (like [Required], [StringLength]) hamari requirements ko fulfill nahi karte. ASP.NET Core me custom validation 2 tareeke se ki ja sakti hai:

Custom Validation Attribute (Class Based)

IValidatableObject Interface (Model Based)

1️⃣ Custom Validation Attribute (Class Based)
Yeh approach use hoti hai jab kisi ek ya multiple models ke liye ek hi validation logic apply karni ho.
--------------------------------------------------
Example: Special Characters Not Allowed
🔹 Step 1: Custom Validation Class Banayein

using System.ComponentModel.DataAnnotations;
using System.Linq;

public class NoSpecialCharactersAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null && value.ToString().Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return new ValidationResult("Special characters are not allowed.");
        }
        return ValidationResult.Success;
    }
}
------------------------------------------
🔹 Step 2: Isko Model me Apply Karein

public class User
{
    [NoSpecialCharacters]
    public string Username { get; set; }
}
---------------------------------
🔹 Step 3: Validate in Controller

[HttpPost("register")]
public IActionResult Register([FromBody] User user)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("User registered successfully!");
}
📌 Agar user Username me special characters daalega toh error ayegi! ✅
--------------------------------------------------------------------------
2️⃣ IValidatableObject Interface (Model Based)
Agar validation multiple fields ko ek saath check karni ho, toh IValidatableObject interface use kar sakte hain.

Example: Age Should Be Greater Than 18 If Marital Status is Married
🔹 Step 1: Model me IValidatableObject Implement Karein

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Person : IValidatableObject
{
    public int Age { get; set; }
    public string MaritalStatus { get; set; } // Single or Married

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MaritalStatus == "Married" && Age < 18)
        {
            yield return new ValidationResult("Age must be at least 18 if married.", new[] { "Age" });
        }
    }
}
----------------------------------------------
🔹 Step 2: Validate in Controller

[HttpPost("validate-person")]
public IActionResult ValidatePerson([FromBody] Person person)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("Person data is valid!");
}
📌 Agar koi Married hai par age 18 se kam hai toh validation fail ho jayegi! ✅
-----------------------------------------------------------
3️⃣ Custom Validation Using Dependency Injection
Agar validation me database ya kisi external service ka data check karna ho, toh Dependency Injection ka use kar sakte hain.

Example: Username Should Be Unique (Check from Database)
🔹 Step 1: Custom Validator Service Banayein

public interface IUniqueUsernameService
{
    bool IsUsernameUnique(string username);
}

public class UniqueUsernameService : IUniqueUsernameService
{
    private static List<string> existingUsernames = new List<string> { "admin", "user123", "testUser" };

    public bool IsUsernameUnique(string username)
    {
        return !existingUsernames.Contains(username);
    }
}
--------------------------------------
🔹 Step 2: Custom Validation Attribute Banayein

using Microsoft.Extensions.DependencyInjection;

public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = validationContext.GetService<IUniqueUsernameService>();
        if (service != null && value != null && !service.IsUsernameUnique(value.ToString()))
        {
            return new ValidationResult("Username already exists. Choose a different one.");
        }
        return ValidationResult.Success;
    }
}
--------------------------------------------------
🔹 Step 3: Model me Apply Karein

public class User
{
    [UniqueUsername]
    public string Username { get; set; }
}
---------------------------------------------------------
🔹 Step 4: Dependency Injection Setup in Program.cs

builder.Services.AddSingleton<IUniqueUsernameService, UniqueUsernameService>();
---------------------------------------------------
🔹 Step 5: Validate in Controller

[HttpPost("register")]
public IActionResult Register([FromBody] User user)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("User registered successfully!");
}
📌 Agar Username database ya list me already exist karega toh error ayegi! ✅
--------------------------------------------------------
4️⃣ Summary
✅ Custom Validation Attribute → Jab ek ya multiple models ke liye reusable validation likhni ho.
✅ IValidatableObject Interface → Jab multiple properties ek saath validate karni ho.
✅ Dependency Injection Based Validation → Jab database ya external service ka data validate karna ho.