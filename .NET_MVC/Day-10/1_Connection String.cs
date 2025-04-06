using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-10
{
    public class 1_Connection String
    {
        
    }
}
--------------------------------------------
Connection String ek chhoti si line hoti hai jo batati hai ki tumhari application ko kaunsa database, kahan aur kaise access karna hai.

🔍 Simple Definition:
Connection String ek text format me hoti hai jisme server ka address, database ka naam, username/password ya trusted connection jaise details hoti hain — jo batati hai application ko DB se kaise connect hona hai.
----------------------------------------------------
🔧 Real Example (SQL Server):

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=MyAppDb;Trusted_Connection=True;"
}
----------------------------------------
Ya agar SQL authentication use karte ho:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=MyAppDb;User Id=sa;Password=your_password;"
}
Ye appsettings.json me rakha jata hai.