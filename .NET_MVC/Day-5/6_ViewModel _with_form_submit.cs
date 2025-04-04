using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-5
{
    public class 6_ViewModel _with_form_submit
    {
        
    }
}
------------------------------------
ViewModel ke sath Form Submit ka concept real-world me Create/Edit form me bahut use hota hai. Main tujhe ekdum real-life scenario se samjhata hoon step-by-step.

🔥 Scenario: Ek Student ko Course assign karna hai (Form ke through)
Hum use karenge:

✅ Strongly Typed ViewModel

✅ POST method

✅ Model validation
---------------------------------------
🧱 1️⃣ Models

🔸 Student.cs

public class Student
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
----------------------------------------
🔸 Course.cs

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
}
------------------------------------------------
🧱 2️⃣ ViewModel: StudentCourseViewModel.cs

public class StudentCourseViewModel
{
    public Student Student { get; set; }

    [Required(ErrorMessage = "Course is required")]
    public int SelectedCourseId { get; set; }

    public List<Course> Courses { get; set; }
}
---------------------------------------------
🧱 3️⃣ Controller: StudentController.cs

public class StudentController : Controller
{
    public IActionResult Create()
    {
        var viewModel = new StudentCourseViewModel
        {
            Student = new Student(),
            Courses = GetAllCourses() // Suppose ye method course list laata hai
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Create(StudentCourseViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // ✅ Data save karna yaha hota
            // Student info: viewModel.Student
            // Course Id: viewModel.SelectedCourseId

            return RedirectToAction("Success");
        }

        // Agar model invalid hai to course list dubara set karni padegi
        viewModel.Courses = GetAllCourses();
        return View(viewModel);
    }

    private List<Course> GetAllCourses()
    {
        return new List<Course>
        {
            new Course{ Id = 1, Title = "ASP.NET Core" },
            new Course{ Id = 2, Title = "Angular" },
            new Course{ Id = 3, Title = "React" }
        };
    }

    public IActionResult Success()
    {
        return View();
    }
}
--------------------------------------------
🧱 4️⃣ View: Create.cshtml (Strongly Typed ViewModel)

@model YourNamespace.Models.StudentCourseViewModel

<h2>Assign Course to Student</h2>

<form asp-action="Create" method="post">
    <div>
        <label>Name:</label>
        <input asp-for="Student.Name" />
        <span asp-validation-for="Student.Name" class="text-danger"></span>
    </div>

    <div>
        <label>Course:</label>
        <select asp-for="SelectedCourseId" asp-items="@(new SelectList(Model.Courses, "Id", "Title"))">
            <option value="">-- Select Course --</option>
        </select>
        <span asp-validation-for="SelectedCourseId" class="text-danger"></span>
    </div>

    <button type="submit">Submit</button>
</form>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
✅ Output Flow:
GET /Student/Create → empty form open hoti hai.

User form fill karta hai.

POST /Student/Create → ViewModel data controller me aata hai.

Agar valid hai to success page dikhaata hai, nahi to wahi form with errors.
-----------------------------------------------------
📌 Real-World Use Cases:
Registration form jisme dropdown ho (Gender, Department, Role)

Admin panel forms (Add Product with Category)

User profile update with dynamic list