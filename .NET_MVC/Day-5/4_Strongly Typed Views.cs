using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 4_Strongly Typed Views
    {
        
    }
}
--------------------------------
🔹 Strongly Typed Views in ASP.NET Core MVC
📌 Strongly Typed View kya hoti hai?
Strongly Typed View wo view hoti hai jo directly ek specific C# model class se bind hoti hai.

Iska matlab:

View ke top pe ek model define hota hai.

Aur fir hum model ke properties ko @Model.PropertyName ke through access karte hain.

Ye approach compile-time checking deta hai (agar koi galti hui toh compile time par error milta hai).
---------------------------------------------------
📌 Real-World Example: Student Form & Details Display
------------------------------
🔶 1️⃣ Model Class
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Marks { get; set; }
}
-------------------------------------------
🔶 2️⃣ Controller Code
public class StudentController : Controller
{
    public IActionResult Details()
    {
        var student = new Student
        {
            Id = 1,
            Name = "Rahul Sharma",
            Marks = 95
        };

        return View(student); // Model bhej rahe hain View me
    }
}
---------------------------------------
🔶 3️⃣ Strongly Typed View (Details.cshtml)
@model YourNamespace.Models.Student

<h2>Student Details</h2>
<p><strong>ID:</strong> @Model.Id</p>
<p><strong>Name:</strong> @Model.Name</p>
<p><strong>Marks:</strong> @Model.Marks</p>
⚠️ Note: Top pe @model likhna mandatory hai. Ye bataata hai ki kis type ka model view ke sath bind hoga.
-------------------------------------------------
📌 Benefits of Strongly Typed Views
🔹 IntelliSense support milta hai Visual Studio me.
🔹 Compile-time error checking hota hai.
🔹 Large applications me error-free code likhne me help milti hai.
🔹 Data access clean aur readable hota hai.

📌 Kab use kare?
✅ Jab aapko controller se ek defined model class ka object view me bhejna ho.
✅ Jab form ke zariye model ki values bind karni ho (Create/Edit form).
✅ Jab aapko maintainable, reusable code likhna ho.
------------------------------
📌 Kab avoid kare?
❌ Jab data bahut chhota ya dynamic ho, aur sirf ek string/text message bhejna ho (e.g. ViewBag/ViewData se).
❌ Jab view me data strongly defined class se bind nahi hota (e.g. loosely typed dashboard data).
------------------------------------------------------
✅ Conclusion
Strongly typed view:

Clean and maintainable hoti hai.

Saath hi development me fast aur error-free kaam karne me madad karti hai.

MVC architecture ka best practice hai.

