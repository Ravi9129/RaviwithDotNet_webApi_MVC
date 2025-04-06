using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 10_Details View
    {
        
    }
}
---------------------------------------
Details View ki — iska kaam hota hai ek specific item ka full detail dikhana user ko — bina kisi editing ke, sirf read-only view.

🔍 Real-Life Example:
Soch tu ek E-commerce app bana raha hai. Jab user kisi product pe click karta hai, toh usse ek Details page dikhta hai jisme product ka naam, price, description, aur image hoti hai.

Wahi concept .NET me bhi hai — har item ka Detail View hota hai jahan uska full data show hota hai.
--------------------------------------------------
🔧 Step-by-Step Example — "Blog Post Details"
🔹 Step 1: Model

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
-----------------------------------
🔹 Step 2: Controller Action

public IActionResult Details(int id)
{
    var post = _context.BlogPosts.FirstOrDefault(x => x.Id == id);
    if (post == null)
        return NotFound();

    return View(post);
}
🔑 Yahan id query string ya route se aata hai, aur us ID ka data hum fetch karte hain.
-----------------------------------------------------------
🔹 Step 3: Create Details View (Details.cshtml)

@model BlogPost

<h2>Post Details</h2>

<div class="card p-3 shadow">
    <h4>@Model.Title</h4>
    <p><strong>Posted On:</strong> @Model.CreatedAt.ToString("dd MMM yyyy")</p>
    <p>@Model.Content</p>

    <a asp-action="Index" class="btn btn-secondary mt-3">Back to List</a>
</div>
----------------------------------------------
🧠 Important Baatein:
Details view read-only hota hai.

Yeh mostly sirf ek model ka object show karta hai.

Agar item null milta hai toh NotFound() return karte hain.

View strongly typed hoti hai (@model BlogPost).
-------------------------------------------------
✅ Kab Use Karein?
Jab user ko kisi single record ka full data dikhana ho.

Product detail, Blog post detail, User profile detail, etc.

Jab koi “View” button click karta hai.