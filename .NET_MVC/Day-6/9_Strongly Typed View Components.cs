using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-6
{
    public class 9_Strongly Typed View Components
    {
        
    }
}
--------------------------------------
Strongly Typed View Components ki — iska matlab tu ViewComponent ke view me model ka type define karta hai, jaise strongly typed views me karte hain.

Yeh approach clean, safe aur compile-time checking ke liye best hai — koi "object" ya "dynamic" ka jhanjhat nahi!

✅ Real Example — Strongly Typed ViewComponent
🧠 Scenario:
Tu ek “Recent Articles” ka ViewComponent bana raha hai, jo last 5 articles show karega.
Aur tu chah raha hai ki ViewComponent view me List<Article> model use kare — strongly typed.
--------------------------------------------------
🔹 Step 1: Create a Model
public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime PublishedOn { get; set; }
}
----------------------------------------------
🔹 Step 2: Create a Strongly Typed ViewComponent
public class RecentArticlesViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public RecentArticlesViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var recentArticles = _context.Articles
                                     .OrderByDescending(a => a.PublishedOn)
                                     .Take(5)
                                     .ToList();

        return View(recentArticles); // Strongly typed list passed
    }
}
✍️ Important: View ka naam Default.cshtml hona chahiye ya jo tu explicitly specify kare.
----------------------------------------------------------------
🔹 Step 3: ViewComponent View — Strongly Typed
📁 Views/Shared/Components/RecentArticles/Default.cshtml
@model List<Article>

<div class="recent-articles">
    <h4>📰 Recent Articles</h4>
    <ul>
        @foreach (var article in Model)
        {
            <li>
                <strong>@article.Title</strong>  
                <small>(@article.PublishedOn.ToShortDateString())</small>
            </li>
        }
    </ul>
</div>
-------------------------------------
🔹 Step 4: Use in Any View
@await Component.InvokeAsync("RecentArticles")
-----------------------------------------------------
✅ Fayde (Benefits):
💥 Compile-time checking — errors jaldi milte hain

🧼 Cleaner code — koi casting nahi

🔐 Type safety — koi galat model use nahi hoga

🔄 Easily reusable with real data models
---------------------------------------------
🧠 Kab Use Kare?
Jab ViewComponent me tu model list ya object bhej raha hai

Jab tu foreach ya model ke property access kar raha hai view me

Jab business logic se data pass kar raha hai ViewComponent ke view me

