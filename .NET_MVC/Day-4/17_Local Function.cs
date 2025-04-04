using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 17_Local Function
    {
        
    }
}
-------------------------------------------------
🔹 Local Function in C# & ASP.NET Core
📌 Local Function Kya Hota Hai?
🔹 Local Function ek method ke andar defined hoti hai aur sirf usi method ke andar accessible hoti hai.
🔹 Yeh Encapsulation aur Readability badhane ke liye use hoti hai.
🔹 C# 7.0 se introduce hui hai.
🔹 Lambda Expression ki jagah bhi use ho sakti hai, lekin iska proper name hota hai.
---------------------------------------------------
📌 1️⃣ Local Function Ka Basic Example

using System;

class Program
{
    static void Main()
    {
        void Greet(string name)
        {
            Console.WriteLine($"Hello, {name}!");
        }

        Greet("Rahul");  // Output: Hello, Rahul!
    }
}
✅ Yahan Greet ek local function hai jo Main() ke andar hi define aur call ho rahi hai.
------------------------------------------
📌 2️⃣ Local Function Ke Rules
✔ Method ke andar define hoti hai.
✔ Local Function ko sirf usi method ke andar call kar sakte hain.
✔ Private Scope hota hai, iska scope sirf parent method tak limited hota hai.
---------------------------------------------------------
📌 3️⃣ Local Function vs Lambda Expression
🔹 Local Function:

Properly named function hoti hai.

return statement use kar sakti hai.

ref aur out parameters support karti hai.
---------------------------------------
🔹 Lambda Expression:

Anonymous function hoti hai.

Short syntax hoti hai (()=> { }).

ref aur out parameters support nahi karti.
-------------------------------
👨‍💻 Example - Lambda vs Local Function

Func<int, int> squareLambda = x => x * x; // Lambda Expression

int SquareLocal(int x) // Local Function
{
    return x * x;
}

Console.WriteLine(squareLambda(5)); // Output: 25
Console.WriteLine(SquareLocal(5)); // Output: 25
✅ Lambda aur Local Function dono same kaam kar rahi hain, par Local Function named hai aur zyada control deti hai.
-----------------------------------------------
📌 4️⃣ Local Function with Recursion
Local Function recursion ke liye bhi use ho sakti hai.

👨‍💻 Example - Factorial Calculation

using System;

class Program
{
    static void Main()
    {
        int Factorial(int n)
        {
            if (n <= 1)
                return 1;
            return n * Factorial(n - 1);
        }

        Console.WriteLine(Factorial(5)); // Output: 120
    }
}
✅ Yahan Factorial local function hai jo apne aap ko call kar raha hai.
--------------------------------------------------------
📌 5️⃣ Local Function in ASP.NET Core
Local Functions Controllers aur Business Logic me bhi use hoti hain.

👨‍💻 Example - Local Function in Controller

public IActionResult GetUserDetails(int id)
{
    bool IsValidId(int userId) => userId > 0; // Local Function

    if (!IsValidId(id))
    {
        return BadRequest("Invalid ID");
    }

    return Ok($"User Details for ID: {id}");
}
✅ Yahan IsValidId local function hai jo ID validate kar raha hai.
----------------------------------------------------------------
📌 6️⃣ Local Function with Asynchronous Method
Local Function async bhi ho sakti hai.

👨‍💻 Example - Async Local Function
public async Task<IActionResult> GetData()
{
    async Task<string> FetchData()
    {
        await Task.Delay(1000); // Simulating delay
        return "Data Loaded";
    }

    string result = await FetchData();
    return Ok(result);
}
✅ Yahan FetchData ek async local function hai jo database call ko simulate kar raha hai.
------------------------------------------------------
📌 Summary
✅ Local Function method ke andar define hoti hai.
✅ Encapsulation aur Code Readability improve karti hai.
✅ Lambda expressions se zyada powerful hoti hai.
✅ ASP.NET Core controllers aur async calls me bhi useful hai.