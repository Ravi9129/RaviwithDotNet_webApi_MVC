using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 14_generate csv files
    {
        
    }
}
-----------------------------------------
CSV file generate karna .NET me bilkul asaan hai. Tu Excel-style data export karna chahta hai? Bas plain text format me comma-separated rows likhni hoti hain.

✅ Basic CSV File Generation in .NET
Example: Manual CSV Generation
----------------------------------------------------
public void GenerateCsvFile()
{
    var lines = new List<string>
    {
        "Id,Name,Email", // Header row
        "1,Aman,aman@example.com",
        "2,Neha,neha@example.com",
        "3,Raj,raj@example.com"
    };

    File.WriteAllLines("users.csv", lines);
}
----------------------------
✅ With StringBuilder for Dynamic Data

public void GenerateCsvWithBuilder(List<User> users)
{
    var sb = new StringBuilder();
    sb.AppendLine("Id,Name,Email"); // Header

    foreach (var user in users)
    {
        sb.AppendLine($"{user.Id},{user.Name},{user.Email}");
    }

    File.WriteAllText("users.csv", sb.ToString());
}
-----------------------
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
---------------------------------
✅ CSV File Download from ASP.NET Core Controller

[HttpGet]
public IActionResult DownloadCsv()
{
    var sb = new StringBuilder();
    sb.AppendLine("Id,Name,Email");
    sb.AppendLine("1,Aman,aman@example.com");
    sb.AppendLine("2,Neha,neha@example.com");

    var bytes = Encoding.UTF8.GetBytes(sb.ToString());
    return File(bytes, "text/csv", "users.csv");
}
-----------------------------------
✅ Using CSVHelper Library (for Complex Models)

--------------------------------------------------
Install-Package CsvHelper

using CsvHelper;
using System.Globalization;

public void GenerateCsvWithCsvHelper(List<User> users)
{
    using var writer = new StreamWriter("users.csv");
    using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
    csv.WriteRecords(users);
}
-----------------------------------------------
🔥 Use Cases
Export user data to Excel

Download reports

Product inventory export

Logs export

Analytics tools

