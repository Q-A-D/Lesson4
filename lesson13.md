**Program.cs**:
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

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
    
    public Student GetById(int id)
    {
        using var conn = new SqlConnection(_connStr);
        return conn.QueryFirstOrDefault<Student>("SELECT * FROM Students WHERE Id = @Id", new { Id = id });
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
    
    public Group GetById(int id)
    {
        using var conn = new SqlConnection(_connStr);
        return conn.QueryFirstOrDefault<Group>("SELECT * FROM Groups WHERE Id = @Id", new { Id = id });
    }
}
```
