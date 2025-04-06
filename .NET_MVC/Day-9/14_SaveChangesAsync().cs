using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 14_SaveChangesAsync()
    {
        
    }
}
-----------------------------------------------------------------
.SaveChangesAsync() — ye .SaveChanges() ka asynchronous version hai, jo background me bina thread block kiye database me changes save karta hai.

🔍 Simple Definition:
.SaveChangesAsync() Entity Framework Core me use hota hai jab tum async programming kar rahe ho — ye database operations ko non-blocking way me perform karta hai.
---------------------------------------------
🔧 Real-life Comparison:
Soch, tu restaurant me order deta hai aur wait karta hai (SaveChanges())
vs
Tu order deta hai aur meanwhile phone check karta hai jab tak khana aa jaye (SaveChangesAsync())

🟢 SaveChangesAsync = Background me kaam karo, UI ya thread free rakho.

✅ Syntax:

await _context.SaveChangesAsync();
🧠 Yaad rakhna: await lagana mandatory hai, warna async hone ka koi faida nahi.
----------------------------------------
🧪 Example:

public async Task<IActionResult> Create(Student std)
{
    if(ModelState.IsValid)
    {
        _context.Students.Add(std);
        await _context.SaveChangesAsync();   // 🔄 Non-blocking DB save
        return RedirectToAction("Index");
    }
    return View(std);
}

-----------------------------------------------------------------
🔥 Real Use-case Scenario:
Tumhari app me agar multiple users ek saath kaam karte hain (web app, mobile app etc.), toh .SaveChangesAsync() ka use karo.

public async Task<IActionResult> Update(int id, Student updated)
{
    var student = await _context.Students.FindAsync(id);
    if(student != null)
    {
        student.Name = updated.Name;
        student.Age = updated.Age;

        await _context.SaveChangesAsync();  // ✅ Non-blocking update
        return RedirectToAction("Index");
    }

    return NotFound();
}
----------------------------------------------------------
🔐 Exception Handling (Best Practice):

try
{
    await _context.SaveChangesAsync();
}
catch (DbUpdateException ex)
{
    // Handle db-level errors (e.g. constraint issues)
}
catch (Exception ex)
{
    // Log or handle unexpected issues
}
--------------------------------------------------------
🧠 Jab Use Karna Chahiye?
Jab tum ASP.NET Core ya Blazor me ho

Jab multiple users same time pe access karte ho

Jab tum UI responsiveness maintain rakhna chahte ho