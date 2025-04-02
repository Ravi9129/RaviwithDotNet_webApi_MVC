using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 2_.NET Core
    {
        
    }
}
-----------------------------------------------
.NET Core Kya Hai?
.NET Core ek modern, cross-platform, open-source, aur high-performance framework hai jo Microsoft ne .NET Framework ke alternative ke roop me banaya hai. Iska main purpose fast, scalable, aur cloud-ready applications develop karna hai.

.NET Core ko ab officially .NET 5+ ke baad se sirf ".NET" bola jata hai, par log ab bhi "NET Core" naam use karte hain jab .NET Framework se difference samjhna hota hai.

.NET Core vs .NET Framework
Feature	.NET Core	.NET Framework
Platform	Cross-platform (Windows, Linux, macOS)	Sirf Windows
Performance	High-performance	Moderate performance
Microservices Support	Best support for Microservices	Limited support
Cloud-Ready	Fully cloud-optimized	Cloud support limited
Open-Source	Yes	No (partial open-source)
Future Support	Yes (Active development)	No (Only maintenance updates)
Agar new project start kar rahe ho, toh .NET Core ya .NET 6+ (latest version) best option hai.

Real-World Example & Usage Scenarios
Ab dekhtay hain .NET Core real-world applications me kab, kahan, aur kyun use hota hai.

1. Web Application - ASP.NET Core
Agar aapko fast aur scalable web application banani ho, toh ASP.NET Core MVC best choice hai.

Scenario:
Aap ek E-commerce website bana rahe hain jisme products list honge aur users order place kar sakenge.
ASP.NET Core MVC ka use karke aap Model-View-Controller architecture follow kar sakte hain.
-----------------------------------------------------------------
Example: Product Listing in ASP.NET Core MVC
📌 Step 1: Controller

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAllProducts();
        return View(products);
    }
}
---------------------------------------------
📌 Step 2: View (Razor Page)

@model List<Product>

<h2>Product List</h2>

@foreach (var product in Model)
{
    <div>
        <h3>@product.Name</h3>
        <p>Price: $@product.Price</p>
    </div>
}
-----------------------------------
📌 Step 3: Model

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
🛠 Why use ASP.NET Core MVC?
✔ Fast performance due to built-in dependency injection
✔ Lightweight & modular architecture
✔ Razor pages make UI rendering easy
---------------------------------------------------------
2. Web API - RESTful API with .NET Core
Agar aapko mobile apps ya Angular/React frontend ke liye backend API develop karni ho, toh ASP.NET Core Web API use kar sakte hain.

Scenario:
Aap ek job portal bana rahe hain jisme recruiters job listings post kar sakein aur job seekers apply kar sakein.

Example: Job API using ASP.NET Core Web API
📌 Step 1: Job Model

public class Job
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Company { get; set; }
    ---------------------------------------------------------
}
📌 Step 2: API Controller

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private static List<Job> jobs = new List<Job>();

    [HttpGet]
    public IActionResult GetAllJobs()
    {
        return Ok(jobs);
    }

    [HttpPost]
    public IActionResult AddJob([FromBody] Job job)
    {
        jobs.Add(job);
        return Created("", job);
    }
}
📌 Step 3: API Call Example (Using Postman or Angular/React)

GET Request: https://localhost:5001/api/job
--------------------------------------------

POST Request (JSON Body):

{
    "title": "Software Engineer",
    "description": "Develop ASP.NET Core applications",
    "company": "Microsoft"
}
🛠 Why use ASP.NET Core Web API?
✔ Ideal for Microservices architecture
✔ High-performance APIs with minimal memory usage
✔ Secure with JWT authentication & authorization
------------------------------------------------
3. Cloud-Ready Applications (Docker + Kubernetes)
Agar aapko cloud-ready applications banani hain jo Azure, AWS, ya Google Cloud par run ho sakein, toh .NET Core best choice hai.

Scenario:
Aap ek multi-tenant SaaS application develop kar rahe hain jo Azure Kubernetes Service (AKS) me deploy hoga.

Dockerfile for .NET Core Application

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "MyApp.dll"]
🛠 Why use .NET Core for Cloud Apps?
✔ Optimized for containers (Docker, Kubernetes)
✔ Auto-scaling supported with Kubernetes
✔ Seamless deployment to Azure App Services
----------------------------------------------------
4. Microservices Architecture with .NET Core
Agar aapko independent services banana hain jo loosely coupled ho, toh Microservices Architecture me .NET Core best fit hai.

Scenario:
Aap ek Food Delivery App develop kar rahe hain jisme Order, Payment, aur Notification alag-alag microservices honge.

Example: Payment Microservice in .NET Core

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    [HttpPost("process")]
    public IActionResult ProcessPayment([FromBody] PaymentRequest request)
    {
        // Simulated payment processing
        return Ok(new { message = "Payment Successful" });
    }
}
-------------------------------
🛠 Why use .NET Core for Microservices?
✔ Scalability – Services independently scale ho sakte hain
✔ Technology Flexibility – Different services Python, Node.js me likh sakte hain
✔ Performance – .NET Core fastest web framework hai

.NET Core Use Cases   (When to Use .NET Core?)
Use Case 	Why Use .NET Core?
Enterprise Web Apps	Fast, scalable, cloud-ready
RESTful APIs	High-performance & secure
Microservices	Independent & loosely coupled
Cloud Apps	Azure, AWS, Kubernetes optimized
IoT & AI Applications	ML.NET, Azure Cognitive Services
Blazor (WebAssembly)	C# frontend apps
---------------------------------
Conclusion
🚀 .NET Core best hai jab aapko:
✅ Cross-platform apps chahiye (Windows, Linux, macOS)
✅ Cloud-ready & scalable applications banani ho
✅ High-performance APIs aur Microservices implement karni ho