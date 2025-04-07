using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOLID principles.Day-1
{
    public class 7_Tests 
    {
        
    }
}
--------------------------------------
🧪 Testing in Clean Architecture
Clean Architecture ka sabse bada faida hi yeh hai ki har layer easily testable hoti hai, aur logic segregated hota hai. Is wajah se hum different types of testing easily kar sakte hain:
---------------------------------
🔹 1. Unit Tests
📌 Purpose:
Sirf ek chhoti si unit ko test karta hai — jaise ek method, ek class, ek service.
-------------------------------------------------------
✅ Clean Architecture mein kisko test karte hain?
Application layer ke services / use-cases

Domain layer ke rules / entities

Core logic – bina external dependencies ke
-------------------------------------------------

🔧 Example:
Use case: CreateInvoiceHandler

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, Result>
{
    public async Task<Result> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.CustomerName))
            return Result.Failure("Customer name is required.");

        // Simulate DB insert
        return Result.Success("Invoice created.");
    }
}
------------------------------------
Test Case:

[Fact]
public async Task Handle_ShouldReturnFailure_WhenCustomerNameIsEmpty()
{
    var handler = new CreateInvoiceHandler();
    var result = await handler.Handle(new CreateInvoiceCommand { CustomerName = "" }, default);

    result.IsSuccess.Should().BeFalse();
    result.ErrorMessage.Should().Be("Customer name is required.");
}
---------------------------------------------
🧠 Learnings:

Test is only checking business logic.

No DB, no API, no UI.

Fast, focused, clean.
-------------------------------------------
🔹 2. Integration Tests
📌 Purpose:
End-to-end test karta hai system ke different components ka interaction. Mostly Application + Infrastructure test hota hai ismein.
--------------------
✅ Clean Architecture mein kahan use hote hain?
Application + Infra layer ka real flow

Repository + DbContext + real DB (Test DB)
-----------------------------------------------------
🏗️ Real-World Example:
Tu chah raha hai verify karna:

"CreateInvoiceHandler properly saves invoice to the real DB (Test DB), and returns correct ID."
----------------
[Fact]
public async Task CreateInvoice_ShouldSaveToDatabase()
{
    using var context = new AppDbContext(TestDbOptions);
    var handler = new CreateInvoiceHandler(context);

    var command = new CreateInvoiceCommand { CustomerName = "Faizan", Amount = 5000 };
    var result = await handler.Handle(command, CancellationToken.None);

    result.IsSuccess.Should().BeTrue();
    context.Invoices.Count().Should().Be(1);
}
------------------------------------------
🧠 Tu dekh: yahan application aur infra (DB) dono interact kar rahe hain. Ye slow hota hai unit test se, 
lekin zaroori hota hai real flow validate karne ke liye.
---------------------------------------
🔹 3. UI / End-to-End Tests (E2E)
📌 Purpose:
User ke level pe test karna — poora flow: UI → App Layer → DB → Response
-----------------------------------------
🔧 Tech: Playwright, Selenium, Cypress (Agar frontend hai)
✅ Clean Arch mein mostly WebApp or API level pe hota hai:
API Test via Postman / integration test project

UI Test via Browser automation
-------------------------------------------------
Example (ASP.NET API Test):

[Fact]
public async Task POST_CreateInvoice_ReturnsSuccess()
{
    var client = _factory.CreateClient();

    var response = await client.PostAsJsonAsync("/api/invoices", new {
        CustomerName = "Test",
        Amount = 2000
    });

    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var result = await response.Content.ReadFromJsonAsync<Result>();
    result.IsSuccess.Should().BeTrue();
}
--------------------------------------------
🔹 4. Domain Tests (Entity logic)
Clean Architecture mein Domain layer test karna easy hota hai.

[Fact]
public void Invoice_ShouldCalculateTotalCorrectly()
{
    var invoice = new Invoice("Customer", 1000);
    invoice.AddDiscount(10); // 10%

    invoice.TotalAmount.Should().Be(900);
}
No infrastructure. Just pure domain logic.
-------------------------------------------------
🧠 Benefits of Testing in Clean Architecture
Testable by design (separation of concerns)

Unit tests don’t break due to infrastructure

Application logic lives in isolation — easy to mock

Better confidence in deployments

Faster feedback loop
-------------------------------------------------
🤖 Tools You Can Use
xUnit / NUnit / MSTest – for Unit/Integration tests

FluentAssertions – for readable assertions

Moq / NSubstitute – mocking dependencies

Testcontainers – for running DB in Docker for integration tests

Playwright / Selenium – for UI testing
------------------------------------------------
🔄 Recommended Test Structure in Clean Arch

/Tests
  /UnitTests
    /Application
      - CreateInvoiceTests.cs
    /Domain
      - InvoiceTests.cs
  /IntegrationTests
    - CreateInvoiceDbTests.cs
  /E2ETests
    - InvoiceApiTests.cs
    ---------------------------------------------------
🔥 Real-World Analogy
Tere ghar ka geyser le:

Unit test: Geyser ka thermostat kaam kar raha ya nahi

Integration test: Geyser button dabane pe paani garam ho raha ya nahi

E2E test: Tap kholo → paani aaya → temperature thik hai → kaam done ✅

