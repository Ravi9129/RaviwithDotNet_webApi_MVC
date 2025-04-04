using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-4
{
    public class 16_Literal 
    {
        
    }
}
---------------------------------------
🔹 Literal in C# & ASP.NET Core
📌 Literal Kya Hota Hai?
🔹 Literal ek fixed value hoti hai jo directly code me likhi jati hai aur execution ke time change nahi hoti.
🔹 Yeh constants ki tarah behave karti hai, magar bina kisi identifier ke.
🔹 Literal data types ke hisaab se different hote hain, jaise:

Integer Literal

Floating-Point Literal

Boolean Literal

Character Literal

String Literal

Null Literal
-----------------------------------------
📌 1️⃣ Integer Literal
Yeh integer numbers ko represent karte hain.

👨‍💻 Example:
int age = 25;  // 25 is an integer literal
✅ Yeh ek integer value hai jo change nahi hogi.
-------------------------------------------------
📌 2️⃣ Floating-Point Literal
Yeh decimal numbers ko represent karte hain.

👨‍💻 Example:
double price = 99.99;  // 99.99 is a floating-point literal
float discount = 10.5f; // 'f' bata raha hai ki yeh float type hai
✅ Decimal numbers ko store karne ke liye floating-point literal use hota hai.
-----------------------------------------
📌 3️⃣ Boolean Literal
Yeh sirf true ya false hota hai.

👨‍💻 Example:
bool isAvailable = true;  // 'true' is a boolean literal
bool isDeleted = false;   // 'false' is also a boolean literal
✅ Yeh conditional logic me use hota hai, jaise if-else me.
---------------------------------------
📌 4️⃣ Character Literal
Yeh single character ko represent karta hai aur single quotes (' ') me likha jata hai.

👨‍💻 Example:
char grade = 'A';  // 'A' is a character literal
char symbol = '#'; // '#' is also a character literal
✅ Ek character ko store karne ke liye char type use hota hai.
-------------------------------------------------
📌 5️⃣ String Literal
Yeh text ko represent karta hai aur double quotes (" ") me likha jata hai.

👨‍💻 Example:
string message = "Hello, World!"; // "Hello, World!" is a string literal
✅ Web pages me content display karne ke liye string literals ka use hota hai.
---------------------------------------  
🔹 Multiline String Literal (Verbatim String @)

string multiLine = @"This is
a multi-line 
string literal.";
✅ Yeh @ operator multiple lines ko as-is treat karta hai.
--------------------------------------------
📌 6️⃣ Null Literal
Yeh kisi object ko null reference dene ke liye use hota hai.

👨‍💻 Example:

string? name = null; // 'null' is a null literal
object obj = null;
✅ Jab kisi variable me koi value nahi hoti toh null assign karte hain.
---------------------------------------------------
📌 7️⃣ ASP.NET Core me Literal Ka Use
🔹 String Literal in Razor View
<p>@("This is a string literal")</p>
-----------------------------------------------
🔹 Integer Literal in Razor View

<p>@(100 + 50)</p>  <!-- Output: 150 -->
-----------------------------------------
🔹 Boolean Literal in Condition
@if (true) {
    <p>This is always displayed</p>
}
---------------------------------------
🔹 Null Literal in Model Binding

public IActionResult GetUser(string? name)
{
    if (name == null)
    {
        return Content("No user found");
    }
    return Content($"User: {name}");
}
------------------------------------------------
📌 Summary
✅ Literals fixed values hoti hain jo runtime pe change nahi hoti.
✅ Types: Integer, Floating-Point, Boolean, Character, String, Null.
✅ ASP.NET Core me literals ka use Razor Views, Model Binding aur Controller logic me hota hai.