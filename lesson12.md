<img width="1113" height="626" alt="1231233333" src="https://github.com/user-attachments/assets/f56286f8-2420-4814-8f83-e123e14b3392" />

**Program.cs**
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/message", () => "Привет, это тестовое сообщение из ASP.NET API!");

app.Run();
```

**Models/Student.cs**:
```csharp
public class Student
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public int GroupId { get; set; }
}
```

**Models/Group.cs**:
```csharp
public class Group
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
```

**Data/StudentRepository.cs**:
```csharp
using Dapper;
using Microsoft.Data.SqlClient;

public class StudentRepository
{
    private readonly string _connStr = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public IEnumerable<Student> GetAll()
    {
        using var conn = new SqlConnection(_connStr);
        return conn.Query<Student>("SELECT * FROM Students");
    }
}
```

**Data/GroupRepository.cs**:
```csharp
using Dapper;
using Microsoft.Data.SqlClient;

public class GroupRepository
{
    private readonly string _connStr = "Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public IEnumerable<Group> GetAll()
    {
        using var conn = new SqlConnection(_connStr);
        return conn.Query<Group>("SELECT * FROM Groups");
    }
}
```
