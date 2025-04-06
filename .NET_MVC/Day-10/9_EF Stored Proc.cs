using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 9_EF Stored Proc
    {
        
    }
}
-----------------------------------------
Entity Framework (EF Core) ke saath Stored Procedures ka use kaise karte hain — ye helpful hoti hain jab:

Complex SQL logic ko database mein rakhna ho

Performance optimize karni ho

Legacy DB ke stored procs reuse karne ho
----------------------------------------------------
🔥 Scenario:
Maan lo tumhare SQL Server mein ek stored procedure hai:

CREATE PROCEDURE GetAllProducts
AS
BEGIN
   SELECT * FROM Products
END
---------------------------------------
✅ Step 1: Call Stored Proc Using FromSqlRaw()

var products = _context.Products
    .FromSqlRaw("EXEC GetAllProducts")
    .ToList();
⚠️ Make sure Products entity DbSet ke form mein defined ho.
---------------------------------------------------------
✅ Step 2: Stored Procedure With Parameters

CREATE PROCEDURE GetProductsByCategory
    @CategoryId INT
AS
BEGIN
    SELECT * FROM Products WHERE CategoryId = @CategoryId
END
-----------------------------------
✅ C# Code:

int categoryId = 2;

var products = _context.Products
    .FromSqlRaw("EXEC GetProductsByCategory @CategoryId = {0}", categoryId)
    .ToList();
✅ Step 3: Stored Proc Returning Non-Entity Result
Agar proc ka result kisi existing DbSet se match nahi karta:
-------------------------------------------------
🔧 Create a DTO (ViewModel):

public class ProductSummaryDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
---------------------------------------------------
✅ Call Using Database.SqlQuery (EF Core 8+):

var data = await _context.Database
    .SqlQuery<ProductSummaryDTO>($"EXEC GetProductSummary")
    .ToListAsync();
    ------------------------------------
✅ Step 4: Execute Stored Proc for INSERT/UPDATE/DELETE (No Return)

CREATE PROCEDURE UpdateProductPrice
    @Id INT,
    @NewPrice DECIMAL(10, 2)
AS
BEGIN
    UPDATE Products SET Price = @NewPrice WHERE Id = @Id
END
-----------------------------------------
✅ C# Code:

await _context.Database
    .ExecuteSqlRawAsync("EXEC UpdateProductPrice @Id = {0}, @NewPrice = {1}", 1, 499.99M);
⚠️ Use ExecuteSqlRaw for non-select procedures.
---------------------------------------------------
🧠 Bonus: Use Parameters Safely

var param = new SqlParameter("@CategoryId", 2);

var result = _context.Products
    .FromSqlRaw("EXEC GetProductsByCategory @CategoryId", param)
    .ToList();
    ------------------------------------------
💡 Tip:
Always use parameterized queries to prevent SQL Injection

DTOs help when stored proc result ≠ entity

