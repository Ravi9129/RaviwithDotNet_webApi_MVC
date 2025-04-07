using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-12
{
    public class 10_Serilog Database Sink
    {
        
    }
}
--------------------------------------------------------
Serilog Database Sink ka matlab hota hai:
Log messages ko database (jaise SQL Server, PostgreSQL) me directly store karna.
Yeh enterprise-level logging ke liye use hota hai, taaki logs searchable aur centralized hoon.

🔧 Step-by-Step: Serilog + SQL Server Sink
🔹 1. Install NuGet Package

dotnet add package Serilog.Sinks.MSSqlServer
-------------------------------------------------------
🔹 2. Add Connection String in appsettings.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=LogDb;Trusted_Connection=True;"
  }
}
---------------------------------------
🔹 3. Create a Table (Optional)
Serilog will auto-create table agar permission hai. But you can manually create this too:

CREATE TABLE Logs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Message NVARCHAR(MAX),
    MessageTemplate NVARCHAR(MAX),
    Level NVARCHAR(128),
    TimeStamp DATETIMEOFFSET,
    Exception NVARCHAR(MAX),
    Properties NVARCHAR(MAX),
    LogEvent NVARCHAR(MAX)
)
-------------------------------------------
🔹 4. Configure in Program.cs

using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

var builder = WebApplication.CreateBuilder(args);

// SQL Server connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Define column options (optional)
var columnOptions = new ColumnOptions();
columnOptions.Store.Remove(StandardColumn.Properties); // remove if not needed
columnOptions.Store.Add(StandardColumn.LogEvent); // add if needed

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true // optional
        },
        columnOptions: columnOptions
    )
    .CreateLogger();

builder.Host.UseSerilog();
var app = builder.Build();

app.MapGet("/", () =>
{
    Log.Information("This is a test log");
    return "Hello World!";
});

app.Run();
-------------------------------------------------
🧪 Result in DB:
Id	Message	Level	TimeStamp	Exception	Properties
1	"This is a test log"	INF	2025-04-02 12:00:00	NULL	{ ... }
---------------------------------------------------
🔥 Pro Tips:
✅ Useful for:

Audit trails

Production issue debugging

Centralized log dashboards (Grafana, Kibana if connected)
-----------------------------------------------
✅ Can be used with:

PostgreSQL: Serilog.Sinks.PostgreSQL

MySQL: Serilog.Sinks.MySQL

MongoDB: Serilog.Sinks.MongoDB

Elasticsearch: Serilog.Sinks.Elasticsearch

