using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 5_strongly typed view me multiple models
    {
        
    }
}
---------------------------------------------
strongly typed view me multiple models ek tricky concept hai, lekin real-world projects me bahut kaam aata hai — jaise jab form me 2 alag data sets dikhane ho ya ek page pe multiple sections ho.

ASP.NET Core MVC me View ek hi strongly typed model accept karta hai, to multiple models bhejne ke liye kuch common solutions use kiye jaate hain:

✅ 3 Best Approaches for Strongly Typed Views with Multiple Models
🔹 Approach 1: Create a ViewModel (Recommended)
Multiple model classes ko ek nayi class me wrap kar do — use hi ViewModel kehte hain.
------------------------------------
🧱 Example Scenario: Student Info + Course Info
1️⃣ Student.cs
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}
------------------------------------
2️⃣ Course.cs
public class Course
{
    public string Title { get; set; }
    public int Credits { get; set; }
}
-----------------------------------------------
3️⃣ StudentCourseViewModel.cs (ViewModel)

public class StudentCourseViewModel
{
    public Student Student { get; set; }
    public Course Course { get; set; }
}
---------------------------------------------------
4️⃣ Controller
public IActionResult Details()
{
    var viewModel = new StudentCourseViewModel
    {
        Student = new Student { Id = 1, Name = "Ankit" },
        Course = new Course { Title = "ASP.NET Core", Credits = 4 }
    };

    return View(viewModel);
}
-----------------------------------------------
5️⃣ View (Details.cshtml)
@model YourNamespace.Models.StudentCourseViewModel

<h2>Student Info</h2>
<p>ID: @Model.Student.Id</p>
<p>Name: @Model.Student.Name</p>

<h2>Course Info</h2>
<p>Title: @Model.Course.Title</p>
<p>Credits: @Model.Course.Credits</p>
✅ Ab ek hi view me multiple model objects ka data access ho gaya!
--------------------------------------------------------
🔹 Approach 2: Tuple ka Use
Agar chhoti requirement ho, toh Tuple<Student, Course> ka use kar sakte ho.

public IActionResult Details()
{
    var student = new Student { Id = 1, Name = "Ankit" };
    var course = new Course { Title = "Angular", Credits = 5 };

    var tupleData = Tuple.Create(student, course);
    return View(tupleData);
}
---------------------------
@model Tuple<Student, Course>

<h2>@Model.Item1.Name</h2>
<h2>@Model.Item2.Title</h2>
⚠️ Downside: Ye thoda non-readable hota hai (Item1, Item2), isliye production me avoid karte hain.
--------------------------------------------
🔹 Approach 3: ViewBag ya ViewData Se Multiple Model Pass Karna
Agar ek model strongly typed pass kar diya, aur doosra chhoti cheez hai, toh use ViewBag me bhej do.


public IActionResult Details()
{
    var student = new Student { Id = 2, Name = "Neha" };
    ViewBag.CourseTitle = "Blazor";

    return View(student);
}
--------------------------
@model Student

<p>Student: @Model.Name</p>
<p>Course: @ViewBag.CourseTitle</p>
⚠️ Type safety nahi milegi, isliye temporary cases ke liye theek hai.
-----------------------------------------------
✅ Summary – Best Practice
Method	Use When
✅ ViewModel	Most professional & clean solution
🔄 Tuple	Short/quick code with 2 objects only
🔸 ViewBag/ViewData	One object is optional or secondary
