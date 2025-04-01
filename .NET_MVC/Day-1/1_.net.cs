using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-1
{
    public class 1_.net
    {
        
    }
}
---------------------------------------------------
.NET Framework Kya Hai?
.NET ek software development framework hai jo Microsoft ne banaya hai. Iska main purpose developers ko tools aur libraries provide karna hai, jisse woh software applications efficiently develop kar sakein. .NET platform ka use karke aap web applications, desktop applications, mobile apps, games, aur even enterprise level software bhi bana sakte ho.

.NET ka ek advantage yeh hai ki yeh cross-platform hai, iska matlab hai ke aap ek hi codebase se Windows, Linux, aur macOS par apps bana sakte ho.

Real-World Example:
Scenario: Aap ek eCommerce website bana rahe ho, jisme product details show karna hai, shopping cart ka feature ho, aur payment gateway integration bhi ho. Is project ko aap .NET par implement karte ho.
-----------------------------------------------------
1. Web Application (ASP.NET Core):
Aapko ek scalable aur fast web application chahiye. Toh, ASP.NET Core ka use karenge. Yeh .NET ka ek framework hai jo specifically web applications aur APIs banane ke liye design kiya gaya hai.

Why use it?

ASP.NET Core ko use karke aap fast, secure, aur scalable web applications bana sakte ho.

Yeh cross-platform hai, toh aap Windows, Linux, aur macOS sab pe deploy kar sakte ho.

Yeh cloud-ready bhi hai, jiska matlab hai ki aap easily Azure par apna application deploy kar sakte ho.
-------------------------------------------------------
Example: Aap ek product page bana rahe hain jisme product ka naam, description, aur price dikhana hai. Yeh data aapke database se fetch hoga. Aap ASP.NET Core MVC ko use karenge is functionality ko implement karne ke liye.


// Controller Code Example
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
--------------------------------------------------------------
2. Backend APIs (Web API using ASP.NET Core):
Agar aapko front-end aur back-end ko separate karna hai, toh aap ASP.NET Core Web API use kar sakte ho.
 Isse aapke client-side aur server-side ka communication RESTful APIs ke through hoga.

Why use it?

Web APIs lightweight hote hain aur easy to scale hote hain.

Aap multiple clients ke liye ek hi API bana sakte ho jo mobile app, web, aur other services se interact kare.
---------------------------------------------------
Example: Aap payment gateway integration kar rahe hain, toh payment ke liye ek API call banani padegi jo server ko request bheje. 
Is API ko handle karne ke liye aap ASP.NET Core Web API use karenge.

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("process")]
    public IActionResult ProcessPayment(PaymentRequest paymentRequest)
    {
        var result = _paymentService.Process(paymentRequest);
        if (result.IsSuccess)
        {
            return Ok("Payment Processed Successfully");
        }
        return BadRequest("Payment Failed");
    }
}
-------------------------------------------------------
3. Desktop Application (WinForms or WPF):
Agar aapko ek desktop application banana ho, jisme GUI interface ho, toh aap WinForms ya WPF (Windows Presentation Foundation) ka use kar sakte ho.

Why use it?

WinForms aur WPF, Windows desktop applications ke liye use kiye jate hain.

Yeh easy-to-use GUI framework provide karte hain jo aapko fast application development me madad karte hain.
--------------------------------------------------------------------
Example: Agar aap ek desktop application bana rahe hain jo products ko manage kare, toh WPF aapke liye best option hoga kyunki WPF rich UI features provide karta hai.

// WPF Button Click Example
private void AddProductButton_Click(object sender, RoutedEventArgs e)
{
    var product = new Product { Name = ProductNameTextBox.Text, Price = decimal.Parse(ProductPriceTextBox.Text) };
    _productService.AddProduct(product);
}
Conclusion:
Web Application (ASP.NET Core): Agar aapko fast aur scalable web application banani hai.

Web API (ASP.NET Core): Agar aapko client aur server ke beech RESTful communication chahiye.

Desktop Application (WPF/WinForms): Agar aapko rich GUI-based desktop application banani hai.

.NET ka use har type ke application development mein hota hai, aur yeh cross-platform aur cloud-ready hone ki wajah se ek popular choice hai.