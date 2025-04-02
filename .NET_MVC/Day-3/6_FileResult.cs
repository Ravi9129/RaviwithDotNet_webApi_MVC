using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-3
{
    public class 6_FileResult
    {
        
    }
}
-----------------------------------------
FileResult in ASP.NET Core 🚀
1️⃣ FileResult Kya Hai?
🔹 FileResult ek built-in action result hai jo file content HTTP response me return karne ke liye use hota hai.
🔹 Iska use files download karne, images serve karne, ya dynamically generated files provide karne ke liye hota hai.
🔹 Yeh IActionResult inherit karta hai aur different types of file responses handle kar sakta hai.
----------------------------------------------------------- 
2️⃣ FileResult Ka Basic Syntax
public FileResult DownloadFile()
{
    byte[] fileBytes = System.IO.File.ReadAllBytes("wwwroot/files/sample.pdf");
    return File(fileBytes, "application/pdf", "downloaded_sample.pdf");
}
📌 Request: GET /api/download
📌 Response: PDF file download hogi "downloaded_sample.pdf" naam se.

3️⃣ FileResult Kab Use Karein?
🔹 Jab client ko file download karani ho (PDF, CSV, DOCX, etc.).
🔹 Jab server-side generated files serve karni ho (Excel reports, invoices, etc.).
🔹 Jab images ya static assets dynamically provide karne ho.
--------------------------------------------
4️⃣ FileResult Ke Example
✅ 1. Static File Download (Local File System se)
[HttpGet("download")]
public FileResult DownloadFile()
{
    string filePath = "wwwroot/files/sample.txt";
    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
    return File(fileBytes, "text/plain", "sample.txt");
}
📌 Response: "sample.txt" file client ke browser me download hogi.
--------------------------------------------------------
✅ 2. File Stream Return Karna (Large Files ke liye Best Practice)
[HttpGet("download-pdf")]
public FileResult DownloadPdf()
{
    var filePath = "wwwroot/files/document.pdf";
    var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    return File(stream, "application/pdf", "document.pdf");
}
📌 Response: PDF file streaming ke through serve hogi, jo memory-efficient hota hai.
------------------------------------------
✅ 3. File from Byte Array
[HttpGet("download-image")]
public FileResult DownloadImage()
{
    byte[] imageBytes = System.IO.File.ReadAllBytes("wwwroot/images/pic.jpg");
    return File(imageBytes, "image/jpeg", "downloaded_pic.jpg");
}
📌 Response: "downloaded_pic.jpg" file browser me download hogi.
--------------------------------------------------------------------
✅ 4. FileStreamResult ka Use (Streaming ke liye Best)
[HttpGet("stream-video")]
public FileStreamResult StreamVideo()
{
    var stream = new FileStream("wwwroot/videos/sample.mp4", FileMode.Open, FileAccess.Read);
    return new FileStreamResult(stream, "video/mp4");
}
📌 Response: Video stream hogi instead of download.
---------------------------------------------------------------
✅ 5. PhysicalFileResult ka Use (Direct Path se Serve Karna)
[HttpGet("get-report")]
public PhysicalFileResult GetReport()
{
    return PhysicalFile("C:\\Reports\\monthly_report.pdf", "application/pdf");
}
📌 Response: Directly file serve karega bina manually FileStream create kiye.
----------------------------------------------------------------------------------
5️⃣ FileResult Ke Different Types
Type	              Use Case
FileContentResult    	Byte array se file return karne ke liye
FileStreamResult	    Large files ko stream karne ke liye
PhysicalFileResult	    Direct file path se serve karne ke liye
VirtualFileResult	    ASP.NET Core ke virtual file system se file serve karne ke liye
---------------------------------------
6️⃣ Real-World Scenario Example
🔹 Report Download API jo Excel File return kare:

[HttpGet("export-excel")]
public FileResult ExportExcel()
{
    var filePath = "wwwroot/reports/sales.xlsx";
    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "sales_report.xlsx");
}
📌 Use Case: Sales Report generate karna aur client ko download option dena.
------------------------------------------------
7️⃣ Conclusion
✔ FileResult tab use hota hai jab server se koi file return karni ho (download, streaming, ya serving).
✔ Different implementations (FileContentResult, FileStreamResult, PhysicalFileResult) specific needs ke liye hote hain.
✔ Streaming (FileStreamResult) prefer karna chahiye large files ke liye taaki memory optimized rahe.