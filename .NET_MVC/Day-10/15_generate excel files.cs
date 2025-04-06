using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 15_generate excel files
    {
        
    }
}
------------------------------------------
Excel file generate karna .NET me aur bhi powerful hai — especially jab tu ClosedXML ya EPPlus jaise libraries use karta hai. Yeh libraries bina Excel install kiye .xlsx files bana sakti hain.

✅ Step-by-Step: Excel File Generation using ClosedXML
📦 Step 1: Install Package
---------------------------
Install-Package ClosedXML
-----------------------------------------
👨‍💻 Step 2: Code to Generate Excel
using ClosedXML.Excel;

public void GenerateExcelFile()
{
    var workbook = new XLWorkbook();
    var worksheet = workbook.Worksheets.Add("Users");

    // Header
    worksheet.Cell(1, 1).Value = "Id";
    worksheet.Cell(1, 2).Value = "Name";
    worksheet.Cell(1, 3).Value = "Email";

    // Data
    worksheet.Cell(2, 1).Value = 1;
    worksheet.Cell(2, 2).Value = "Aman";
    worksheet.Cell(2, 3).Value = "aman@example.com";

    worksheet.Cell(3, 1).Value = 2;
    worksheet.Cell(3, 2).Value = "Neha";
    worksheet.Cell(3, 3).Value = "neha@example.com";

    workbook.SaveAs("Users.xlsx");
}
-----------------------------------------------------
✅ Excel Download in ASP.NET Core Controller

[HttpGet]
public IActionResult DownloadExcel()
{
    using var workbook = new XLWorkbook();
    var worksheet = workbook.Worksheets.Add("Report");
    worksheet.Cell(1, 1).Value = "Id";
    worksheet.Cell(1, 2).Value = "Name";

    worksheet.Cell(2, 1).Value = 1;
    worksheet.Cell(2, 2).Value = "Aman";

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    stream.Seek(0, SeekOrigin.Begin);

    return File(stream.ToArray(), 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                "report.xlsx");
}
-------------------------------------------------
🔥 Real-World Use Cases
Export customer data

Generate invoices

Export sales reports

Timesheets

Employee payroll sheets
---------------------------------------------------
🧠 BONUS: Dynamic Data Example

public void ExportDynamic(List<User> users)
{
    using var workbook = new XLWorkbook();
    var ws = workbook.Worksheets.Add("Users");

    ws.Cell(1, 1).Value = "Id";
    ws.Cell(1, 2).Value = "Name";
    ws.Cell(1, 3).Value = "Email";

    for (int i = 0; i < users.Count; i++)
    {
        ws.Cell(i + 2, 1).Value = users[i].Id;
        ws.Cell(i + 2, 2).Value = users[i].Name;
        ws.Cell(i + 2, 3).Value = users[i].Email;
    }

    workbook.SaveAs("DynamicUsers.xlsx");
}