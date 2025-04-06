using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 1_unit testing
    {
        
    }
}
-------------------------------------------
Unit Testing ek core concept hai — jo ensure karta hai ki har individual unit (method, service, logic) expected behavior follow kar raha hai. ASP.NET Core me mostly xUnit + Moq + TestServer use hote hain.

🔥 What is Unit Testing?
Unit Testing is the process of testing individual units or components of code (like methods, services) in isolation.
------------------------------------------------------
✅ Benefits:
Bugs jaldi milte hain

Code reliable hota hai

Refactoring safe hota hai

CI/CD pipelines me fit baithta hai

-------------------------------------------
🧪 Sample Method to Test

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
----------------------------------------
✅ xUnit Unit Test

public class CalculatorTests
{
    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        // Arrange
        var calc = new Calculator();

        // Act
        var result = calc.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }
}
------------------------------------------------
🧠 Naming Convention for Unit Tests

MethodName_Scenario_ExpectedBehavior
-------------------------------------------------------
Example:
Add_TwoPositiveNumbers_ReturnsCorrectSum
--------------------------------------------------------
🔁 Test with Dependency (Mocking Example)

public interface IEmailService
{
    void Send(string to, string message);
}

public class NotificationService
{
    private readonly IEmailService _emailService;
    public NotificationService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public bool Notify(string user)
    {
        if (string.IsNullOrEmpty(user)) return false;

        _emailService.Send(user, "Welcome!");
        return true;
    }
}
-------------------------------------------------
✅ Unit Test with Moq

public class NotificationServiceTests
{
    [Fact]
    public void Notify_ValidUser_CallsEmailSend()
    {
        var mock = new Mock<IEmailService>();
        var service = new NotificationService(mock.Object);

        var result = service.Notify("test@x.com");

        Assert.True(result);
        mock.Verify(e => e.Send("test@x.com", "Welcome!"), Times.Once);
    }
}
--------------------------------------------------------
🧪 Unit Test Tips
One test = one purpose

Use [Fact] for regular tests

Use [Theory] with [InlineData] for parameterized tests

Mock only external dependencies

Avoid testing private methods
--------------------------------------------------
🔍 Bonus: Test Project Setup

dotnet new xunit -n MyApp.Tests
dotnet add MyApp.Tests reference ../MyApp/MyApp.csproj
dotnet add MyApp.Tests package Moq
dotnet add MyApp.Tests package FluentAssertions
