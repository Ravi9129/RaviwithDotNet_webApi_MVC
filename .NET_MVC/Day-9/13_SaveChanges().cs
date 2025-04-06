using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 13_SaveChanges()
    {
        
    }
}
---------------------------------------------
.SaveChanges() Entity Framework Core me sabse important method hai — ye tumhare model objects me ki gayi changes ko database me apply karta hai.

🔍 Simple Definition:
.SaveChanges() ka kaam hota hai:

"Context ke andar jo bhi changes (Add, Update, Delete) kiye gaye hain, unko real database me permanently save karna."
------------------------------------------------------------------------
🔧 Real-Life Analogy:
Maan le ek notebook hai jisme tu pencil se likh raha hai (matlab temporarily changes kar raha hai).
Lekin jab tu pen se likh deta hai, toh woh permanent ho jata hai.

.SaveChanges() = Pen se likhna 🔏
Ye tumhare temporary changes ko database me permanently likh deta hai.
----------------------------------------------------------
🔥 Example:
1️⃣ Add New Record

var student = new Student { Name = "Rohit", Age = 22 };
_context.Students.Add(student);      // Temporary memory me add
_context.SaveChanges();              // Ab database me insert hoga ✅
-----------------------------------------------------
2️⃣ Update Existing Record

var student = _context.Students.Find(1);  
student.Age = 25;
_context.SaveChanges();              // Update query chal jayegi DB pe ✅
--------------------------------------------------
3️⃣ Delete Record

var student = _context.Students.Find(1);
_context.Students.Remove(student);
_context.SaveChanges();              // Record delete ho jayega ✅
--------------------------------------------------
❗Important Points:
Ye synchronous method hai – means ye thread ko block karta hai jab tak kaam complete na ho.

Agar koi error aata hai (like null value, constraint break etc.), toh ye exception throw karega.

Transaction ke under bhi use hota hai (multiple queries ek saath commit karne ke liye).
------------------------------------------------------------
🧠 Kab Use Karte Hai?
Jab bhi tum Entity Framework ke through kuch bhi database me change kar rahe ho:
✅ Add
✅ Update
✅ Delete
✅ Cascade changes (like related entities)
-------------------------------------------------------------
🚀 Real Project Scenario:

public IActionResult Create(Student std)
{
    if(ModelState.IsValid)
    {
        _context.Students.Add(std);
        _context.SaveChanges(); // Database me record insert
        return RedirectToAction("Index");
    }
    return View(std);
}