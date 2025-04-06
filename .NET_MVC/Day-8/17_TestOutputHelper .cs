using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 17_TestOutputHelper 
    {
        
    }
}
---------------------------------
TestOutputHelper ke baare mein — iska kaam hai test ke andar se kuch log ya messages print karna, jaise debugging ke liye ya test execution trace karne ke liye.

📢 Kya hota hai ITestOutputHelper?
Ye ek xUnit ka built-in interface hai jo test ke output ko console pe likhne ka support deta hai — especially jab Visual Studio, CLI ya CI/CD pipeline me test run hota hai.

💡 Yeh normal Console.WriteLine() ki tarah kaam nahi karta, kyunki test ka output xUnit ke output window me dikhana hota hai.
------------------------------------
🛠 Kaise Use Karte Hain?
1️⃣ Constructor Injection

public class MyTests
{
    private readonly ITestOutputHelper _output;

    public MyTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ShouldPrintMessage()
    {
        _output.WriteLine("Test execution started...");
        _output.WriteLine("Kuch important info ya debug logs yahan likho");

        Assert.True(true);
    }
}
✅ Ye message test output window me dikhega jab tu test run karega Visual Studio ya CLI se.
--------------------------------------
🔍 Real Use Cases
Debugging: Jaise agar test fail ho raha hai, tu dekh sakta hai ki kaha tak execution gaya

Logging: Log karne ke liye kya data aaya, kya assert hua

Assertion se pehle data dikhana: Jaise model ya object ka data

Loop debugging: Agar tu loop me test kar raha hai toh har iteration ka data print
----------------------------------------------
🧪 Example: Print Object Details

public class UserServiceTests
{
    private readonly ITestOutputHelper _output;

    public UserServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void PrintUserObject()
    {
        var user = new { Id = 1, Name = "Rohit", IsActive = true };

        _output.WriteLine($"User ID: {user.Id}");
        _output.WriteLine($"User Name: {user.Name}");
        _output.WriteLine($"Is Active: {user.IsActive}");

        Assert.NotNull(user);
    }
}
------------------------------------------
💡 Important Notes:
Console.WriteLine() se test runner pe kuch nahi dikhega

Har test me ITestOutputHelper inject hota hai

Tu logger setup bhi kar sakta hai jo ITestOutputHelper use kare internally
----------------------------------
🔐 Bonus Tip: Log JSON Object

_output.WriteLine(JsonConvert.SerializeObject(myObject, Formatting.Indented));
Agar tu Newtonsoft.Json use kar raha hai toh object ko poore pretty format me print kar sakta hai.

