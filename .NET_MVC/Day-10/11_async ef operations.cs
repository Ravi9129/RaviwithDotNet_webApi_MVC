using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 11_async ef operations
    {
        
    }
}
-----------------------------------------------
Entity Framework Core ke Async Operations ke baare mein — real projects me async kaafi important hota hai performance aur scalability ke liye. Main tujhe most-used EF Core Async methods ke examples ke saath samjhaata hoon. 👇
---------------------------------
🔄 1. ToListAsync()
Sabse commonly used method — list me data fetch karne ke liye.

var products = await _context.Products.ToListAsync();
---------------------------------------------------
🔎 2. FirstOrDefaultAsync()
First item ya null return karega (agar match na ho):

var product = await _context.Products
    .FirstOrDefaultAsync(p => p.Id == 1);
    -----------------------------------------------------
🔍 3. SingleOrDefaultAsync()
Sirf ek record hona chahiye — ya null milega. Agar zyada mile toh exception throw karega.


var user = await _context.Users
    .SingleOrDefaultAsync(u => u.Email == "test@example.com");
    -------------------------------------------------
🔢 4. CountAsync()
Total count leke aata hai:


int count = await _context.Products.CountAsync();
--------------------------------------------
✔️ 5. AnyAsync()
Kya koi record exist karta hai?


bool exists = await _context.Users.AnyAsync(u => u.Username == "admin");
---------------------------------------------------------
➕ 6. AddAsync()
Naya record insert karne ke liye:


var product = new Product { Name = "New Phone", Price = 50000 };
await _context.Products.AddAsync(product);
await _context.SaveChangesAsync();
------------------------------------------------------
➕ 7. AddRangeAsync()
Multiple records ek saath add karne ke liye:

var list = new List<Product> {
    new Product { Name = "A", Price = 100 },
    new Product { Name = "B", Price = 200 }
};
await _context.Products.AddRangeAsync(list);
await _context.SaveChangesAsync();
----------------------------------------------------
🗑️ 8. Delete (Async tarike se)
Remove async nahi hota, lekin SaveChangesAsync() use hota hai:


var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == 1);
if (product != null)
{
    _context.Products.Remove(product);
    await _context.SaveChangesAsync();
}
-------------------------------------------
✏️ 9. Update
Update bhi async me hota hai jab pehle fetch kar lo:


var product = await _context.Products.FindAsync(1);
product.Price = 999;
await _context.SaveChangesAsync();
------------------------------------------
📌 10. FindAsync()
Id ke base pe record fetch karna:

var product = await _context.Products.FindAsync(1);
--------------------------------------
🔧 11. SaveChangesAsync()
----------------------------------
EF ke jitne bhi changes hain — Add, Update, Delete — unko database me save karta hai:

await _context.SaveChangesAsync();
-----------------------------------------------
💡 Pro Tips:
Async methods use karo jab bhi I/O operation ho (like DB calls)

Controller methods me async Task<IActionResult> return karo

Avoid async void, unless you're writing event handlers