using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 3_ASP.NET Core
    {
        
    }
}
------------------------------------
ASP.NET Core ek cross-platform, high-performance, open-source web framework hai jo modern web applications aur APIs banane ke liye use hota hai. Ye .NET Core ka part hai aur ASP.NET Framework ka upgraded version hai. Isme performance, security, scalability, aur cloud-optimization ko enhance kiya gaya hai.

🔹 Key Features of ASP.NET Core:
✔ Cross-platform – Windows, Linux, macOS par chal sakta hai
✔ Fast & lightweight – High-performance web applications develop karne ke liye
✔ Modular architecture – Dependency Injection aur Middleware system
✔ Cloud-ready – Docker, Kubernetes aur Azure ke liye optimized
✔ Razor Pages, MVC, Web API & Blazor support – Multiple development models
✔ Microservices architecture – Loosely coupled services ke liye best
✔ Secure authentication & authorization – Identity, OAuth, JWT

ASP.NET Core vs ASP.NET Framework
Feature	ASP.NET Core	ASP.NET Framework
Platform	Windows, Linux, macOS	Windows only
Performance	Faster, lightweight	Moderate
Dependency Injection	Built-in support	Limited
Microservices Support	Best for Microservices	Limited
Cloud-Ready	Optimized for cloud & containers	Not fully optimized
Future Updates	Actively developed	Only security fixes
✅ Agar aap new project start kar rahe hain toh ASP.NET Core best choice hai!

Real-World Use Cases
1️⃣ E-commerce Website (MVC Architecture)
2️⃣ RESTful Web API for Mobile Apps
3️⃣ Cloud-based SaaS Application
4️⃣ Enterprise Web Applications
5️⃣ IoT Applications with SignalR
6️⃣ Microservices-based Architecture
7️⃣ Blazor Apps for Full-Stack C# Development

1. ASP.NET Core MVC - Web Application
Agar aap ek dynamic, scalable web application develop kar rahe hain jisme frontend/backend ka proper structure ho, toh ASP.NET Core MVC best hai.

🛠 Scenario:
Aap ek E-commerce website develop kar rahe hain jisme users product list dekh sakein aur shopping cart me add kar sakein.
-----------------------------------------------------------------------------
📌 Step 1: Create MVC Controller (ProductController.cs)

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
----------------------------------------------------
📌 Step 2: Create Model (Product.cs)

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
----------------------------------------------------------
📌 Step 3: Create Razor View (Index.cshtml)

@model List<Product>

<h2>Product List</h2>

@foreach (var product in Model)
{
    <div>
        <h3>@product.Name</h3>
        <p>Price: $@product.Price</p>
        <p>@product.Description</p>
    </div>
}
✅ Why Use ASP.NET Core MVC?
✔ Model-View-Controller architecture – Maintainability aur scalability ke liye
✔ Dependency Injection built-in – Code modular aur reusable hota hai
✔ Razor View Engine – Fast HTML rendering aur dynamic UI support

----------------------------------------------------------
2. ASP.NET Core Web API - RESTful Services
Agar aapko mobile apps ya Angular/React frontend ke liye backend API develop karni ho, toh ASP.NET Core Web API best hai.

🛠 Scenario:
Aap ek job portal develop kar rahe hain jisme recruiters job listings post kar sakein aur job seekers apply kar sakein.

📌 Step 1: Create Job Model (Job.cs)

public class Job
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Company { get; set; }
}
------------------------------------------------
📌 Step 2: Create API Controller (JobController.cs)

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
----------------------------------------------------
📌 Step 3: API Call Example (Using Postman or Angular/React)

GET Request: https://localhost:5001/api/job

POST Request (JSON Body):

{
    "title": "Software Engineer",
    "description": "Develop ASP.NET Core applications",
    "company": "Microsoft"
}
✅ Why Use ASP.NET Core Web API?
✔ Microservices support – Independent & loosely coupled services
✔ High-performance APIs – Minimal memory usage
✔ Secure API with JWT authentication
--------------------------------------------------------------------------
3. ASP.NET Core Blazor - Full-Stack C# Development
Agar aapko full-stack C# development karni ho bina JavaScript ke, toh Blazor best hai.

🛠 Scenario:
Aap ek Real-Time Chat Application develop kar rahe hain jo WebSockets aur SignalR ka use karega.
----------------------------------------------------
📌 Step 1: Install Blazor Server App

dotnet new blazorserver -n ChatApp
cd ChatApp
dotnet run
--------------------------------------------------
📌 Step 2: Create Chat Component (Chat.razor)

@page "/chat"
<h3>Live Chat</h3>
<input @bind="message" />
<button @onclick="SendMessage">Send</button>

<ul>
    @foreach (var msg in messages)
    {
        <li>@msg</li>
    }
</ul>

@code {
    private string message;
    private List<string> messages = new List<string>();

    private void SendMessage()
    {
        messages.Add(message);
        message = "";
    }
}
✅ Why Use Blazor?
✔ C# frontend + backend – No need for JavaScript
✔ Fast & lightweight – WebAssembly based execution
✔ Real-time updates – SignalR integration
-----------------------------------------------------
4. ASP.NET Core with Docker & Kubernetes
Agar aapko cloud-ready applications banani hain jo Azure, AWS, ya Google Cloud par run ho sakein, toh Docker aur Kubernetes ke saath ASP.NET Core best hai.
------------------------------------------------------------
📌 Dockerfile for ASP.NET Core Application

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "MyApp.dll"]
-------------------------------------------------
📌 Run the Docker container

docker build -t myapp .
docker run -p 5000:80 myapp
✅ Why Use ASP.NET Core for Cloud Apps?
✔ Optimized for containers (Docker, Kubernetes)
✔ Auto-scaling supported with Kubernetes
✔ Seamless deployment to Azure App Services
----------------------------------------------------
Conclusion
🚀 ASP.NET Core best hai jab aapko:
✅ High-performance Web Applications banani ho
✅ Scalable APIs develop karni ho
✅ Microservices architecture implement karni ho
✅ Cloud-optimized applications banani ho
✅ Full-stack development with C# (Blazor) karni ho