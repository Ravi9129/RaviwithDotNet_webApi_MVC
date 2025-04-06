using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-11
{
    public class 4_Fluent Assertions
    {
        
    }
}
--------------------------------------------
Fluent Assertions ek powerful .NET library hai jo unit tests ko readable, 
expressive, aur natural language jaise banata hai. Iska main goal hai test readability ko boost karna, 
taaki assert likhna simple aur intuitive ho jaye.

✅ Why Fluent Assertions?
🟢 Readable
🟢 IntelliSense-friendly
🟢 Strong error messages
🟢 Chaining support
🟢 Works with xUnit, NUnit, MSTest
---------------------------------------------
📦 Installation

dotnet add package FluentAssertions
------------------------------
🚀 Basic Usage
🧪 Without Fluent Assertions (xUnit)

Assert.Equal(5, result);
--------------------------------------
✅ With Fluent Assertions

result.Should().Be(5);
----------------------------------
🧪 Examples
✔️ String Assertions

string name = "Fluent";
name.Should().StartWith("Flu").And.EndWith("ent").And.HaveLength(6);
----------------------------------------------------
✔️ Object Assertions

var obj = new Person { Name = "Ravi", Age = 25 };

obj.Should().BeEquivalentTo(new Person { Name = "Ravi", Age = 25 });
-----------------------------------------------
✔️ Collections

var numbers = new[] { 1, 2, 3 };
numbers.Should().Contain(2).And.HaveCount(3);
-----------------------------------------------
✔️ Exception Assertions

Action act = () => throw new InvalidOperationException("Invalid!");

act.Should().Throw<InvalidOperationException>()
    .WithMessage("Invalid!");
    ------------------------------------------
✔️ Nullable Assertions

int? value = 5;
value.Should().HaveValue().And.Be(5);
--------------------------------------------
✔️ DateTime

DateTime dt = DateTime.Today;
dt.Should().BeAfter(DateTime.Today.AddDays(-1));
---------------------------------------------
💡 Custom Assertion Messages

value.Should().Be(5, "because we expect 5 after calculation");
-----------------------------------------------------
🛠 FluentAssertions.Extensions
You can even extend it to work with ASP.NET Core responses, e.g.:


response.StatusCode.Should().Be(HttpStatusCode.OK);
