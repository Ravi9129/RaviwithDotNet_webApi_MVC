using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-9
{
    public class 12_DbSet 
    {
        
    }
}
----------------------------------------------------
DbSet<T> Entity Framework Core me database table ka representative hota hai. Ye basically model class ka collection hota hai jo database ke table ke row ke barabar hoti hain.

✅ Definition (Simple Words Me):
DbSet<T> ka matlab:

“Ek virtual table jo database me T naam ke model ka data store karta hai.”

🔥 Real-Life Analogy:
Jaise ek school me "Student" naam ka register hota hai jisme sab students ki entry hoti hai —
waise hi DbSet<Student> ek "Students" table banata hai jisme har object ek student ki row hoti hai.
--------------------------------------------------
🔧 Real Example:
🔹 1. Model:

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
---------------------------------------------------------------
🔹 2. DbContext Me DbSet Declare:

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
}
🧠 Iska matlab — ab tere paas ek "Students" table hoga database me, jisme Student object save honge.
-------------------------------------------------------------
✅ Common Operations with DbSet<T>
Operation	Code
Read (All)	_context.Students.ToList()
Read (Single)	_context.Students.FirstOrDefault(s => s.Id == 1)
Add	_context.Students.Add(new Student { Name = "Ali", Age = 22 })
Update	_context.Students.Update(studentObj)
Delete	_context.Students.Remove(studentObj)
Save	_context.SaveChanges()
-------------------------------------------------------------
🛠 Real Scenario:
🟩 Insert Student:

var std = new Student { Name = "Ravi", Age = 20 };
_context.Students.Add(std);
_context.SaveChanges();
-------------------------------------------------
🟩 Fetch All Students:

var allStudents = _context.Students.ToList();
-----------------------------------------------------
🟩 Update Student:

var student = _context.Students.Find(1);
student.Age = 21;
_context.SaveChanges();
--------------------------------------
🟩 Delete Student:

var student = _context.Students.Find(1);
_context.Students.Remove(student);
_context.SaveChanges();
---------------------------------------------------------------
🧠 Important Notes:
DbSet<T> ka naam default me plural hota hai (Student → Students).

Ye LINQ query support karta hai.

DbContext me jitne DbSet<T> honge, utni hi tables banengi.

Har T ek model class honi chahiye with proper properties.

