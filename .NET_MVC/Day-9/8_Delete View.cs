using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 8_Delete View
    {
        
    }
}
---------------------------------------
 Delete View ke baare mein ― iska kaam hota hai kisi existing data ko confirm karke delete karna. Ye bhi CRUD ka ek basic part hai: D = Delete

🔧 Real-life Scenario:
Soch le ek E-Commerce Admin Panel hai jahan tu product list manage karta hai. Kisi product ko permanently delete karne se pehle, tu user se confirmation chahta hai ― "Are you sure you want to delete?". Ye kaam Delete View karega.
---------------------------------------------------------
⚙️ Setup: Kaise banta hai Delete View
Step 1: Model (Example)

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
------------------------------------------
Step 2: Controller - Delete (GET)

public IActionResult Delete(int id)
{
    var product = _context.Products.Find(id);
    if (product == null)
        return NotFound();

    return View(product); // Pass product to view for confirmation
}
------------------------------------------------------------
Step 3: Controller - DeleteConfirmed (POST)
[HttpPost, ActionName("Delete")]
public IActionResult DeleteConfirmed(int id)
{
    var product = _context.Products.Find(id);
    if (product != null)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}
----------------------------------------
Step 4: Delete View (Delete.cshtml)

@model Product

<h2>Are you sure you want to delete this product?</h2>

<div>
    <h4>@Model.Name</h4>
    <p>Price: @Model.Price</p>
</div>

<form asp-action="Delete" method="post">
    <input type="hidden" asp-for="Id" />
    <button type="submit" class="btn btn-danger">Yes, Delete</button>
    <a asp-action="Index" class="btn btn-secondary">Cancel</a>
</form>
-------------------------------------------------
🔁 Flow Kaise Chalta Hai:
User Delete button click karta hai.

GET Delete(int id) action chalta hai, user ko confirmation dikhayi jaati hai.

User confirm karta hai → POST DeleteConfirmed action chalta hai.

Data database se delete ho jaata hai.
-----------------------------------------------------
✅ Important Points:
ActionName("Delete") use hota hai so that GET aur POST dono same route pe ho sakein.

Confirmation dikhana zaroori hota hai real projects me (especially production apps me).

RedirectToAction("Index") delete ke baad list page pe wapas le jaata hai.

asp-action="Delete" aur POST method ensure karta hai safe delete.

