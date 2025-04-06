using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 7_Edit View 
    {
        
    }
}
--------------------------------
Edit View ke baare mein ― ASP.NET Core MVC me jab tu kisi existing data ko update karna chahta hai, tab Edit View ka use hota hai. Ye mostly CRUD operations ka part hota hai: Create, Read, Update, Delete.

🔧 Real-life Scenario:
Tu ek blog bana raha hai, jisme user ne ek article publish kiya. Ab user ko article ke title ya content ko edit karna hai. Uske liye tu Edit action aur Edit view create karega.
-----------------------------------------
⚙️ Setup: Kaise banta hai Edit View
Step 1: Model (Example)

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
-----------------------------------------
Step 2: Controller - Edit Action (GET)

public IActionResult Edit(int id)
{
    var article = _context.Articles.Find(id);
    if (article == null)
        return NotFound();

    return View(article); // Pass model to view
}
--------------------------------------------
Step 3: Controller - Edit Action (POST)

[HttpPost]
public IActionResult Edit(Article model)
{
    if (ModelState.IsValid)
    {
        _context.Articles.Update(model);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(model);
}
-------------------------------------------
Step 4: Edit View (Edit.cshtml)

@model Article

<h2>Edit Article</h2>

<form asp-action="Edit">
    <input type="hidden" asp-for="Id" />

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

    <button type="submit" class="btn btn-primary">Update</button>
</form>
------------------------------------------------
🔁 Flow Kaise Chalta Hai:
User Edit button click karta hai.

GET Edit(int id) action chalta hai aur existing data load hota hai.

Form me user data edit karta hai.

Form submit hota hai → POST Edit(Article model) action call hota hai.

Data database me update hota hai.
---------------------------------------------
✅ Important Points:
asp-for automatically model properties bind karta hai.

Hidden field me Id zaroor bhejna padta hai warna update nahi hoga.

ModelState.IsValid validation ke liye check karta hai.

View strongly-typed hoti hai — @model Article