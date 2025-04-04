using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 14_Model Validators
    { 
        
    }
}
---------------------------------------
Model Validators in ASP.NET Core 🚀
1️⃣ Model Validation Kya Hai?
🔹 Model Validation ka matlab hai data ko validate karna before processing.
🔹 Ye client-side aur server-side validation dono ko support karta hai.
🔹 Data Annotations (Attributes) ka use karke validation lagayi ja sakti hai.
-------------------------------------------------------
2️⃣ Basic Model Validator Example
Agar ek Product model hai toh usme validation lagane ke liye Data Annotations ka use kar sakte hain.
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product Name is required.")]
    [StringLength(100, ErrorMessage = "Product Name cannot be more than 100 characters.")]
    public string Name { get; set; }

    [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; }
}
📌 Yahaan Required, StringLength, aur Range attributes ka use karke validation apply ki gayi hai.
----------------------------------------------------------------
3️⃣ Common Model Validation Attributes
ASP.NET Core me built-in validation attributes available hain jo model properties par apply kiye ja sakte hain.
----------------------------------------------
✅ 1. [Required] – Field Ko Mandatory Banata Hai
[Required(ErrorMessage = "Name is required.")]
public string Name { get; set; }
✔ Agar user Name field blank chhodega toh error show hoga.
------------------------------------------------------------------
✅ 2. [StringLength] – Character Limit Set Karta Hai
[StringLength(50, ErrorMessage = "Name cannot be more than 50 characters.")]
public string Name { get; set; }
✔ Agar user 50 se zyada characters enter karega toh error aayega.
---------------------------------------------
✅ 3. [Range] – Min aur Max Value Set Karta Hai
[Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
public decimal Price { get; set; }
✔ Agar user invalid range ka value enter karega toh error milega.
---------------------------------------------------
✅ 4. [EmailAddress] – Valid Email Format Check Karta Hai
[EmailAddress(ErrorMessage = "Invalid Email Address.")]
public string Email { get; set; }
✔ Invalid email format par error show karega.
---------------------------------------------------------
✅ 5. [Phone] – Valid Phone Number Check Karta Hai
[Phone(ErrorMessage = "Invalid phone number.")]
public string PhoneNumber { get; set; }
✔ Sirf valid phone number accept karega.
-----------------------------------------------
✅ 6. [RegularExpression] – Custom Pattern Validation

[RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Name must start with a capital letter.")]
public string Name { get; set; }
✔ Yahaan validation lagayi gayi hai ki Name ka pehla letter capital hona chahiye.
------------------------------------------------------
4️⃣ Model Validation in Controller
Agar model validation fail hoti hai toh ModelState.IsValid false return karega.
[HttpPost("add-product")]
public IActionResult AddProduct([FromBody] Product product)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    return Ok("Product added successfully!");
}
📌 Agar koi validation fail hoti hai toh 400 Bad Request return hoga.
------------------------------------------
5️⃣ Custom Validation Attribute (Advanced)
Agar built-in attributes se kaam na chale toh Custom Validator bana sakte hain.
public class MinimumYearAttribute : ValidationAttribute
{
    private readonly int _minYear;
    
    public MinimumYearAttribute(int minYear)
    {
        _minYear = minYear;
    }

    public override bool IsValid(object value)
    {
        if (value is int year)
        {
            return year >= _minYear;
        }
        return false;
    }
}
------------------------------------
Isko Model me Use Karna:
public class Product
{
    [MinimumYear(2000, ErrorMessage = "Manufacturing year must be 2000 or later.")]
    public int ManufacturingYear { get; set; }
}
📌 Ab agar user 1999 enter karega toh validation fail ho jayegi.
---------------------------------------------------
6️⃣ Model Validation Kaise Kaam Karta Hai?
✔ Step 1: Model validation Data Annotations ke through check hoti hai.
✔ Step 2: Controller me ModelState.IsValid check hota hai.
✔ Step 3: Agar valid hai toh request process hoti hai, warna error return hota hai.
-----------------------------------------------
7️⃣ Conclusion
✅ Model validation data consistency aur security ke liye important hai.
✅ Built-in attributes jaise [Required], [Range], [StringLength] ka use kar sakte hain.
✅ Agar complex validation chahiye toh custom validator bana sakte hain.
✅ ModelState se server-side validation check kar sakte hain.