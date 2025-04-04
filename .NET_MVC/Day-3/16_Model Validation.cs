using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 16_Model Validation
    {
        
    }
}
--------------------------------------------
Model Validation in ASP.NET Core 🚀
Model validation ka use user input ko validate karne ke liye hota hai taaki invalid data database tak na pahunche. ASP.NET Core me validation attributes use karke automatic validation ho sakti hai.
------------------------------
1️⃣ Basic Validation Attributes
(1) Required
🔹 Kisi bhi property ko mandatory banane ke liye.

public class User
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}
📌 Agar user Name field na de toh error message milega.
--------------------------------------------------------------
(2) StringLength
🔹 String ki minimum aur maximum length specify karne ke liye.

public class User
{
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
    public string Name { get; set; }
}
📌 Agar Name 5 se kam ya 50 se zyada ho toh error ayegi.
---------------------------------------------------------------
(3) MinLength & MaxLength
🔹 Yeh StringLength ka alternative hai, par yeh only min ya max specify karne ke liye useful hai.

public class User
{
    [MinLength(3, ErrorMessage = "Name must have at least 3 characters.")]
    [MaxLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
    public string Name { get; set; }
}
(4) Range
----------------------------------------------------
🔹 Kisi bhi numeric value ki range define karne ke liye.

public class User
{
    [Range(18, 60, ErrorMessage = "Age must be between 18 and 60.")]
    public int Age { get; set; }
}
📌 Agar Age 18-60 ke beech nahi hai toh validation fail ho jayegi.
-------------------------------------------------------
(5) RegularExpression
🔹 Pattern match karne ke liye (e.g., email, phone number, password).

public class User
{
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only letters are allowed.")]
    public string Name { get; set; }
}
📌 Agar Name me numbers ya special characters aye toh error milegi.
------------------------------------------------------
(6) EmailAddress
🔹 Valid email format check karne ke liye.
public class User
{
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
}
📌 Agar email format invalid hai toh error ayegi.
---------------------------------------------------
(7) Compare
🔹 2 properties ko compare karne ke liye.

public class User
{
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}
📌 Agar Password aur ConfirmPassword match nahi karte toh error ayegi.
----------------------------------------------------------
(8) CreditCard
🔹 Valid credit card format check karne ke liye.

public class Payment
{
    [CreditCard(ErrorMessage = "Invalid credit card number.")]
    public string CardNumber { get; set; }
}
----------------------------------------------------------
(9) Phone

🔹 Valid phone number format check karne ke liye.

public class User
{
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string PhoneNumber { get; set; }
}
--------------------------------------------------------
(10) Url

🔹 Valid URL format check karne ke liye.

public class Website
{
    [Url(ErrorMessage = "Invalid URL format.")]
    public string WebsiteUrl { get; set; }
}
-------------------------------------------------------------------
2️⃣ Custom Validation Attribute
Agar default validation attributes sufficient nahi hain, toh custom validator bana sakte hain.
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
-----------------------------------

🔹 Use in Model:

public class User
{
    [NoSpecialCharacters]
    public string Username { get; set; }
}
📌 Agar Username me special characters hain toh error ayegi.
--------------------------------------------------------------
3️⃣ How to Validate in Controller?
[HttpPost("register")]
public IActionResult Register([FromBody] User user)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("User registered successfully!");
}
📌 Agar ModelState.IsValid == false hai toh validation fail ho jayegi aur error response milega.
--------------------------------------------
4️⃣ Summary
✅ Required → Field must be filled
✅ StringLength, MinLength, MaxLength → Length validation
✅ Range → Numeric range validate karne ke liye
✅ RegularExpression → Pattern matching ke liye
✅ EmailAddress, Phone, Url → Format check ke liye
✅ Compare → Password matching ke liye
✅ CreditCard → Valid credit card number check ke liye
✅ Custom Validation → Khud ka validator likhne ke liye