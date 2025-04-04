using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 18_Custom Validation with Multiple Properties
    {
        
    }
}
------------------------------------
Custom Validation with Multiple Properties in ASP.NET Core
Agar hume ek se zyada properties ko saath me validate karna ho,
 toh hum IValidatableObject interface ya Custom Validation Attribute ka use kar sakte hain.
---------------------------------------------
1️⃣ IValidatableObject Interface (Best for Multiple Property Validation)
Agar multiple properties ek doosre se dependent hain, toh IValidatableObject interface best hai.

Example: Email or Phone Required (At least one should be provided)
🔹 Step 1: Model me IValidatableObject Implement Karein

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ContactInfo : IValidatableObject
{
    public string Email { get; set; }
    public string Phone { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
        {
            yield return new ValidationResult(
                "Either Email or Phone must be provided.",
                new[] { "Email", "Phone" }
            );
        }
    }
}
---------------------------------------------------
🔹 Step 2: Validate in Controller

[HttpPost("validate-contact")]
public IActionResult ValidateContact([FromBody] ContactInfo contact)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("Contact data is valid!");
}
📌 Agar Email aur Phone dono empty honge, toh validation fail ho jayegi! ✅
----------------------------------------------------------------------
2️⃣ Custom Validation Attribute with Multiple Properties
Agar hume multiple properties ko validate karna ho aur reuse bhi karna ho, toh Custom Validation Attribute banana best rahta hai.

Example: Start Date Should Be Before End Date
🔹 Step 1: Custom Validation Attribute Banayein

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class DateRangeValidationAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateRangeValidationAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var comparisonValue = validationContext.ObjectType.GetProperty(_comparisonProperty)
            .GetValue(validationContext.ObjectInstance);

        if (value != null && comparisonValue != null && (DateTime)value > (DateTime)comparisonValue)
        {
            return new ValidationResult($"Start date must be before end date.");
        }

        return ValidationResult.Success;
    }
}
-------------------------------------------------
🔹 Step 2: Model me Apply Karein

public class EventModel
{
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    [DateRangeValidation(nameof(StartDate))]
    public DateTime EndDate { get; set; }
}
-----------------------------------------------------------
🔹 Step 3: Validate in Controller

[HttpPost("validate-event")]
public IActionResult ValidateEvent([FromBody] EventModel eventModel)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("Event data is valid!");
}
📌 Agar StartDate EndDate se bada hoga, toh validation fail ho jayegi! ✅

3️⃣ Dependency Injection Based Custom Validation
Agar hume database ya external services ka data check karna ho, toh Dependency Injection use kar sakte hain.

Example: Username & Email Should Be Unique (Check from Database)
----------------------------------------------------------
🔹 Step 1: Service Interface Banayein

public interface IUserValidationService
{
    bool IsUserValid(string username, string email);
}

public class UserValidationService : IUserValidationService
{
    private static List<(string Username, string Email)> existingUsers = new()
    {
        ("admin", "admin@example.com"),
        ("testUser", "test@example.com")
    };

    public bool IsUserValid(string username, string email)
    {
        return !existingUsers.Exists(u => u.Username == username || u.Email == email);
    }
}
-----------------------------------------------
🔹 Step 2: Custom Validation Attribute Banayein
using Microsoft.Extensions.DependencyInjection;

public class UniqueUserAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = validationContext.GetService<IUserValidationService>();
        var user = (UserModel)validationContext.ObjectInstance;

        if (service != null && !service.IsUserValid(user.Username, user.Email))
        {
            return new ValidationResult("Username or Email is already taken.");
        }

        return ValidationResult.Success;
    }
}
-----------------------------------------------------------
🔹 Step 3: Model me Apply Karein
public class UserModel
{
    public string Username { get; set; }
    public string Email { get; set; }

    [UniqueUser]
    public string ValidationTrigger { get; set; } // Just a dummy property to trigger validation
}
----------------------------------------------
🔹 Step 4: Dependency Injection Setup in Program.cs
builder.Services.AddSingleton<IUserValidationService, UserValidationService>();
-------------------------------------------------
🔹 Step 5: Validate in Controller

[HttpPost("register-user")]
public IActionResult RegisterUser([FromBody] UserModel user)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("User registered successfully!");
}
📌 Agar Username ya Email database me pehle se exist karega, toh error ayegi! ✅
-------------------------------------------------
4️⃣ Summary
✅ IValidatableObject Interface → Jab multiple properties ek saath validate karni ho.
✅ Custom Validation Attribute → Jab ek ya multiple properties ke liye reusable validation likhni ho.
✅ Dependency Injection Based Validation → Jab database ya external service ka data validate karna ho.