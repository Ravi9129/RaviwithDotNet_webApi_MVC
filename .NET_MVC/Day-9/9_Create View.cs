using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 9_Create View
    {
        
    }
}
--------------------------------------------
 Create View samjhte hain — iska kaam hota hai new data ko form ke through user se lena aur database me insert karna. Ye CRUD ka C = Create part hai.

📘 Real-Life Scenario:
Maan le tu ek blog app bana raha hai jisme admin naya blog post add kare. Toh uske liye ek Create View chahiye jisme user ek form fill kare ― title, content, date, etc.

🔧 Steps to Create “Create View” in ASP.NET Core MVC
----------------------------------------------------
🔹 Step 1: Model (Example - BlogPost)

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
--------------------------------------------
🔹 Step 2: Controller - Create (GET)

public IActionResult Create()
{
    return View();
}
--------------------------------------------
🔹 Step 3: Controller - Create (POST)

[HttpPost]
public IActionResult Create(BlogPost post)
{
    if (ModelState.IsValid)
    {
        _context.BlogPosts.Add(post);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(post); // Return view with validation errors if any
}
----------------------------------
🔹 Step 4: Create View (Create.cshtml)

@model BlogPost

<h2>Create New Blog Post</h2>

<form asp-action="Create" method="post">
    <div class="form-group">
        <label asp-for="Title"></label>
        <input asp-for="Title" class="form-control" />
        <span asp-validation-for="Title" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Content"></label>
        <textarea asp-for="Content" class="form-control"></textarea>
        <span asp-validation-for="Content" class="text-danger"></span>
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
----------------------------------
🧠 Samajhne Layak Baatein:
asp-for automatically binds to the model property.

asp-validation-for shows client-side validation errors.

ModelState.IsValid ensures backend validation before saving data.

POST method actually saves the data in database.

_ValidationScriptsPartial enables client-side JS validation.
----------------------------------
🤝 Real-World Touch:
Tu jab koi user registration form, new product form, add contact form banata hai — woh sab Create View se hota hai.

Form banana, user se data lena, validate karna, aur database me insert karna — ye flow sab CRUD apps me hota hi hota hai.

