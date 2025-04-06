using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 8_ EF (Entity Framework) Queries
    {
        
    }
}
---------------------------------------------
EF (Entity Framework) Queries ki — real project mein data ko database se retrieve, filter, sort, join karne ke liye ye sabse powerful feature hota hai.

🔥 Real-Life Scenario:
Maan lo ek eCommerce app hai jisme:

Products table hai (Id, Name, Price, CategoryId)

Categories table hai (Id, Name)

Ab tujhe chahiye:

Saare products

Filtered by category

Sorted by price

Include category name
--------------------------------------------------
✅ Basic Select Query

var products = _context.Products.ToList();
Yeh sabhi products dega, bina kisi filter ke.
--------------------------------------
✅ Where Clause (Filter)

var cheapProducts = _context.Products
    .Where(p => p.Price < 500)
    .ToList();
Sirf 500 se kam price wale products dega.
-----------------------------------------------------
✅ OrderBy (Sorting)

var sortedProducts = _context.Products
    .OrderBy(p => p.Price)
    .ToList();
Price ke ascending order mein.
----------------------------------------------------------
✅ Include (Joins / Related Data)

var productsWithCategory = _context.Products
    .Include(p => p.Category)
    .ToList();
Yeh har product ke saath uska category bhi laata hai.
------------------------------------------------------
✅ Join with Filter and Projection (Select)

var result = _context.Products
    .Where(p => p.Category.Name == "Electronics")
    .Select(p => new {
        p.Name,
        p.Price,
        Category = p.Category.Name
    })
    .ToList();
Sirf Electronics category wale product ka naam, price aur category laata hai.
-----------------------------------------------------------
✅ FirstOrDefault (Single item fetch)

var product = _context.Products
    .FirstOrDefault(p => p.Id == 1);
Id = 1 ka product dega ya null agar na mila.
--------------------------------------------------
✅ Count

var count = _context.Products
    .Count(p => p.Price > 1000);
Kitne products 1000 se zyada price ke hain.
-----------------------------------------------
✅ Any (Boolean check)

bool exists = _context.Products
    .Any(p => p.Name == "iPhone 15");
True ya False return karega.
--------------------------------------------------------
✅ GroupBy (Aggregate)

var grouped = _context.Products
    .GroupBy(p => p.Category.Name)
    .Select(g => new {
        Category = g.Key,
        Count = g.Count()
    })
    .ToList();
Har category mein kitne product hain.
------------------------------------------------
✅ Complex: Multiple Where + Include + OrderBy

var finalList = _context.Products
    .Where(p => p.Price > 100 && p.Category.Name == "Books")
    .Include(p => p.Category)
    .OrderByDescending(p => p.Price)
    .ToList();
    ----------------------------------------------
✅ Note:
Queries run only when you call .ToList(), .First(), .Count() → called deferred execution

Always use Include() to avoid lazy loading issues

