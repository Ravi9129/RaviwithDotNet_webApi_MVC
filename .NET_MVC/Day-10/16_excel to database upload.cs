using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 16_excel to database upload
    {
        
    }
}
-------------------------------------------------
Excel se data read karke database me insert karna ek dum real-world feature hai — jaise admin panel me bulk uploads. 
Yeh kaam tu ClosedXML ke saath EF Core use karke easily kar sakta hai.

✅ Step-by-Step: Excel to Database Upload
🧩 Step 1: Install Required Packages
---------------------------------------------------
Install-Package ClosedXML
Install-Package Microsoft.EntityFrameworkCore
-------------------------------------
🧾 Step 2: Excel Format Example
Id	Name	Email
1	Aman	aman@test.com
2	Neha	neha@test.com
-------------------------------------------
📦 Step 3: Sample Model

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
--------------------------------------
🏗️ Step 4: DbContext

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
----------------------------------------------------------
📤 Step 5: Upload & Save Logic in Controller

[HttpPost]
public async Task<IActionResult> UploadExcel(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("File not selected");

    using var stream = new MemoryStream();
    await file.CopyToAsync(stream);

    using var workbook = new XLWorkbook(stream);
    var worksheet = workbook.Worksheet(1); // First sheet
    var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header

    var users = new List<User>();

    foreach (var row in rows)
    {
        var user = new User
        {
            Id = Convert.ToInt32(row.Cell(1).Value),
            Name = row.Cell(2).GetString(),
            Email = row.Cell(3).GetString()
        };
        users.Add(user);
    }

    _context.Users.AddRange(users);
    await _context.SaveChangesAsync();

    return Ok("Users imported successfully");
}
------------------------------------------------------------
📄 Razor Page or View HTML

<form asp-action="UploadExcel" enctype="multipart/form-data" method="post">
    <input type="file" name="file" />
    <button type="submit">Upload</button>
</form>
--------------------------------------------------------------
✅ Tips
Tip         	Description
Validate Data	Before inserting, validate fields in Excel
Remove Header Row	Skip first row if it has headers
Use Skip(n) carefully	If Excel format changes, adapt skip logic
Use TryParse	To avoid runtime errors while converting cells