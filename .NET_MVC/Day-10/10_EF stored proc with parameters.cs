using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 10_EF stored proc with parameters
    {
        
    }
}
-------------------------------------
Entity Framework (EF Core) me Stored Procedure with Parameters ko use karna samjhte hain step-by-step 👇
--------------------------------
🎯 Scenario
SQL Server me ek stored procedure hai jo category ID ke base pe products return karta hai:

CREATE PROCEDURE GetProductsByCategory
    @CategoryId INT
AS
BEGIN
    SELECT * FROM Products WHERE CategoryId = @CategoryId
END
-------------------------------------------------
✅ Step 1: DbContext me DbSet hona chahiye

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}
-----------------------------------------
Aur Product class kuch aise ho:

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
------------------------------------
✅ Step 2: Call Stored Proc With Parameter
👇 Using FromSqlRaw():

int categoryId = 2;

var products = _context.Products
    .FromSqlRaw("EXEC GetProductsByCategory @CategoryId = {0}", categoryId)
    .ToList();
{0} placeholder ke through SQL injection se bach jaate ho
----------------------------------------------
✅ Alternate: Using SqlParameter

var categoryParam = new SqlParameter("@CategoryId", 2);

var products = _context.Products
    .FromSqlRaw("EXEC GetProductsByCategory @CategoryId", categoryParam)
    .ToList();
    -----------------------------------
✅ Stored Proc With Multiple Params
👇 SQL:

CREATE PROCEDURE GetProductsByCategoryAndPrice
    @CategoryId INT,
    @MinPrice DECIMAL
AS
BEGIN
    SELECT * FROM Products
    WHERE CategoryId = @CategoryId AND Price >= @MinPrice
END
------------------------------------------------
👇 C# Code:

var products = _context.Products
    .FromSqlRaw("EXEC GetProductsByCategoryAndPrice @CategoryId = {0}, @MinPrice = {1}", 2, 500)
    .ToList();
    ----------------------------------------------
🔄 Async Version

var products = await _context.Products
    .FromSqlRaw("EXEC GetProductsByCategory @CategoryId = {0}", 2)
    .ToListAsync();
    -------------------------------------------------------
💡 Tips:
Always validate and sanitize input if it's dynamic

Always use {0} style or SqlParameter to avoid SQL Injection

FromSqlRaw() returns entity type (DbSet<Product>), use DTOs for custom result sets

