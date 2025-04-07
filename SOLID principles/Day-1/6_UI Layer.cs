using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLID principles.Day-1
{
    public class 6_UI Layer
    {
        
    }
}
-----------------------------------------------------
UI Layer in Clean Architecture – full detail ke saath, real-world examples aur practical approach 
jaise tu pehle bola tha – bina table format, seedhe dil se samjhayenge 💯

🎨 What is the UI Layer?
UI Layer yaani User Interface Layer wo layer hoti hai jo directly user ke saamne hoti hai.
 Ye application ka front face hota hai — yahin se user input deta hai, data dekhte hain, aur application se interact karta hai.

“Ye wo layer hai jo users ko dikhayi deti hai, lekin andar core logic tak direct access nahi karti — 
use sirf Application layer ke through baat karti hai.”
-----------------------------------------------
🧱 Role of UI Layer in Clean Architecture
Display data to user (View)

Take input from user (Forms, fields, clicks)

Send commands/queries to Application layer

Handle responses/errors from Application layer

Maintain zero business logic (sirf presentation logic)
-----------------------------------
💡 Real-World Analogy
Imagine ek restaurant:

Customer → UI

Waiter → Application Layer

Chef → Core Layer

Grocery/Storage → Infrastructure Layer

Customer (user) order deta hai UI pe. Waiter (application) wo request le jaata hai chef ke paas. Chef (core logic) us request ke basis pe dish banata hai. Aur agar fridge se kuch chahiye (infra), toh chef infra ko call karta hai.
-----------------------------------------------
🔨 Technologies for UI Layer (depending on app type)
ASP.NET Core MVC / Razor Pages
Traditional web apps — Views, Controllers, Forms, Tag Helpers

Blazor (Server or WASM)
Modern .NET SPA — Component-based UI

Angular / React / Vue (Frontend)
Agar tu Clean Architecture REST API bana raha hai backend me, toh ye frameworks frontend me use hote hain.

MAUI / Xamarin
For cross-platform mobile and desktop apps
----------------------------------------------------------
🧠 Example: Invoice Application
UI Layer me Razor Pages ya Blazor Server hai:

User ek invoice create form fill karta hai

Ye data ViewModel ke through Application layer me jata hai

Application use process karke response deta hai (invoice ID, status)

UI us response ko user ko dikhata hai
------------------------
<EditForm Model="@invoiceModel" OnValidSubmit="HandleSubmit">
    <InputText @bind-Value="invoiceModel.CustomerName" />
    <InputNumber @bind-Value="invoiceModel.Amount" />
    <button type="submit">Create</button>
</EditForm>
---------------------
private async Task HandleSubmit()
{
    var result = await _invoiceService.CreateInvoiceAsync(invoiceModel);
    if (result.Success)
    {
        // Show success message
    }
}
----------------------------------------------
🧠 Clean Architecture Rule
❗ UI should not know anything about Core or Infrastructure

UI ← Application Layer → Core
UI sirf Application Services ya Mediator/DTO ke through data send/receive karta hai.
----------------------------------------------------
✅ What it must contain:
Pages / Components / Views

Controllers (MVC) or Razor Pages / Blazor Components

ViewModels or Form Models

Only basic form validation logic

No DB calls, no business logic, no direct repo access
---------------------------------------------------
🚫 What it must NOT contain:
Direct DbContext usage

Business rules

Any knowledge of infrastructure

Services defined in Core directly

🔐 Security Note
Even though UI layer me user ka input aata hai, validation aur authorization back-end me Application ya Core layer me confirm hoti hai. Kyunki UI me hone wali validation easily bypass ki ja sakti hai (browser se, tools se, etc.).
----------------------------------------------------
🧪 Testing the UI Layer
UI Testing with tools like Selenium, Playwright, bUnit (for Blazor)

Minimal unit testing – zyadatar interaction ya integration level pe test hoti hai
------------------------------------------------------
🔄 Example Workflow in Clean Arch
UI (Blazor Page) — gets form data

Sends command to MediatR or ApplicationService

Application validates & passes to Core

Core executes rules & calls repository via interface

Infrastructure layer performs DB save

Result returns → shown to user in UI
----------------------------------------------------
📦 Typical UI Structure (Blazor/ASP.NET Core)

/UI
  /Pages
    - CreateInvoice.razor
    - InvoiceList.razor
  /Components
    - InvoiceCard.razor
  /ViewModels
    - CreateInvoiceModel.cs
  /Services
    - IInvoiceUIService.cs
    - InvoiceUIService.cs
    -----------------------------------------------------
🧠 Interview Style Explanation
"The UI Layer is responsible for user interaction in a Clean Architecture system.
 It communicates with the Application Layer via commands and queries, handles rendering and collecting input,
  but does not contain business logic or direct data access. This promotes testability and separation of concerns."
--------------------------------------------------------------------
🔚 Summary
UI = Face of app, but no brains (business logic)

Talks only to Application layer

Technologies vary: Razor, Blazor, Angular, React

Never accesses Core or Infra directly

Presents data, takes input, shows results

