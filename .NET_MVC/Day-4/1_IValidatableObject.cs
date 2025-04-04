using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 1_IValidatableObject
    {
        
    }
}
-----------------------------------------
IValidatableObject in ASP.NET Core
Agar hume multiple properties ke basis pe custom validation likhni ho, toh IValidatableObject interface best option hai. Iska faida ye hai ki ye model ke andar hi validation logic likhne ki permission deta hai.
------------------------------------------------------------------------
🛠 Kab Use Karein?
✅ Jab multiple properties ko ek saath validate karna ho.
✅ Jab business logic based validation lagani ho.
✅ Jab model-specific validation chahiye, jo database ya kisi aur source pe dependent na ho.

👨‍💻 Example: Contact Info (Either Email or Phone Required)
Koi ek user jab form submit kare aur email ya phone me se ek required ho (dono empty nahi hone chahiye), toh IValidatableObject ka use karenge.
------------------------------------------------------------
1️⃣ Model Class me IValidatableObject Implement Karein

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
                new[] { "Email", "Phone" } // Ye error message dono fields ke liye apply hoga
            );
        }
    }
}
-------------------------------------------------------
2️⃣ Controller Me Validate Karna
[HttpPost("validate-contact")]
public IActionResult ValidateContact([FromBody] ContactInfo contact)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("Contact data is valid!");
}
--------------------------------------
Jab StartDate hamesha EndDate se pehle hona chahiye, toh validation aise likhenge:

public class EventModel : IValidatableObject
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate >= EndDate)
        {
            yield return new ValidationResult(
                "Start date must be before end date.",
                new[] { "StartDate", "EndDate" }
            );
        }
    }
}
-------------------------------------------
💡 Summary
✅ IValidatableObject Multiple property validation ke liye best hai.
✅ Model-specific validation likhne ke liye use hota hai.
✅ Agar complex validation chahiye jo database ya services pe depend ho, 
toh custom validation attribute ya dependency injection ka use karein.