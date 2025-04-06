using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 16_xUnit 
    {
        
    }
}
--------------------------------------------
xUnit ke baare mein — ekdum real-world code, real scenario, simple bhasha mein.
Agar tu .NET mein testing kar raha hai, toh xUnit tere liye ek default testing framework ban gaya hai.

🧠 xUnit Kya Hota Hai?
xUnit ek unit testing framework hai .NET ke liye.
Iska kaam hai:
➡️ Tere code ko test karna
➡️ Har class/method sahi kaam kar rahi hai ya nahi, ye check karna
➡️ Bugs ko jaldi pakadna before production
------------------------------------------------
📌 Kyun use karein xUnit?
✅ Code reliable banane ke liye
✅ Har function, class ko alag-alag test karne ke liye
✅ Refactor karte waqt code todta hai ya nahi, ye pakadne ke liye
✅ CI/CD me test run karne ke liye (e.g. GitHub Actions, Azure DevOps)
-------------------------------------------
💡 Real Scenario Example
Scenario: Tu ek calculator bana raha hai jisme Add method hai
📦 Step 1: Calculator class banate hain

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
---------------------------------------
🧪 Step 2: xUnit test likhna
public class CalculatorTests
{
    [Fact] // ye xUnit ka attribute hai
    public void Add_ShouldReturnCorrectSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(2, 3);

        // Assert
        Assert.Equal(5, result); // expected, actual
    }
}
--------------------
🔍 Important Concepts:
Term	Explanation
Fact	Ek normal test method, bina parameters ke
Theory	Test method with multiple sets of data
InlineData	Data dene ke liye Theory ke saath use hota hai
--------------------------------------
🔁 Example: Theory + InlineData

public class CalculatorTests
{
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(10, 5, 15)]
    public void Add_ShouldWorkWithMultipleData(int a, int b, int expected)
    {
        var calculator = new Calculator();

        var result = calculator.Add(a, b);

        Assert.Equal(expected, result);
    }
}
--------------------------------------------
🛠 Test Setup with Constructor (Dependency Injection)

public class MyServiceTests
{
    private readonly MyService _service;

    public MyServiceTests()
    {
        _service = new MyService();
    }

    [Fact]
    public void Something_ShouldWork()
    {
        var result = _service.DoSomething();
        Assert.True(result);
    }
}
----------------------------------
🔄 Common Assertions in xUnit
Assert.Equal(expected, actual)

Assert.NotNull(object)

Assert.True(condition)

Assert.Throws<ExceptionType>(() => method())
------------------------------------------
🛠 Mocking (If class has dependency)
Tu agar koi service test kare jisme koi aur service inject hai, toh Moq use karega.

var mockService = new Mock<IMyDependency>();
mockService.Setup(x => x.DoSomething()).Returns(true);
----------------------------------
🏁 Run Karne Kaise?
Tu Visual Studio ya dotnet test CLI se run kar sakta hai:
-----------------
dotnet test
----------------------------------------
🚀 Real Use Cases:
Controller testing

Service testing (business logic)

DB se disconnected test (using mocking)

API layer test

Before CI/CD deployment testing
---------------------------------------------
🔐 BONUS: Common Folder Structure for xUnit
/MyProject
  /Services
    - Calculator.cs

/MyProject.Tests
  /Services
    - CalculatorTests.cs
    -----------------------------------------------
📢 Final Note:
🧪 xUnit se tu bug-free, clean, and testable code likh sakta hai.
Real project me deploy karne se pehle test likhna must hai.

